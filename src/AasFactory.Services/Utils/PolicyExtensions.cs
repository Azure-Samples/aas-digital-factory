namespace AasFactory.Services.Utils;

using System;
using System.Net;
using Azure;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.Simmy;
using Polly.Contrib.Simmy.Outcomes;
using Polly.Registry;
using StackExchange.Redis;

public static class PolicyExtensions
{
    public const string AdtPolicyName = "adtPolicy";
    public const string AdtAsyncPolicyName = "adtAsyncPolicy";
    public const string RedisAsyncPolicyName = "redisAsyncPolicy";
    public const string StorageAccountPolicyName = "storageAccountPolicy";
    private static string adtKey = "Adt";
    private static string redisKey = "Redis";
    private static string storageAccountKey = "StorageAccount";

    /// <summary>
    /// Adds Polly Policies to service collection.
    /// Overload method when circuit breaker config is supplied
    /// </summary>
    /// <param name="services">Service collection to add Polly policies to</param>
    /// <param name="allowedExceptionCount">Allowed exceptions in timeframe</param>
    /// <param name="waitTimeMinutes">Duration of Break</param>
    /// <param name="simmyInjectionRate">If set, rate to inject faults for chaos testing.</param>
    /// <param name="chaosDependencyTesting">If set, service to run dependency tests for.</param>
    /// <returns>void</returns>
    public static void AddPollyPolicies(
        this IServiceCollection services,
        int allowedExceptionCount,
        int waitTimeMinutes,
        double simmyInjectionRate,
        string chaosDependencyTesting) // ADT, Redis, Storage Account
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services
          .AddPolicyRegistry()
          .AddWrappedAdtPolicies(true, simmyInjectionRate, chaosDependencyTesting, allowedExceptionCount, waitTimeMinutes)
          .AddWrappedRedisPolicies(simmyInjectionRate, chaosDependencyTesting)
          .AddWrappedStorageAccountPolicies(simmyInjectionRate, chaosDependencyTesting);
    }

    /// <summary>
    /// Adds Polly Policies to service collection.
    /// Overload method in the case where Polly circuit breaker config is not required.
    /// </summary>
    /// <param name="services">Service collection to add Polly policies to</param>
    /// <param name="simmyInjectionRate">If set, rate to inject faults for chaos testing.</param>
    /// <param name="chaosDependencyTesting">If set, service to run dependency tests for.</param>
    /// <returns>void</returns>
    public static void AddPollyPolicies(
        this IServiceCollection services,
        double simmyInjectionRate,
        string chaosDependencyTesting) // ADT, Redis, Storage Account
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services
          .AddPolicyRegistry()
          .AddWrappedAdtPolicies(false, simmyInjectionRate, chaosDependencyTesting)
          .AddWrappedRedisPolicies(simmyInjectionRate, chaosDependencyTesting)
          .AddWrappedStorageAccountPolicies(simmyInjectionRate, chaosDependencyTesting);
    }

    /// <summary>
    /// Combines Polly Circuit Breaker + Simmy fault policies (for ADT).
    /// </summary>
    /// <param name="policyRegistry">Policy Registry to add the fault Policy to</param>
    /// <returns>void</returns>
    private static IPolicyRegistry<string> AddWrappedAdtPolicies(
      this IPolicyRegistry<string> policyRegistry,
      bool usingCircuitBreaker,
      double simmyInjectionRate,
      string chaosDependencyTesting,
      int circuitBreakerAllowedExceptionCount = 0,
      int circuitBreakerWaitTimeSeconds = 0)
    {
        if (policyRegistry is null)
        {
            throw new ArgumentNullException(nameof(policyRegistry));
        }

        // By default, use a no-op policy
        IAsyncPolicy adtAsyncPolicy = Policy.NoOpAsync();
        List<IAsyncPolicy> allAsyncPolicies = new List<IAsyncPolicy> { adtAsyncPolicy };

        ISyncPolicy adtPolicy = Policy.NoOp();
        List<ISyncPolicy> allPolicies = new List<ISyncPolicy> { adtPolicy };

        // Add additional policies per config
        if (usingCircuitBreaker)
        {
            var circuitBreakerPolicies = GetCircuitBreakerPolicy(circuitBreakerAllowedExceptionCount, circuitBreakerWaitTimeSeconds);
            allAsyncPolicies.Add(circuitBreakerPolicies.AsyncPolicy);
            allPolicies.Add(circuitBreakerPolicies.SyncPolicy);
        }

        if (string.Equals(chaosDependencyTesting, adtKey))
        {
            var adtFaultPolicies = GetRequestFailedExceptionFaultPolicy(simmyInjectionRate, adtKey);
            allAsyncPolicies.Add(adtFaultPolicies.AsyncPolicy);
            allPolicies.Add(adtFaultPolicies.SyncPolicy);
        }

        // If we ended up adding more policies, combine them - otherwise, just return no-op
        if (allPolicies.Count > 1)
        {
            adtPolicy = Policy.Wrap(allPolicies.ToArray());
        }

        if (allAsyncPolicies.Count > 1)
        {
            adtAsyncPolicy = Policy.WrapAsync(allAsyncPolicies.ToArray());
        }

        policyRegistry
            .Add(AdtPolicyName, adtPolicy);

        policyRegistry
            .Add(AdtAsyncPolicyName, adtAsyncPolicy);

        return policyRegistry;
    }

    /// <summary>
    /// Combines Polly NoOp + Simmy fault policies (for Redis).
    /// </summary>
    /// <param name="policyRegistry">Policy Registry to add the fault Policy to</param>
    /// <returns>void</returns>
    private static IPolicyRegistry<string> AddWrappedRedisPolicies(
        this IPolicyRegistry<string> policyRegistry,
        double simmyInjectionRate,
        string chaosDependencyTesting)
    {
        if (policyRegistry is null)
        {
            throw new ArgumentNullException(nameof(policyRegistry));
        }

        IAsyncPolicy redisPolicy;
        if (string.Equals(chaosDependencyTesting, redisKey))
        {
            redisPolicy = GetRedisConnectionExceptionFaultPolicy(simmyInjectionRate);
        }
        else
        {
            redisPolicy = Policy.NoOpAsync();
        }

        policyRegistry
            .Add(RedisAsyncPolicyName, redisPolicy);

        return policyRegistry;
    }

    /// <summary>
    /// Combines Polly NoOp + Simmy fault policies (for Storage Account).
    /// </summary>
    /// <param name="policyRegistry">Policy Registry to add the fault Policy to</param>
    /// <returns>void</returns>
    private static IPolicyRegistry<string> AddWrappedStorageAccountPolicies(
      this IPolicyRegistry<string> policyRegistry,
      double simmyInjectionRate,
      string chaosDependencyTesting)
    {
        if (policyRegistry is null)
        {
            throw new ArgumentNullException(nameof(policyRegistry));
        }

        ISyncPolicy storageAccountPolicy;
        if (string.Equals(chaosDependencyTesting, storageAccountKey))
        {
            storageAccountPolicy = GetRequestFailedExceptionFaultPolicy(simmyInjectionRate, storageAccountKey).SyncPolicy;
        }
        else
        {
            storageAccountPolicy = Policy.NoOp();
        }

        policyRegistry
            .Add(StorageAccountPolicyName, storageAccountPolicy);

        return policyRegistry;
    }

    private static (ISyncPolicy SyncPolicy, IAsyncPolicy AsyncPolicy) GetCircuitBreakerPolicy(int allowedExceptionCount, int waitTimeSeconds)
    {
        // Open circuit based on potentially transient HTTP error exceptions being thrown
        var policy = Policy
            .Handle<RequestFailedException>(ex =>
                ex.Status >= 500 ||
                ex.Status == (int)HttpStatusCode.RequestTimeout ||
                ex.Status == (int)HttpStatusCode.TooManyRequests);
        var circuitBreakerPolicy = policy.CircuitBreaker(allowedExceptionCount, TimeSpan.FromSeconds(waitTimeSeconds));
        var circuitBreakerPolicyAsync = policy.CircuitBreakerAsync(allowedExceptionCount, TimeSpan.FromSeconds(waitTimeSeconds));

        return (circuitBreakerPolicy, circuitBreakerPolicyAsync);
    }

    private static (ISyncPolicy SyncPolicy, IAsyncPolicy AsyncPolicy) GetRequestFailedExceptionFaultPolicy(double injectionRate, string serviceName)
    {
        // Causes the policy to throw a RequestFailedException with a probability of {injectionRate}% if enabled
        var fault = new RequestFailedException(500, $"Simmy: {serviceName} Internal Status Error");
        var chaosExceptionPolicy = MonkeyPolicy.InjectException(with =>
          with.Fault(fault)
            .InjectionRate(injectionRate)
            .Enabled());

        var chaosExceptionPolicyAsync = MonkeyPolicy.InjectExceptionAsync(with =>
          with.Fault(fault)
            .InjectionRate(injectionRate)
            .Enabled());

        return (chaosExceptionPolicy, chaosExceptionPolicyAsync);
    }

    private static IAsyncPolicy GetRedisConnectionExceptionFaultPolicy(double injectionRate)
    {
        // Redis exceptiions: https://github.com/StackExchange/StackExchange.Redis/blob/main/src/StackExchange.Redis/Exceptions.cs
        var fault = new RedisConnectionException(ConnectionFailureType.ConnectionDisposed, "Simmy: Redis connection disposed");
        var chaosExceptionPolicy = MonkeyPolicy.InjectExceptionAsync(with =>
          with.Fault(fault)
            .InjectionRate(injectionRate)
            .Enabled());

        return chaosExceptionPolicy;
    }
}
