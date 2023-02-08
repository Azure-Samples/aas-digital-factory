# Overall Architecture <!-- omit in toc -->

This document captures the high level architecture of the project.

- [Model Transformation Flow](#model-transformation-flow)
- [Streaming Data Flow](#streaming-data-flow)
- [Updates to an existing Model](#updates-to-an-existing-model)
- [Q\&A](#qa)

We will have two main flows in the architecture:

1. Syncing model data including factory, lines, machine types, and machines.
1. Processing telemetry data as per ADT rate limiting requirements.

![Architecture](./assets/Swimlane%20-%20overall%20process.png)

## Model Transformation Flow

The input to this data processing flow is the factory's model data specification in JSON format, conforming to the schema detailed [here](./design/model-data-raw-to-aas.md).
This assumes that this file will be stored in an Azure blob storage account, and that an event will be triggered at this time to notify an Azure function (#1) to process the
json file.
This Azure function will use a list of mapping rules to translate the factory model data JSON file into another model data based on Asset Administration
Shell(AAS) metamodel.
A configuration file will help define the mapping of the factory models and their standard fields to AAS.
The mapping describes which models will be converted to assets or submodels and how the fields of a factory model will be mapped to this new metamodel.

```json
{
   "operationalData": {
      "submodels": [
        {
          "idShort": "OperationalData",
          "subModelElements":
          [
            {
              "name": "StartTime",
            },
            {
              "name": "EndTime"
            },
          ]
        }
      ],
      "defaultSubmodel": "OperationalData"
   }

}
```

After processing data and converting the factory model data into AAS model data, the Azure function will write that to another JSON file in a separate folder in the same storage account as the input;
it will then notify the next Azure function (#2) via an Event Hub message.
The second function will process the AAS-formatted model data
to create a representative graph in Azure Digital Twins (ADT).

## Streaming Data Flow

The streaming data flow is very similar to the model data transformation flow.
In this flow, we assume that the streaming data will be sent to an EventHub from some service that receives IoT sensor data, where each event contains information for some property values that correspond to twins in our model graph.
We will again use the mapping rules to translate this data to AAS format in an Azure Function (#3).
In doing so, we will also need to know the exact list of ADT
twins that will need to be updated as the IDs of the twins are unique based on the naming convention described in [this document](./model-data-aas-naming.md).
After converting data to AAS format, it will send the newly converted payload via Event Hub message to another Azure Function (#4), as shown in the overall architecture diagram.
Via configuration, ADT will automatically store all historical updates in Azure Data Explorer (ADX).

## Updates to an existing Model

When model data for a particular factory is first input into this workflow, the entire graph is generated in ADT.
On subsequent updates for the same factory model, the corresponding twins and relationships in the ADT graph would either change or stay the same.
Function 2 handles this by using the ADT SDK `CreateOrReplace` methods, which covers the cases where the twins/relationships stay the same across updates.
It also has a cleanup process for deleting the twins/relationships that are no longer present after an update;
it does this by maintaining a list of the twins and relationships created via the specified input to F2, and then deleting any graph element with the prefix `aas_` not present in that list.

This logic could potentially be made more efficient by having F2 perform only the required diff of operations to get the graph to the desired updated state; this is not currently implemented in the project.

## Q&A

1. Why AAS?
    The Industrie 4.0 Asset Administration Shell (AAS) is an open standard for the exchange of information between partners in the manufacturing
     value chain. The Asset Administration Shell (AAS) /digital twin is the digital representation of an asset.

    Converting to AAS adds complexity but on the other hand it is the manufacturing industry std. and give us flexibility. Traditionally what would
     become a DtDL model change followed by updating the graph when any schema changes happens is now a simple graph update as the AAS metamodels
      don’t have to change.

1. Why Event Hub vs other messaging systems in Azure for Telemetry Data flow?
    - Event hub is recommended for ingesting high volume of data specially for telemetry data
    - Event hub can ingress 1MB/s or 1000 events per second per TU and egress 2MB/s or 4K events per seconds with no upper limit on the number of TUs.
    - Lighter version of Service Bus due to not having support for some features such as dead letter feature and atomic operations which are not
     required for this project - In terms of cost, event hub is cheaper than other messaging services as per the customer requirement's and their
      benchmark for volume of telemetry data

1. Why the use of a blob storage?
    - The size of model data can be bigger than 1MB.

1. Why we need to have a messaging system for Model Data flow?
    - There is a max delay of 10mins if azure function triggered by blob storage
    - Retry mechanism can be done without pushing data again
    - Less complexity in our dependency maintenance (rather than having EventHub + any other different messaging service)

1. Why Event Hub vs. other messaging systems in Azure for Model Data flow?
    - The best option for messaging in this project would be Event Grid, but this sample chose to use EventHub for integration points in order to have consistency across the solution.
    - Mainly because of maintaining two different message service technologies from implementing multiple coding, monitoring and tooling cost perspective.

1. What are the downsides of using Event Hub for Model Data flow?
    - Event Hub is not recommended for low volume data ingestion.
    - Costly for low volume data, but it will use the same EventHub namespace as the telemetry data. Hence, the cost will be unremarkable.

1. Why do we need 2 Event Hubs in Telemetry Flow instead of a single Event Hub?
    - Scalable. Data egress rate is important with telemetry data. By having the second function/Event Hub, the processing load will be distributed
     between two functions and so will increase throughput rate.
    - If accomplishing all this in a single EventHub, then processing of responsibilities of these two functions in one single function will make the
     function busier and not able to keep up with the backlog, since the major processing time is taken by the function is related to the ADT
      operation (negligible for first function and approximately 56 seconds for patching 1000 twins).
    - Loosely Couple. It facilitates troubleshooting, retriable executions, and parallel development and is easier to maintain.
    - Versioning/Deployments. If the event contract has changed, then only update that specific component instead of
     updating all components involved.

1. Why do we need 2 Event Hubs in Model Flow instead of a single Event Hub?
    - In addition to the benefits of versioning and loosely coupling, the first Azure function will implement fan-out pattern to accomplish the load
     of model data by creating multiple messages, one for each bulk ingestion of 25K twins for the second Azure function.

1. Why do we have a 2 step process for model and telemetry ingestion flows?
    - Adding a logical layer on top of ADT to handle the ADT updates and encapsulate the complexity of AAS from top layer
    - The ADT component is easily reusable across different partners, though the AAS conversion layer may need to be updated based on differences in partner schemas
    - Lighter/thinner functions with limited responsibilities instead of thicker functions to do complex work
    - Easily extendable to support other AAS applications instead of ADT
    - Parallel/fast development and easy to maintain code
    - Retry any step without starting from the beginning in case of any failure
    - Scalable. Data egress rate is important with telemetry data. By having the second function, the processing load will be distributed between two
     functions and so will increase ingestion rate.
