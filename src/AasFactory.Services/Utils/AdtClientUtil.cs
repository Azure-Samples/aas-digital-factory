using System.Net;

namespace AasFactory.Services.Utils;

public class AdtClientUtil : IAdtClientUtil
{
    /// <inheritdoc />
    public string RequestFailedExceptionFriendlyMessage(string message)
    {
        string msg = message;
        if (msg.IndexOf("\r\nStatus") > 0)
        {
            msg = msg.Substring(0, msg.IndexOf("\r\nStatus"));
        }

        return msg;
    }

    /// <inheritdoc />
    public bool ShouldNotContinueForCreateOrReplace(int statusCode, bool continueWithAdtErrorsSetting)
    {
        if (statusCode != (int)HttpStatusCode.NotFound && statusCode != (int)HttpStatusCode.BadRequest)
        {
            return true;
        }

        return !continueWithAdtErrorsSetting;
    }

    /// <inheritdoc />
    public bool ShouldNotContinueForDelete(int statusCode, bool continueWithAdtErrorsSetting)
    {
        if (statusCode != (int)HttpStatusCode.NotFound && statusCode != (int)HttpStatusCode.BadRequest)
        {
            return true;
        }
        else if (statusCode == (int)HttpStatusCode.NotFound)
        {
            return false;
        }

        return !continueWithAdtErrorsSetting;
    }
}