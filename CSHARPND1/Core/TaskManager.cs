using CSHARPND1.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
    class TaskManager
    {
        // Naudojamas List is System.Collections.Generic (1 t)
        private TaskCollection<BaseTask> tasks = new TaskCollection<BaseTask>();

        public TaskManager() { }

        public TaskCollection<BaseTask> AllTasks
        {
            get { return tasks; }
        }

        // params raktažodis naudojamas keliems užduotims pridėti (0,5 t)
        public void AddTask(params BaseTask[] tasks)
        {
            this.tasks.AddRange(tasks);
        }

        public void RemoveTask(uint taskId)
        {
            tasks.RemoveAll(t => t.Id == taskId);
        }

        public void RemoveTask(BaseTask task)
        {
            tasks.Remove(task);
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

        // Delegatai su lambdą funkcijoms naudojami filtravimo funkcijai jinai bus kviešiama kituose metoduose (1,5 t)
        public TaskCollection<BaseTask> findTasks(Func<BaseTask, bool> predicate, TaskCollection<BaseTask> tasks)
        {
            return tasks.Where(predicate);
        }

        // Čia is raktažodis naudojamas surasti tik TaskItem tipo užduotis iš BaseTask masyvo (0,5 t)
        public TaskCollection<BaseTask> getTaskItems()
        {
            return tasks.Where(t => t is TaskItem);
        }

        // Čia tas pats tik ReccuringTask tipo užduotims (0,5 t)
        public TaskCollection<BaseTask> getReccuringTasks()
        {
            return tasks.Where(t => t is ReccuringTask);
        }

        public TaskCollection<BaseTask> getTasksByStatus(TaskStatus status, TaskCollection<BaseTask> tasks)
        {
            return findTasks(t=>t.Status==status, tasks);
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
