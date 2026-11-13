namespace Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Назва завдання не може бути порожньою.", nameof(title));

        Id = Guid.NewGuid();
        Title = title.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    // Конструктор для відновлення збереженого завдання (використовує Infrastructure)
    public TaskItem(Guid id, string title, bool isCompleted, DateTime createdAt)
    {
        Id = id;
        Title = title;
        IsCompleted = isCompleted;
        CreatedAt = createdAt;
    }

    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Завдання вже виконане.");

        IsCompleted = true;
    }
}