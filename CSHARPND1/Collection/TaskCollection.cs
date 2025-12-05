using CSHARPND1.Core;
using CSHARPND1.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Collection
{
    // Ši kolekcija leidžia valdyti užduotys ir implementuoja IEnumerable<T> (1 t.)
    // Naudojamas 'where' raktažodis (1 t.)
    internal class TaskCollection<T> : IEnumerable<T> where T : BaseTask
    {
        private List<T> tasks = new List<T>();

        public void Add(T task)
        {
            if(task == null)
            {
                throw new InvalidTaskActionException("Task cannot be null");
            }
            tasks.Add(task);
        }
        
        public void AddRange(IEnumerable<T> tasks)
        {
            if(tasks == null)
            {
                throw new InvalidTaskActionException("Tasks collection cannot be null");
            }
            this.tasks.AddRange(tasks);
        }

        public void Remove(T task)
        {
            if (!tasks.Remove(task))
            {
                throw new InvalidTaskActionException("Task not found in collection");
            }
        }

        public int RemoveAll(Predicate<T> match)
        {
            return tasks.RemoveAll(match);
        }

        public T Get(int index)
        {
            if(index < 0 || index >= tasks.Count)
            {
                throw new InvalidTaskActionException("Index is out of range");
            }
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
