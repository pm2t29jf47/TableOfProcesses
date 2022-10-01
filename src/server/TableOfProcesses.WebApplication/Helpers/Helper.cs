using System;

namespace TableOfProcesses.WebApplication.Helpers
{
    public class Helper
    {
        public static void LogInfo(string methodName, string message)
        {
            Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Info, Method: {methodName}, Message: {message}");
        }

        public static void LogError(string methodName, string message)
        {
            Console.WriteLine($"Time: {DateTime.UtcNow}, Level: Error, Method: {methodName}, Message: {message}");
        }
    }
}
