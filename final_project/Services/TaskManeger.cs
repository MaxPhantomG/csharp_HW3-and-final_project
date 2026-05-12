using TaskHub.Models;

namespace TaskHub.Services
{
    public class TaskManager
    {
        private readonly List<TaskItem> _tasks = new();

        public void AddTask(TaskItem task)
        {
            _tasks.Add(task);
        }

        public List<TaskItem> GetAllTasks()
        {
            return _tasks;
        }

        public TaskItem? GetTaskById(Guid id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public void RemoveTask(Guid id)
        {
            var task = GetTaskById(id);

            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public List<TaskItem> GetCompletedTasks()
        {
            return _tasks
                .Where(t => t.Status == TaskStatuses.Done)
                .ToList();
        }

        public List<TaskItem> GetPendingTasks()
        {
            return _tasks
                .Where(t => t.Status != TaskStatuses.Done)
                .ToList();
        }

        public List<TaskItem> GetHighPriorityTasks()
        {
            return _tasks
                .Where(t => t.Priority == PriorityLevel.High)
                .ToList();
        }

        public List<TaskItem> SearchByTitle(string title)
        {
            return _tasks
                .Where(t => t.Title.Contains(
                    title,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<TaskItem> SearchByStatus(Models.TaskStatuses status)
        {
            return _tasks
                .Where(t => t.Status == status)
                .ToList();
        }

        public List<TaskItem> SearchByPriority(PriorityLevel priority)
        {
            return _tasks
                .Where(t => t.Priority == priority)
                .ToList();
        }

        public void UpdateTask(
            Guid id,
            string title,
            string description,
            PriorityLevel priority,
            Models.TaskStatuses status)
        {
            var task = GetTaskById(id);

            if (task != null)
            {
                task.Title = title;
                task.Description = description;
                task.Priority = priority;
                task.Status = status;
            }
        }

        public void SetTasks(List<TaskItem> tasks)
        {
            _tasks.Clear();
            _tasks.AddRange(tasks);
        }
    }
}
