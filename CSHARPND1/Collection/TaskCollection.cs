using CSHARPND1.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Collection
{
    internal class TaskCollection<T> : IEnumerable<T> where T : BaseTask
    {
        private readonly List<T> tasks = new List<T>();

        public void Add(T task)
        {
            tasks.Add(task);
        }
        
        public void AddRange(IEnumerable<T> tasks)
        {
            this.tasks.AddRange(tasks);
        }

        public bool Remove(T task)
        {
            return tasks.Remove(task);
        }

        public int RemoveAll(Predicate<T> match)
        {
            return tasks.RemoveAll(match);
        }

        public T Get(int index)
        {
            return tasks[index];
        }

        public T First(uint taskId)
        {
            return tasks.First(t => t.Id == taskId);
        }

        public void Update(T taskToUpdate)
        {
            for(int i=0; i<tasks.Count; i++)
            {
                if (tasks[i].Id == taskToUpdate.Id)
                {
                        tasks[i] = taskToUpdate;
                }
            }
        }

        public TaskCollection<T> Where(Func<T, bool> predicate)
        {
            TaskCollection<T> result = new TaskCollection<T>();
            foreach (var task in tasks)
            {
                if (predicate(task))
                {
                    result.Add(task);
                }
            }
            return result;
        }

        public TaskCollection<T> Sort(Comparison<T> comparison)
        {
            TaskCollection<T> sortedCollection = new TaskCollection<T>();
            List<T> sortedList = new List<T>(tasks);
            sortedList.Sort(comparison);
            sortedCollection.AddRange(sortedList);
            return sortedCollection;
        }

        public int Count => tasks.Count;

        public void Clear()
        {
            tasks.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TaskIterator<T>(tasks);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
