using CSHARPND1.Collection;
using CSHARPND1.Exceptions;
using CSHARPND1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
    class TaskManager
    {
        private TaskCollection<BaseTask> tasks = new TaskCollection<BaseTask>();

        public TaskManager() { }

        public TaskCollection<BaseTask> AllTasks
        {
            get { return tasks; }
        }
        // Naudojami try/catch blokai (1t.)
        public void AddTask(params BaseTask[] tasks)
        {
            try
            {
                this.tasks.AddRange(tasks);
            }
            catch (InvalidTaskActionException ex)
            {
                throw new InvalidTaskActionException("Error adding tasks: " + ex.Message);
            }
        }

        public void RemoveTask(uint taskId)
        {
            try
            {
                tasks.RemoveAll(t => t.Id == taskId);
            }
            catch (InvalidTaskActionException ex)
            {
                throw new InvalidTaskActionException("Error removing task: " + ex.Message);
            }
        }

        public void RemoveTask(BaseTask task)
        {
            try
            {
                tasks.Remove(task);
            }
            catch (InvalidTaskActionException ex)
            {
                throw new InvalidTaskActionException("Error removing task: " + ex.Message);
            }
        }

        public BaseTask getTaskById(uint taskId)
        {
            return tasks.First(taskId);
        }

        public void updateTask(BaseTask updatedTask)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks.Get(i).Id == updatedTask.Id)
                {
                    tasks.Update(updatedTask);
                    break;
                }
            }
        }

        public TaskCollection<BaseTask> findTasks(Func<BaseTask, bool> predicate, TaskCollection<BaseTask> tasks)
        {
            return tasks.Where(predicate);
        }

        public TaskCollection<BaseTask> getTaskItems()
        {
            return tasks.Where(t => t is TaskItem);
        }


        public TaskCollection<BaseTask> getReccuringTasks()
        {
            return tasks.Where(t => t is ReccuringTask);
        }

        public TaskCollection<BaseTask> getTasksByStatus(TaskStatus status, TaskCollection<BaseTask> tasks)
        {
            return tasks.WithStatus(status);
        }

        public TaskCollection<BaseTask> getTasksByPriority(Priority priority, TaskCollection<BaseTask> tasks)
        {
            return findTasks(t => t.Priority == priority, tasks);
        }

        public TaskCollection<BaseTask> getNotCompletedTasks(TaskCollection<BaseTask> tasks)
        {
            return findTasks(t=>!t.IsCompleted, tasks);
        }

        public TaskCollection<BaseTask> getTasksInDateRange(TaskCollection<BaseTask> tasks, DateTime startDate, int range)
        {
            return findTasks(t=>t.DueDate >= startDate && t.DueDate <= startDate.AddDays(range), tasks);
        }

        public void clearAllTasks()
        {
            tasks.Clear();
        }
    }
}
