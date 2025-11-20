using CSHARPND1.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = CSHARPND1.Core.TaskStatus;

namespace CSHARPND1.App
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool isRunning = true;
            bool isChoicePage = true;
            TaskManager taskManager = new TaskManager();

            while (isRunning)
            {
                Console.Clear();
                if (isChoicePage)
                {
                    Console.WriteLine("---------------Task Manager---------------\n");
                    Console.WriteLine("Upcoming Tasks:");
                    List<BaseTask> upcomingTasks = taskManager.getTasksInDateRange(taskManager.getNotCompletedTasks(taskManager.AllTasks), DateTime.Now, 2);
                    upcomingTasks.Sort((t1, t2) => t1.CompareTo(t2));
                    foreach (BaseTask task in upcomingTasks)
                    {
                        if (task is TaskItem ti)
                        {
                            Console.WriteLine(ti.ToString(null, null));
                        }
                        else if (task is ReccuringTask rt)
                        {
                            Console.WriteLine(rt.ToString(null, null));
                        }
                    }
                    Console.WriteLine("\n-----------------Menu-----------------");
                    Console.WriteLine("1. Add new task");
                    Console.WriteLine("2. Add two simple tasks together");
                    Console.WriteLine("3. Update existing task");
                    Console.WriteLine("4. View all tasks");
                    Console.WriteLine("5. View Tasks");
                    Console.WriteLine("6. View Reccurent Tasks");
                    Console.WriteLine("7. Filter Tasks");
                    Console.WriteLine("8. Exit");
                }
                Console.Write("\nEnter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("What type of task do you want to add?");
                        Console.WriteLine("1. Standard Task | 2. Reccuring Task");
                        Console.Write("Task Type: ");
                        int taskType = int.Parse(Console.ReadLine());
                        if (taskType == 1)
                        {
                            Console.Write("Enter task name: ");
                            string name = Console.ReadLine();
                            Console.Write("\nEnter task description (optional): ");
                            string description = Console.ReadLine();
                            Console.Write("\nEnter due date (yyyy-MM-dd): ");
                            DateTime dueDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("\nEnter priority (0. None, 1. Low, 2. Medium, 3. High): ");
                            int priorityInput = int.Parse(Console.ReadLine());
                            if (priorityInput < 0 || priorityInput > 3)
                            {
                                Console.WriteLine("Invalid priority. Setting to None.");
                                priorityInput = 0;
                            }
                            Priority priority = (Priority)priorityInput;
                            TaskItem newTask = new TaskItem(name, description, dueDate, priority);
                            taskManager.AddTask(newTask);
                            Console.WriteLine("Task added successfully! ");
                        }
                        else if (taskType == 2)
                        {
                            Console.Write("Enter task name: ");
                            string name = Console.ReadLine();
                            Console.Write("\nEnter task description (optional): ");
                            string description = Console.ReadLine();
                            Console.Write("\nEnter due date (yyyy-MM-dd): ");
                            DateTime dueDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("\nEnter priority (0. None, 1. Low, 2. Medium, 3. High): ");
                            int priorityInput = int.Parse(Console.ReadLine());
                            if (priorityInput < 0 || priorityInput > 3)
                            {
                                Console.WriteLine("Invalid priority. Setting to None. ");
                                priorityInput = 0;
                            }
                            Priority priority = (Priority)priorityInput;
                            Console.Write("\nEnter reccurence interval in days: ");
                            int interval = int.Parse(Console.ReadLine());
                            ReccuringTask newReccuringTask = new ReccuringTask(name, description, dueDate, priority, interval);
                            taskManager.AddTask(newReccuringTask);
                            Console.WriteLine("Reccuring Task added successfully! ");
                        }

                        Console.WriteLine("Press any key to return to main menu... ");
                        Console.ReadKey();
                        break;

                    case 2:
                        isChoicePage = false;
                        Console.Clear();
                        Console.WriteLine("------------------Tasks-------------------");
                        foreach (TaskItem task in taskManager.getTaskItems())
                        {
                            Console.WriteLine(task.ToString("detailed", null));
                            Console.WriteLine();
                        }
                        Console.Write("\nEnter the ID of the first task: ");
                        uint firstTaskId = uint.Parse(Console.ReadLine());
                        Console.Write("Enter the ID of the second task: ");
                        uint secondTaskId = uint.Parse(Console.ReadLine());
                        TaskItem compoundTask = (TaskItem)taskManager.getTaskById(firstTaskId) + (TaskItem)taskManager.getTaskById(secondTaskId);
                        taskManager.AddTask(compoundTask);
                        Console.WriteLine("\nCompound Task created successfully! ");
                        Console.WriteLine(compoundTask.ToString("detailed", null));
                        Console.WriteLine("Press any key to return to main menu... ");
                        Console.ReadKey();
                        isChoicePage = true;
                        break;
                    case 3:
                        isChoicePage = false;
                        printAllTasks(taskManager);
                        Console.Write("\nEnter the ID of the task you want to update: ");
                        uint taskId = uint.Parse(Console.ReadLine());
                        bool isUpdating = true;
                        while (isUpdating)
                        {
                            Console.Clear();
                            BaseTask taskToUpdate = taskManager.getTaskById(taskId);
                            Console.WriteLine("---------------Update Task----------------");
                            if (taskToUpdate is TaskItem ti)
                            {
                                Console.WriteLine(ti.ToString("detailed", null));
                            }
                            else if (taskToUpdate is ReccuringTask rt)
                            {
                                Console.WriteLine(rt.ToString("detailed", null));
                            }
                            Console.WriteLine("\nWhat do you want to update?\n1. Mark as Complete\n2. Name\n3. Description\n4. Due Date\n5. Priority\n6. Reccurence Interval (For ReccurentTask)\n7. Remove Task");
                            Console.Write("Enter your choice (or 0 to go back): ");
                            int updateChoice = int.Parse(Console.ReadLine());
                            switch (updateChoice)
                            {
                                case 0:
                                    isUpdating = false;
                                    isChoicePage = true;
                                    break;
                                case 1:
                                    taskToUpdate.markAsComplete();
                                    taskManager.updateTask(taskToUpdate);
                                    break;
                                case 2:
                                    Console.Write("Enter new name: ");
                                    string newName = Console.ReadLine();
                                    taskToUpdate.TaskName = newName;
                                    taskManager.updateTask(taskToUpdate);
                                    break;
                                case 3:
                                    Console.Write("Enter new description: ");
                                    string newDescription = Console.ReadLine();
                                    taskToUpdate.TaskDescription = newDescription;
                                    taskManager.updateTask(taskToUpdate);
                                    break;
                                case 4:
                                    Console.Write("Enter new due date (yyyy-MM-dd): ");
                                    DateTime newDueDate = DateTime.Parse(Console.ReadLine());
                                    taskToUpdate.DueDate = newDueDate;
                                    taskToUpdate.updateStatus();
                                    taskManager.updateTask(taskToUpdate);
                                    break;
                                case 5:
                                    Console.Write("Enter new priority (0. None, 1. Low, 2. Medium, 3. High): ");
                                    int newPriorityInput = int.Parse(Console.ReadLine());
                                    if (newPriorityInput < 0 || newPriorityInput > 3)
                                    {
                                        Console.WriteLine("Invalid priority. Setting to None. ");
                                        newPriorityInput = 0;
                                    }
                                    Priority newPriority = (Priority)newPriorityInput;
                                    taskToUpdate.Priority = newPriority;
                                    taskManager.updateTask(taskToUpdate);
                                    break;
                                case 6:
                                    if (taskToUpdate is ReccuringTask recTask)
                                    {
                                        Console.Write("Enter new reccurence interval in days: ");
                                        int newInterval = int.Parse(Console.ReadLine());
                                        recTask.ReccurenceIntervalDays = newInterval;
                                        taskManager.updateTask(recTask);
                                    }
                                    else
                                    {
                                        Console.WriteLine("This task is not a Reccuring Task.");
                                    }
                                    break;
                                case 7:
                                    taskManager.RemoveTask(taskToUpdate);
                                    isUpdating = false;
                                    isChoicePage = true;
                                    break;
                            }

                        }
                        break;

                    case 4:
                        isChoicePage = false;
                        printAllTasks(taskManager);
                        Console.WriteLine("\nPress any key to return to main menu... ");
                        Console.ReadKey();
                        isChoicePage = true;
                        break;

                    case 5:
                        isChoicePage = false;
                        Console.Clear();
                        Console.WriteLine("------------------Tasks-------------------");
                        foreach (TaskItem task in taskManager.getTaskItems())
                        {
                            Console.WriteLine(task.ToString("detailed", null));
                            Console.WriteLine();
                        }
                        Console.WriteLine("\nPress any key to return to main menu... ");
                        Console.ReadKey();
                        isChoicePage = true;
                        break;
                    case 6:
                        isChoicePage = false;
                        Console.Clear();
                        Console.WriteLine("-------------Reccuring Tasks--------------");
                        foreach (ReccuringTask task in taskManager.getReccuringTasks())
                        {
                            Console.WriteLine(task.ToString("detailed", null));
                            Console.WriteLine();
                        }
                        Console.WriteLine("\nPress any key to return to main menu... ");
                        Console.ReadKey();
                        isChoicePage = true;
                        break;

                    case 7:
                        isChoicePage = false;
                        Console.Clear();
                        printAllTasks(taskManager);
                        Console.WriteLine("What task types do you want to filter?\n1. All Tasks\n2. Standard Tasks\n3. Reccuring Tasks");
                        Console.Write("Enter your choice: ");
                        int taskTypeFilterChoice = int.Parse(Console.ReadLine());
                        List<BaseTask> tasksToFilter = taskTypeFilterChoice switch
                        {
                            1 => taskManager.AllTasks,
                            2 => taskManager.getTaskItems(),
                            3 => taskManager.getReccuringTasks(),
                            _ => taskManager.AllTasks,
                        };
                        Console.WriteLine("\nChoose Action:\n0. Exit filter Menu\n1. Filter by Status\n2. Filter by Priority\n3. Filter by Due Date Range\n4. Sort");
                        Console.Write("Enter your choice: ");
                        int filterChoice = int.Parse(Console.ReadLine());
                        switch (filterChoice)
                        {
                            case 0:
                                isChoicePage = true;
                                break;
                            case 1:
                                Console.WriteLine("Select Status to filter by: 1. In Progress | 2. Completed | 3. Late");
                                Console.Write("Enter your choice: ");
                                int statusInput = int.Parse(Console.ReadLine());
                                TaskStatus statusFilter = (TaskStatus)statusInput;
                                List<BaseTask> statusFilteredTasks = taskManager.getTasksByStatus(statusFilter, tasksToFilter);
                                Console.Clear();
                                Console.WriteLine($"-------Tasks with Status: {statusFilter}-------");
                                foreach (BaseTask task in statusFilteredTasks)
                                {
                                    if (task is TaskItem ti)
                                    {
                                        Console.WriteLine(ti.ToString("detailed", null));
                                        Console.WriteLine();
                                    }
                                    else if (task is ReccuringTask rt)
                                    {
                                        Console.WriteLine(rt.ToString("detailed", null));
                                        Console.WriteLine();
                                    }
                                }
                                Console.WriteLine("\nPress any key to return to main menu... ");
                                Console.ReadKey();
                                isChoicePage = true;
                                break;
                            case 2:
                                Console.WriteLine("Select Priority to filter by:\n0. NONE\n1. LOW\n2. MEDIUM\n3. HIGH");
                                Console.Write("Enter your choice: ");
                                int priorityInput = int.Parse(Console.ReadLine());
                                Priority priorityFilter = (Priority)priorityInput;
                                List<BaseTask> priorityFilteredTasks = taskManager.getTasksByPriority(priorityFilter, tasksToFilter);
                                Console.Clear();
                                Console.WriteLine($"-------Tasks with Priority: {priorityFilter}-------");
                                foreach (BaseTask task in priorityFilteredTasks)
                                {
                                    if (task is TaskItem ti)
                                    {
                                        Console.WriteLine(ti.ToString("detailed", null));
                                        Console.WriteLine();
                                    }
                                    else if (task is ReccuringTask rt)
                                    {
                                        Console.WriteLine(rt.ToString("detailed", null));
                                        Console.WriteLine();
                                    }
                                }
                                Console.WriteLine("\nPress any key to return to main menu... ");
                                Console.ReadKey();
                                isChoicePage = true;
                                break;
                            case 3:
                                Console.Write("Enter start date (yyyy-MM-dd): ");
                                DateTime startDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Enter range in days: ");
                                int range = int.Parse(Console.ReadLine());
                                List<BaseTask> dateRangeFilteredTasks = taskManager.getTasksInDateRange(tasksToFilter, startDate, range);
                                Console.Clear();
                                Console.WriteLine($"---Tasks due between {startDate.ToShortDateString()} and {startDate.AddDays(range).ToShortDateString()}---");
                                break;
                            case 4:
                                List<BaseTask> sortedTasks = tasksToFilter;
                                sortedTasks.Sort((t1, t2) => t1.CompareTo(t2));
                                Console.Clear();
                                Console.WriteLine("-------Tasks sorted by Due Date-------");
                                foreach (BaseTask task in sortedTasks)
                                {
                                    if (task is TaskItem ti)
                                    {
                                        Console.WriteLine(ti.ToString("detailed", null));
                                        Console.WriteLine();
                                    }
                                    else if (task is ReccuringTask rt)
                                    {
                                        Console.WriteLine(rt.ToString("detailed", null));
                                        Console.WriteLine();
                                    }
                                }
                                Console.WriteLine("\nPress any key to return to main menu... ");
                                Console.ReadKey();
                                isChoicePage = true;
                                break;
                        }
                        break;
                    case 8:
                        taskManager.clearAllTasks();
                        isRunning = false;
                        break;
                }
            }
        }

        public static void printAllTasks(TaskManager taskManager)
        {
            Console.Clear();
            Console.WriteLine("---------------All Tasks----------------");
            foreach (BaseTask task in taskManager.AllTasks)
            {
                if (task is TaskItem ti)
                {
                    Console.WriteLine(ti.ToString("detailed", null));
                    Console.WriteLine();
                }
                else if (task is ReccuringTask rt)
                {
                    Console.WriteLine(rt.ToString("detailed", null));
                    Console.WriteLine();
                }
            }
        }
    }
}
