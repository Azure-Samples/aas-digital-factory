# Chaos Testing on Serverless Functions

This document describes chaos testing experiments with [Simmy](https://github.com/Polly-Contrib/Simmy),
in order to prove the behavior hypothesis.
Per the scope of story #198, exceptions are injected for a subset of the Azure services that are used in the project:
Azure Digital Twins (ADT), Redis, and storage accounts.
Results from these experiments across the relevant Azure Functions containing those dependencies are described below.

- [Chaos Testing on Serverless Functions](#chaos-testing-on-serverless-functions)
  - [Experiments](#experiments)
    - [Azure Digital Twins (ADT)](#azure-digital-twins-adt)
      - [What if ADT requests fail at a 5% rate with 500 RequestFailedException?](#what-if-adt-requests-fail-at-a-5-rate-with-500-requestfailedexception)
      - [What if ADT requests fail at a 100% rate with 500 RequestFailedException?](#what-if-adt-requests-fail-at-a-100-rate-with-500-requestfailedexception)
    - [Storage Account](#storage-account)
      - [What if Storage Account requests fail at a 5% rate with 500 RequestFailedException?](#what-if-storage-account-requests-fail-at-a-5-rate-with-500-requestfailedexception)
      - [What if Storage Account requests fail at a 100% rate with 500 RequestFailedException?](#what-if-storage-account-requests-fail-at-a-100-rate-with-500-requestfailedexception)
    - [Chaos testing extensions](#chaos-testing-extensions)

## Experiments

### Azure Digital Twins (ADT)

We are using the [ADT .NET SDK](https://learn.microsoft.com/en-us/azure/digital-twins/concepts-apis-sdks#net-c-sdk-data-plane).
Note that retry logic is built into this [by default](https://learn.microsoft.com/en-us/azure/digital-twins/reference-service-limits#working-with-limits);
exceptions are only surfaced at the client level once the internal retry policy has been exhausted.
Since the SDK (similar to many Azure SDKs - [docs](https://learn.microsoft.com/en-us/azure/architecture/best-practices/retry-service-specific#event-hubs))
contains this built-in retry logic, we opted to not use an additional Polly retry policy.
Simmy exceptions are injected in place of the SDK call wrapped by the policy,
meaning that they are meant to represent errors thrown by the SDK after it has gone through its internal retry policies.
If we were able to inject Simmy exceptions at the layer that they would be able to be retried by the SDK, we could expect to see fewer exceptions logged.

In the streaming data flow, we are using the [Polly library implementation of the Circuit Breaker resilience pattern](https://github.com/App-vNext/Polly#circuit-breaker)
to manage the flow of requests when the client encounters repeated exceptions:
when the client receives some set amount of consecutive exceptions (configured in our code),
the circuit breaks and no longer allows any requests using the wrapped ADT SDK calls to proceed.

Note that in the model data flow, we have designed for a 'fail-fast' approach, which is intended to make the function immediately stop on
encountering an error.
A more detailed discussion of the error handling logic here can be found in the [function project README](../../src/AasFactory.Azure.Functions.ModelDataFlow/README.md#error-handling).

#### What if ADT requests fail at a 5% rate with 500 RequestFailedException?

**Function impacted**: #4

**Experiment setup**: each event contains updates for 4 properties on 1 machine; events are sent every 10 seconds.
All events contain valid properties and, absent any injected exceptions, are expected to be processed successfully.

**Impact**:
Per the fairly low injection rate of exceptions, we see very few exceptions logged.
When an exception occurs, the logs show the message `Failed to update twin id - {twinId}`,
along with the injected Simmy error `Polly.Contrib.Simmy: Simmy: ADT Internal Status Error.`,
and the function proceeds normally after that.

Since we are only varying the probability that Simmy injects an exception, Simmy injects exceptions randomly.
Therefore, during this testing at a 5% injection rate, we did not encounter enough consecutive exceptions to trigger the circuit breaker policy.
To see what that behavior might be like, please see the [section below](#what-if-adt-requests-fail-at-a-100-rate-with-500-requestfailedexception).

#### What if ADT requests fail at a 100% rate with 500 RequestFailedException?

**Function impacted**: #4

**Experiment setup**: each event contains updates for 4 properties on 1 machine; events are sent every 10 seconds.
All events contain valid properties and, absent any injected exceptions, are expected to be processed successfully.
Note that the default value for the allowed exception count before the circuit breaker policy gets triggered is 3 -
therefore, since the exception rate is 100% (i.e. every SDK call should fail), we expect to see the Polly circuit break during the processing of this event.

**Impact**:

- First property is being processed and throws 500 RequestFailedException.
- Log with event id 503 is emitted (`Failed to update twin id - {twinId}`)
- Second property is being processed and throws Polly `BrokenCircuitException` (`The circuit is now open and is not allowing calls`).
- Log with event id 200 is emitted (`Failed to process a function`)
- Function crashes. None of the ADT twins are updated.

*Update*:

This is a desired behavior. We considered adding a catch block for exceptions of
`BrokenCircuitException` type to `AdtClient`. However, it does not add any value.
We prefer to fail fast, rather than logging every `BrokenCircuitException` and waisting computing resources.

### Storage Account

#### What if Storage Account requests fail at a 5% rate with 500 RequestFailedException?

**Function impacted**: #1

**Experiment setup**: each event points to a blob file that contains 1 factory, 1 line, 1 machine type, 3 machines and 1 event hub.
The function app settings are properly configured to point to this uploaded blob file in a storage account container.

**Impact**:

There are two potential points of failure due to storage dependencies in Function 1 -
case (1) is at the start, when it tries to download the initial blob file;
case (2) is at the end, when it tries to upload the converted AAS blob file.

Per the fairly low injection rate of exceptions, we see very few exceptions occur.
If it fails at case (1), it emits the error `There was an error downloading and deserializing the blob.`, and the function crashes and does not proceed.
If it fails at case (2), it emits the error `There was an error uploading the shells - {aasBlobPath}` and throws that error, and similarly does not proceed.

**Function impacted**: #2

**Experiment setup**: each event points to an AAS blob file that contains 1 factory, 1 line, 1 machine type, 3 machines.
The function app settings are properly configured to point to this uploaded blob file in a storage account container.

**Impact**:

Per the fairly low injection rate of exceptions, we see very few exceptions occur.
When an exception occurs, it is at the point where Function 2 tries to download the AAS blob file from storage.
At this point, it emits the error message `Failed to process a function`, and the function does not proceed.
No twins are updated.

#### What if Storage Account requests fail at a 100% rate with 500 RequestFailedException?

**Function impacted**: #1

**Experiment setup**: each event points to a blob file that contains 1 factory, 1 line, 1 machine type, 3 machines and 1 event hub.

**Impact**:

- It always throws an exception as it tries to download factory blob file.
- When downloading, log with event id 1010 is emitted (`There was an error downloading and deserializing the blob.`).
- Function crashes. No event is generated for function #2.

**Function impacted**: #2

**Experiment setup**: each event points to an AAS blob file that contains 1 factory, 1 line, 1 machine type, 3 machines.

**Impact**:

- It always throws an exception as it tries to download the AAS blob file.
- When downloading, log with event id 200 is emitted (`Failed to process a function`).
- Function crashes. None twin is updated.

### Chaos testing extensions

There are some additional scenarios for potential points of failure in the system that are not covered in scope of this story to be tested by Simmy.
However, it is still a useful exercise to identify them, and they could be investigated further in another story.

- What if ADT client timeout is exceeded?
- What if ADT server timeout is exceeded?
- What if Event Hubs are down?
- What if Azure Data Explorer (ADX) is down?
