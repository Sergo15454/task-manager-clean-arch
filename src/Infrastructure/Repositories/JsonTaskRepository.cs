using System.Text.Json;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class JsonTaskRepository : ITaskRepository
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void Add(TaskItem task)
    {
        var tasks = Load();
        tasks.Add(task);
        Save(tasks);
    }

    public TaskItem? GetById(Guid id) => Load().FirstOrDefault(t => t.Id == id);

    public IReadOnlyList<TaskItem> GetAll() => Load();

    public void Update(TaskItem task)
    {
        var tasks = Load();
        var index = tasks.FindIndex(t => t.Id == task.Id);

        if (index < 0)
            throw new KeyNotFoundException($"Завдання з Id {task.Id} не знайдено.");

        tasks[index] = task;
        Save(tasks);
    }

    private List<TaskItem> Load()
    {
        if (!File.Exists(_filePath))
            return new List<TaskItem>();

        var json = File.ReadAllText(_filePath);
        var records = JsonSerializer.Deserialize<List<TaskRecord>>(json) ?? new List<TaskRecord>();

        return records
            .Select(r => new TaskItem(r.Id, r.Title, r.IsCompleted, r.CreatedAt))
            .ToList();
    }

    private void Save(List<TaskItem> tasks)
    {
        var records = tasks.Select(t => new TaskRecord(t.Id, t.Title, t.IsCompleted, t.CreatedAt));
        File.WriteAllText(_filePath, JsonSerializer.Serialize(records, Options));
    }

    private record TaskRecord(Guid Id, string Title, bool IsCompleted, DateTime CreatedAt);
}