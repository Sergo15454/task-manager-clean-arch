using Domain.Entities;
using Domain.Interfaces;

namespace Application.UseCases;

/// <summary>Сценарій: отримання списку всіх завдань.</summary>
public class GetAllTasksUseCase
{
    private readonly ITaskRepository _repository;

    public GetAllTasksUseCase(ITaskRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IReadOnlyList<TaskItem> Execute() => _repository.GetAll();
}
