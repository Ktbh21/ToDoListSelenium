namespace Csharpseleniumtechassessment.ToDoList.Utilities
{
    public static class LoggingHelper
    {
        public static void LogMessage(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now}: {message}");
        }
    }
}