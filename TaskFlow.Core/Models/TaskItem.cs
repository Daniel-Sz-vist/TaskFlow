namespace TaskFlow.Core.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Assignee { get; set; } = string.Empty;
    public TaskState Status { get; set; } = TaskState.NotStarted;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public void Validate()
    {
        if (ProjectId == Guid.Empty)
        {
            throw new ArgumentException("Task must belong to a valid project.", nameof(ProjectId));
        }

        if (string.IsNullOrWhiteSpace(Title))
        {
            throw new ArgumentException("Task title is required.", nameof(Title));
        }

        if (string.IsNullOrWhiteSpace(Assignee))
        {
            throw new ArgumentException("Task assignee is required.", nameof(Assignee));
        }
    }
}
