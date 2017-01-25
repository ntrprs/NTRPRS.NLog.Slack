using System;
using NLog;

namespace NTRPRS.NLog.Slack.Demo
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            Logger.Info("This is a piece of useful information.");
            Logger.Debug("I be debuggin' all night long");
            Logger.Warn("Warning, warning... I'm UTF8 ready : 你好世界.");
            Logger.Info("<https://alert-system.com/alerts/1234|Link to something> just to show off!");

            try
            {
                CreateBigStackTrace(20);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "KABOOM!");
            }

            var customAttachemnetDemo = new AttachmentDemoWithAuthor("Hey, you can create your own attachment too! Just pass me in the list of params of NLog methods");
            Logger.Info(customAttachemnetDemo);

            Logger.Info(new AttachmentDemoWithImage());

            Console.WriteLine("Done - check your Slack channel!");
            Console.ReadLine();
        }

        private static void CreateBigStackTrace(int lines)
        {
            if (lines < 1)
            {
                throw new Exception("I'm the exception to the rule.");
            }
            CreateBigStackTrace(lines - 1);
        }
    }
}
