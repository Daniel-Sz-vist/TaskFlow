using TaskFlow.Core.Models;
using TaskFlow.Core.Repositories;

var repository = new TaskFlowRepository();
var project = repository.AddProject("TaskFlow MVP", "Initial project scaffolding and data model.");

repository.AddTask(project.Id, "Prepare domain model", "Daniel", TaskState.InProgress, TaskPriority.High);
repository.AddTask(project.Id, "Document repository setup", "Joanna", TaskState.NotStarted, TaskPriority.Medium);

Console.WriteLine($"Project: {project.Name}");
Console.WriteLine("Task | Assignee | Status | Priority");
Console.WriteLine("--------------------------------");

foreach (var task in repository.GetTasksForProject(project.Id))
{
    Console.WriteLine($"{task.Title} | {task.Assignee} | {task.Status} | {task.Priority}");
}
