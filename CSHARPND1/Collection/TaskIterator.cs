using CSHARPND1.Core;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Collection
{
    // Implementuotas IEnumerator<T> (1 t.)
    // Šitas enumetatorius yra naudojamas TaskCollection klasėje (0,5 t.)
    internal class TaskIterator<T> : IEnumerator<T> where T : BaseTask
    {
        private T current;
        private int index = -1;
        private List<T> tasks;

        public TaskIterator(List<T> tasks)
        {
            this.tasks = tasks;
        }

        public T Current => current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if(++index >= tasks.Count)
            {
                return false;
            }
            current = tasks[index];
            return true;
        }

        public void Reset()
        {
            index = -1;
            current = default;
        }
    }
}
