using NLog;
using NTRPRS.NLog.Slack.Models;

namespace NTRPRS.NLog.Slack
{
    /// <summary>
    /// Implement this interface to have custom Attachment for your object
    /// </summary>
    public interface ISlackLoggable
    {
        Attachment ToAttachment(LogEventInfo info);
    }
}
