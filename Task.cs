using System;
using System.Collections.Generic;

public class Task
{
    public string Name { get; set; }
    public int Priority { get; set; }

    public Task(string name, int priority)
    {
        Name = name;
        Priority = priority;
    }
}

public class TaskScheduler
{
    private LinkedList<Task> taskList;

    public TaskScheduler()
    {
        taskList = new LinkedList<Task>();
    }

    public void AddTask(string name, int priority)
    {
        Task newTask = new Task(name, priority);

        LinkedListNode<Task> currentNode = taskList.First;
        while (currentNode != null && currentNode.Value.Priority >= priority)
        {
            currentNode = currentNode.Next;
        }

        if (currentNode == null)
        {
            taskList.AddLast(newTask);
        }
        else
        {
            taskList.AddBefore(currentNode, newTask);
        }
    }

    public void RemoveTask(string name)
    {
        LinkedListNode<Task> nodeToRemove = taskList.Find(new Task(name, 0));
        if (nodeToRemove != null)
        {
            taskList.Remove(nodeToRemove);
        }
    }

    public void RemoveTaskBasedOnUserChoice(string taskName)
    {
        Console.WriteLine("\nChoose the deletion option:");
        Console.WriteLine("1. Delete from the beginning");
        Console.WriteLine("2. Delete from the end");
        Console.WriteLine("3. Delete from the middle");

        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                RemoveTaskFromBeginning();
                break;
            case 2:
                RemoveTaskFromEnd();
                break;
            case 3:
                RemoveTaskFromMiddle(taskName);
                break;
            default:
                Console.WriteLine("Invalid choice. No task deleted.");
                break;
        }
    }

    private void RemoveTaskFromBeginning()
    {
        if (taskList.Count > 0)
        {
            taskList.RemoveFirst();
        }
        else
        {
            Console.WriteLine("Task list is empty. No task deleted.");
        }
    }

    private void RemoveTaskFromEnd()
    {
        if (taskList.Count > 0)
        {
            taskList.RemoveLast();
        }
        else
        {
            Console.WriteLine("Task list is empty. No task deleted.");
        }
    }

    private void RemoveTaskFromMiddle(string taskName)
    {
        LinkedListNode<Task> nodeToRemove = taskList.Find(new Task(taskName, 0));

        if (nodeToRemove != null)
        {
            taskList.Remove(nodeToRemove);
        }
        else
        {
            Console.WriteLine($"Task '{taskName}' not found. No task deleted.");
        }
    }

    public void ExecuteTasks()
    {
        foreach (Task task in taskList)
        {
            Console.WriteLine($"Executing task: {task.Name}, Priority: {task.Priority}");
        }
    }
}

class Program
{
    static void Main()
    {
        TaskScheduler scheduler = new TaskScheduler();

        scheduler.AddTask("Task1", 2);
        scheduler.AddTask("Task2", 1);
        scheduler.AddTask("Task3", 3);

        Console.WriteLine("Tasks in the scheduler:");
        scheduler.ExecuteTasks();

        Console.Write("\nEnter the name of the task to remove: ");
        string taskToRemove = Console.ReadLine();

        scheduler.RemoveTaskBasedOnUserChoice(taskToRemove);

        Console.WriteLine($"\nTasks after removing {taskToRemove}:");
        scheduler.ExecuteTasks();
    }
}
