# TaskFlow

TaskFlow is a small .NET console application for managing projects and tasks in a lightweight team workflow.

## Solution structure

- `TaskFlow.App` — console entry point.
- `TaskFlow.Core` — domain model and repository logic.
- `TaskFlow.Tests` — xUnit tests for project and task validations.

## Local setup

```bash
dotnet restore
dotnet build TaskFlow.slnx
dotnet run --project TaskFlow.App/TaskFlow.App.csproj
```

## Current implementation

This repository currently covers the first part of the Daniel Szmelter scope from the project plan:

- .NET solution scaffolded with a console app and test project
- domain models for projects, tasks, statuses and priorities
- repository layer for creating projects and tasks
- validation rules for required fields and invalid project references
