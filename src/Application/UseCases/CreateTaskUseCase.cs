using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

/// <summary>Сценарій: створення нового завдання.</summary>
public class CreateTaskUseCase
{
    private readonly ITaskRepository _repository;

    public CreateTaskUseCase(ITaskRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public TaskItem Execute(string title)
    {
        var task = new TaskItem(title);
        _repository.Add(task);
        return task;
    }
}
