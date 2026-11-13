using Domain.Entities;

namespace Domain.Interfaces;

/// <summary>
/// Контракт сховища завдань. Реалізується в шарі Infrastructure.
/// </summary>
public interface ITaskRepository
{
    /// <summary>Додає нове завдання до сховища.</summary>
    void Add(TaskItem task);

    /// <summary>Повертає завдання за ідентифікатором або null, якщо його не знайдено.</summary>
    TaskItem? GetById(Guid id);

    /// <summary>Повертає всі завдання.</summary>
    IReadOnlyList<TaskItem> GetAll();

    /// <summary>Зберігає зміни в існуючому завданні.</summary>
    void Update(TaskItem task);
}