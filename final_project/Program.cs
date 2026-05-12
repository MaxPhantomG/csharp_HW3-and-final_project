using TaskHub.Models;
using TaskHub.Services;
using TaskHub.Delegates;

TaskManager manager = new();

using FileService fileService =
    new FileService("Data/tasks.json");

var loadedTasks = await fileService.LoadAsync();

manager.SetTasks(loadedTasks);

DeadlineMonitor monitor =
    new DeadlineMonitor(manager.GetAllTasks());

monitor.Start();

TaskNotification notification =
    message =>
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    };

bool running = true;

while (running)
{
    Console.WriteLine("""
=========================
        TASK HUB
=========================

1. Add Task
2. Show All Tasks
3. Show Completed Tasks
4. Show Pending Tasks
5. Show High Priority Tasks
6. Update Task
7. Remove Task
8. Search By Title
9. Search By Status
10. Search By Priority
11. Statistics
12. Save Tasks
0. Exit

Choose:
""");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":

            try
            {
                Console.Write("Title: ");
                string title = Console.ReadLine();

                Console.Write("Description: ");
                string description =
                    Console.ReadLine();

                Console.Write(
                    "Priority (Low, Medium, High): ");

                PriorityLevel priority =
                    Enum.Parse<PriorityLevel>(
                        Console.ReadLine(),
                        true);

                Console.Write(
                    "Deadline (yyyy-mm-dd): ");

                DateTime deadline =
                    DateTime.Parse(Console.ReadLine());

                Console.Write(
                    "Status (New, InProgress, Done): ");

                TaskStatuses status =
                    Enum.Parse<TaskStatuses>(
                        Console.ReadLine(),
                        true);

                TaskItem task = new()
                {
                    Title = title,
                    Description = description,
                    Priority = priority,
                    Deadline = deadline,
                    Status = status
                };

                manager.AddTask(task);

                notification(
                    "Task added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Input error: {ex.Message}");
            }

            break;

        case "2":

            foreach (var task
                in manager.GetAllTasks())
            {
                Console.WriteLine(task);
            }

            break;

        case "3":

            foreach (var task
                in manager.GetCompletedTasks())
            {
                Console.WriteLine(task);
            }

            break;

        case "4":

            foreach (var task
                in manager.GetPendingTasks())
            {
                Console.WriteLine(task);
            }

            break;

        case "5":

            foreach (var task
                in manager.GetHighPriorityTasks())
            {
                Console.WriteLine(task);
            }

            break;

        case "6":

            try
            {
                Console.Write("Enter task ID: ");

                Guid updateId =
                    Guid.Parse(Console.ReadLine());

                Console.Write("New title: ");
                string newTitle =
                    Console.ReadLine();

                Console.Write("New description: ");
                string newDescription =
                    Console.ReadLine();

                Console.Write(
                    "New priority (Low, Medium, High): ");

                PriorityLevel newPriority =
                    Enum.Parse<PriorityLevel>(
                        Console.ReadLine(),
                        true);

                Console.Write(
                    "New status (New, InProgress, Done): ");

                TaskStatuses newStatus =
                    Enum.Parse<TaskStatuses>(
                        Console.ReadLine(),
                        true);

                manager.UpdateTask(
                    updateId,
                    newTitle,
                    newDescription,
                    newPriority,
                    newStatus);

                notification(
                    "Task updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Update error: {ex.Message}");
            }

            break;

        case "7":

            try
            {
                Console.Write("Enter task ID: ");

                Guid removeId =
                    Guid.Parse(Console.ReadLine());

                manager.RemoveTask(removeId);

                notification(
                    "Task removed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Remove error: {ex.Message}");
            }

            break;

        case "8":

            Console.Write("Enter title: ");

            string keyword =
                Console.ReadLine();

            var foundByTitle =
                manager.SearchByTitle(keyword);

            foreach (var task in foundByTitle)
            {
                Console.WriteLine(task);
            }

            break;

        case "9":

            Console.Write(
                "Enter status (New, InProgress, Done): ");

            TaskStatuses searchStatus =
                Enum.Parse<TaskStatuses>(
                    Console.ReadLine(),
                    true);

            var foundByStatus =
                manager.SearchByStatus(searchStatus);

            foreach (var task in foundByStatus)
            {
                Console.WriteLine(task);
            }

            break;

        case "10":

            Console.Write(
                "Enter priority (Low, Medium, High): ");

            PriorityLevel searchPriority =
                Enum.Parse<PriorityLevel>(
                    Console.ReadLine(),
                    true);

            var foundByPriority =
                manager.SearchByPriority(
                    searchPriority);

            foreach (var task in foundByPriority)
            {
                Console.WriteLine(task);
            }

            break;

        case "11":

            StatisticsService.ShowStatistics(
                manager.GetAllTasks());

            break;

        case "12":

            await fileService.SaveAsync(
                manager.GetAllTasks());

            notification(
                "Tasks saved successfully!");

            break;

        case "0":

            await fileService.SaveAsync(
                manager.GetAllTasks());

            running = false;

            break;

        default:

            Console.WriteLine(
                "Invalid menu item");

            break;
    }
}
