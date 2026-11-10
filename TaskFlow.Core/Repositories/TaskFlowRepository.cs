using TaskFlow.Core.Models;

namespace TaskFlow.Core.Repositories;

public sealed class TaskFlowRepository
{
    public List<Project> Projects { get; } = new();
    public List<TaskItem> Tasks { get; } = new();

    public Project AddProject(string name, string? description = null)
    {
        var project = new Project
        {
            Name = name,
            Description = description
        };

        project.Validate();
        Projects.Add(project);

        return project;
    }

    public TaskItem AddTask(TaskItem task)
    {
        if (task is null)
        {
            throw new ArgumentNullException(nameof(task));
        }

        task.Validate();

        if (!Projects.Any(project => project.Id == task.ProjectId))
        {
            throw new InvalidOperationException("Task cannot be added to a project that does not exist.");
        }

        Tasks.Add(task);
        return task;
    }

    public TaskItem AddTask(Guid projectId, string title, string assignee, TaskState status = TaskState.NotStarted, TaskPriority priority = TaskPriority.Medium)
    {
        var task = new TaskItem
        {
            ProjectId = projectId,
            Title = title,
            Assignee = assignee,
            Status = status,
            Priority = priority
        };

        return AddTask(task);
    }

    public IEnumerable<TaskItem> GetTasksForProject(Guid projectId)
    {
        return Tasks.Where(task => task.ProjectId == projectId);
    }

    public IEnumerable<TaskItem> GetTasksByStatus(TaskState status)
    {
        return Tasks.Where(task => task.Status == status);
    }

    public IEnumerable<TaskItem> GetTasksByAssignee(string assignee)
    {
        return Tasks.Where(task => string.Equals(task.Assignee, assignee, StringComparison.OrdinalIgnoreCase));
    }
}
