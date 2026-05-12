using TaskHub.Models;

namespace TaskHub.Services
{
    public static class StatisticsService
    {
        public static void ShowStatistics(
            List<TaskItem> tasks)
        {
            Console.WriteLine("\n===== STATISTICS =====");

            Console.WriteLine(
                $"Total tasks: {tasks.Count}");

            Console.WriteLine(
                $"Completed tasks: " +
                $"{tasks.Count(t => t.Status == TaskStatuses.Done)}");

            Console.WriteLine(
                $"Overdue tasks: " +
                $"{tasks.Count(t => t.IsOverdue)}");

            Console.WriteLine("\nBy priority:");

            var grouped = tasks
                .GroupBy(t => t.Priority);

            foreach (var group in grouped)
            {
                Console.WriteLine(
                    $"{group.Key}: {group.Count()}");
            }

            Console.WriteLine();
        }
    }
}
