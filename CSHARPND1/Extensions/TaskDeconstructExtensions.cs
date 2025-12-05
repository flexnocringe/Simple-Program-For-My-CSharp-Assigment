using CSHARPND1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Extensions
{
    //Praplėtimo dekonstruktoriai (1 t.)
    static partial class TaskDeconstructExtensions
    {
        public static void Deconstruct(this TaskItem task, out uint id, out string taskName, out string taskDescription, out DateTime dueDate, out Priority priority, out Core.TaskStatus status)
        {
            id = task.Id;
            taskName = task.TaskName;
            taskDescription = task.TaskDescription;
            dueDate = task.DueDate;
            priority = task.Priority;
            status = task.Status;
        }

        public static void Deconstruct(this ReccuringTask task, out uint id, out string taskName, out string taskDescription, out DateTime dueDate, out DateTime nextDueDate, out int reccurenceInterval, out Priority priority, out Core.TaskStatus status)
        {
            id = task.Id;
            taskName = task.TaskName;
            taskDescription = task.TaskDescription;
            dueDate = task.DueDate;
            nextDueDate = task.NextDueDate;
            reccurenceInterval = task.ReccurenceIntervalDays;
            priority = task.Priority;
            status = task.Status;
        }
    }
}
