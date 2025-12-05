using CSHARPND1.Collection;
using CSHARPND1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Extensions
{
    static partial class TaskExtensions
    {
        //Čia yra generic plėtimo metodas (1 t.)
        public static TaskCollection<T> WithStatus<T>(this TaskCollection<T> tasks, Core.TaskStatus status) where T : BaseTask
        {
            TaskCollection<T> filteredTasks = new TaskCollection<T>();
            foreach(var task in tasks)
            {
                if(task.Status == status)
                {
                    filteredTasks.Add(task);
                }
            }
            return filteredTasks;
        }
    }
}
