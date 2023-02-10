public interface IAdtClientUtil
{
    /// <summary>
    /// RequestFailedException message is concatenation of error message, error code, status and stack trace.
    /// Ref: https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/src/RequestFailedException.cs
    /// This method can be used to extract just the message content.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public string RequestFailedExceptionFriendlyMessage(string message);

    /// <summary>
    ///  This method is used to define what should be the behavior for the ADT create or replace twins/relationships error handling
    ///  For bad request and not found exceptions, there is an option to terminate the execution via config value continueWithAdtErrors
    ///  and for all other system errors the execution will terminate irrespective of the config value.
    ///  The config value gives flexibility for debugging and failing fast for few scenarios like when the model definitions
    ///  in ADT instance dont match with the code definitions, model id doesnt exist in ADT instance, or the data coming in is not
    ///  valid etc. These scenarios may need human intervention and based on the preference for handling the fixes the config value
    ///  can be set accordingly. For other system errors exection is terminated and the function has to be re-run based on the error.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="continueWithAdtErrorsSetting"></param>
    /// <returns></returns>
    public bool ShouldNotContinueForCreateOrReplace(int statusCode, bool continueWithAdtErrorsSetting);

    /// <summary>
    /// This method is used to define what should be the behavior for the ADT delete twins/relationships error handling.
    /// For Delete bad request, there is an option to terminate the execution via config value continueWithAdtErrors
    /// and for all other system errors the execution will terminate irrespective of the config value.
    /// The config value gives flexibility for debugging and failing fast for few scenarios like when there dangling relationships
    /// for a twin and the code attempts to delete a twin. These scenarios may need human intervention and based on the preference for
    /// handling the fixes the config value can be set accordingly.
    /// For not found scenario in delete operations the code just proceeds, 2 ways to have the scenario
    /// 1. When outside of the function clean up of twins and relationships happens in parallel.
    /// 2. When twin creation failed (to make the execution faster there are no checks added for whether twin was created
    /// before a relationship is created so if twin/relationship creation failure).
    /// But if there is a need for it in the future, the method can easily changed to account for it.
    /// And also to keep it simple, the same status code checks have been added to both twins and relationships
    /// but that doesnt mean it will likely happen to both.
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="continueWithAdtErrorsSetting"></param>
    /// <returns></returns>
    public bool ShouldNotContinueForDelete(int statusCode, bool continueWithAdtErrorsSetting);
}