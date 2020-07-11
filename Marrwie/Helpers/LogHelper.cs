namespace Marrwie.Helpers
{
    public static class LogHelper
    {
        public static readonly string LogFolder = "~\\Store";

        public static bool TryLog(string logPath, string logMessage)
        {
            bool result;

            try
            {
                System.IO.File.AppendAllText(logPath, logMessage);
                result = true;
            }
            catch
            {
                // Do nothing.
                result = false;
            }

            return result;
        }
    }
}