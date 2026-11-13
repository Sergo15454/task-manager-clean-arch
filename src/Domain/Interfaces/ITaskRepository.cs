using Domain.Entities;

namespace Domain.Interfaces;

public interface ITaskRepository
{
    void Add(TaskItem task);
    TaskItem? GetById(Guid id);
    IReadOnlyList<TaskItem> GetAll();
    void Update(TaskItem task);
}