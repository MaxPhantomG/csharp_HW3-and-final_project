using TaskHub.Models;

namespace TaskHub.Services
{
    public class DeadlineMonitor
    {
        private readonly List<TaskItem> _tasks;

        public DeadlineMonitor(List<TaskItem> tasks)
        {
            _tasks = tasks;
        }

        public void Start()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    foreach (var task in _tasks)
                    {
                        if (task.IsOverdue)
                        {
                            Console.ForegroundColor =
                                ConsoleColor.Red;

                            Console.WriteLine(
                                $"\nWARNING! " +
                                $"Task overdue: {task.Title}");

                            Console.ResetColor();
                        }
                    }

                    await Task.Delay(5000);
                }
            });
        }
    }
}
