using System;

namespace TaskHub.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Description { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime Deadline { get; set; }

        public TaskStatuses Status { get; set; }

        public bool IsOverdue =>
            Deadline < DateTime.Now &&
            Status != TaskStatuses.Done;

        public override string ToString()
        {
            return
                $"ID: {Id}\n" +
                $"Title: {Title}\n" +
                $"Description: {Description}\n" +
                $"Priority: {Priority}\n" +
                $"Status: {Status}\n" +
                $"Deadline: {Deadline}\n";
        }
    }
}

