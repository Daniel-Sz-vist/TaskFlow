using TaskFlow.Core.Models;
using TaskFlow.Core.Repositories;

namespace TaskFlow.Tests.Tests;

public class DomainTests
{
    [Fact]
    public void AddProject_WithValidData_Succeeds()
    {
        var repository = new TaskFlowRepository();

        var project = repository.AddProject("TaskFlow MVP");

        Assert.Equal("TaskFlow MVP", project.Name);
        Assert.Single(repository.Projects);
    }

    [Fact]
    public void AddProject_WithoutName_ThrowsArgumentException()
    {
        var repository = new TaskFlowRepository();

        Assert.Throws<ArgumentException>(() => repository.AddProject("   "));
    }

    [Fact]
    public void AddTask_WithMissingProject_ThrowsInvalidOperationException()
    {
        var repository = new TaskFlowRepository();

        Assert.Throws<InvalidOperationException>(() => repository.AddTask(Guid.NewGuid(), "Write tests", "Anna"));
    }

    [Fact]
    public void AddTask_WithMissingTitle_ThrowsArgumentException()
    {
        var repository = new TaskFlowRepository();
        var project = repository.AddProject("TaskFlow MVP");

        var task = new TaskItem
        {
            ProjectId = project.Id,
            Title = " ",
            Assignee = "Anna"
        };

        Assert.Throws<ArgumentException>(() => repository.AddTask(task));
    }

    [Fact]
    public void Repository_CanFilterTasksByProjectAndAssignee()
    {
        var repository = new TaskFlowRepository();
        var project = repository.AddProject("Mobile App");
        var projectTwo = repository.AddProject("Web App");

        repository.AddTask(project.Id, "Create login screen", "Anna", TaskState.InProgress, TaskPriority.High);
        repository.AddTask(project.Id, "Prepare icons", "Daniel", TaskState.NotStarted, TaskPriority.Low);
        repository.AddTask(projectTwo.Id, "Deploy server", "Anna", TaskState.Completed, TaskPriority.Critical);

        var projectTasks = repository.GetTasksForProject(project.Id).ToList();
        var annaTasks = repository.GetTasksByAssignee("Anna").ToList();

        Assert.Equal(2, projectTasks.Count);
        Assert.Equal(2, annaTasks.Count);
    }
}
