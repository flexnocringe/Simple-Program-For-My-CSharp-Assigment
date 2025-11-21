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
        private List<BaseTask> tasks = new List<BaseTask>();

        public TaskManager() { }

        public List<BaseTask> AllTasks
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
            return tasks.First(t => t.Id == taskId);
        }

        public void updateTask(BaseTask updatedTask)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Id == updatedTask.Id)
                {
                    tasks[i] = updatedTask;
                    break;
                }
            }
        }

        // Delegatai su lambdą funkcijoms naudojami filtravimo funkcijai jinai bus kviešiama kituose metoduose (1,5 t)
        public List<BaseTask> findTasks(Func<BaseTask, bool> predicate, List<BaseTask> tasks)
        {
            return tasks.Where(predicate).ToList();
        }

        // Čia is raktažodis naudojamas surasti tik TaskItem tipo užduotis iš BaseTask masyvo (0,5 t)
        public List<BaseTask> getTaskItems()
        {
            return tasks.Where(t => t is TaskItem).ToList();
        }

        // Čia tas pats tik ReccuringTask tipo užduotims (0,5 t)
        public List<BaseTask> getReccuringTasks()
        {
            return tasks.Where(t => t is ReccuringTask).ToList();
        }

        public List<BaseTask> getTasksByStatus(TaskStatus status, List<BaseTask> tasks)
        {
            return findTasks(t=>t.Status==status, tasks);
        }

        public List<BaseTask> getTasksByPriority(Priority priority, List<BaseTask> tasks)
        {
            return findTasks(t => t.Priority == priority, tasks);
        }

        public List<BaseTask> getNotCompletedTasks(List<BaseTask> tasks)
        {
            return findTasks(t=>!t.IsCompleted, tasks);
        }

        public List<BaseTask> getTasksInDateRange(List<BaseTask> tasks, DateTime startDate, int range)
        {
            return findTasks(t=>t.DueDate >= startDate && t.DueDate <= startDate.AddDays(range), tasks);
        }

        public void clearAllTasks()
        {
            tasks.Clear();
        }
    }
}
