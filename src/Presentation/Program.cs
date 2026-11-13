using System.Text;
using Application.UseCases;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

// Composition root: тут збираються залежності (Dependency Injection через конструктор).
var storagePath = args.Length > 0 ? args[0] : "tasks.json";
ITaskRepository repository = new JsonTaskRepository(storagePath);
var createTask = new CreateTaskUseCase(repository);
var completeTask = new CompleteTaskUseCase(repository);
var getAllTasks = new GetAllTasksUseCase(repository);

static void PrintTasks(IReadOnlyList<TaskItem> tasks)
{
    if (tasks.Count == 0)
    {
        Console.WriteLine("Список завдань порожній.");
        return;
    }

    for (var i = 0; i < tasks.Count; i++)
    {
        var mark = tasks[i].IsCompleted ? "[x]" : "[ ]";
        Console.WriteLine($"{i + 1}. {mark} {tasks[i].Title} ({tasks[i].CreatedAt:dd.MM.yyyy HH:mm})");
    }
}

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== TaskManager ===");
    Console.WriteLine("1. Додати завдання");
    Console.WriteLine("2. Показати список завдань");
    Console.WriteLine("3. Позначити завдання виконаним");
    Console.WriteLine("0. Вийти");
    Console.Write("Ваш вибір: ");

    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Назва завдання: ");
                var task = createTask.Execute(Console.ReadLine() ?? string.Empty);
                Console.WriteLine($"Створено завдання: {task.Title}");
                break;

            case "2":
                PrintTasks(getAllTasks.Execute());
                break;

            case "3":
                var tasks = getAllTasks.Execute();
                PrintTasks(tasks);
                if (tasks.Count == 0) break;

                Console.Write("Номер завдання: ");
                if (int.TryParse(Console.ReadLine(), out var number) && number >= 1 && number <= tasks.Count)
                {
                    completeTask.Execute(tasks[number - 1].Id);
                    Console.WriteLine("Завдання позначено виконаним.");
                }
                else
                {
                    Console.WriteLine("Невірний номер завдання.");
                }
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невідома команда.");
                break;
        }
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException or KeyNotFoundException)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}
