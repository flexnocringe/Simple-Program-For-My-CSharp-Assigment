using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
    class TaskItem : BaseTask, IFormattable
    {
        public TaskItem(string taskName, string? taskDescripiton, DateTime dueDate, Priority priority) : base(taskName, taskDescripiton, dueDate, priority) { }

        // Implementuotas IFormattable interfeisas ToString metodo perkrovimui (1 t)
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch (format)
            {
                case "detailed":
                    return $"[Task]\nid: {id}\nName: {taskName}\nDescription: {taskDescription}\nDate Created: {dateCreated}\nDue Date: {dueDate}\nCompleted?: {isCompleted}\nPriority: {priority}\nStatus: {status}";
                case "status":
                    return this.status.ToString();
                default:
                    return $"[Task] {taskName} - Due: {dueDate.ToShortDateString()} - Priority: {priority}";
            }
        }

        // Buvo perkrautas + operatorius dviem užduotim į viena nauja sujungumui (0,5 t)
        public static TaskItem operator + (TaskItem task1, TaskItem task2)
        {
            string newName = task1.TaskName + " & " + task2.TaskName;
            string newDescription = (task1.TaskDescription ?? "No description") + " | " + (task2.TaskDescription ?? "");
            DateTime newDueDate = (task1.DueDate > task2.DueDate) ? task1.DueDate : task2.DueDate;
            Priority newPriority = (task1.Priority > task2.Priority) ? task1.Priority : task2.Priority;
            return new TaskItem(newName, newDescription, newDueDate, newPriority);
        }

        // Buvo implementuotas destruktorius visiem užduoties kintamiesiems gauti (0,5 t)
        public void Deconstruct(out uint id, out string taskName, out string taskDescription, out DateTime dueDate, out Priority priority, out TaskStatus status)
        {
            id = this.id;
            taskName = this.TaskName;
            taskDescription = this.TaskDescription ?? "No description";
            dueDate = this.DueDate;
            priority = this.Priority;
            status = this.status;
        }

        // Buvo perkrauti lyginimo operatoriai (==, !=) kurie implementuoja iš BaseTask paveldima Equals metodą (0,5 t)
        public static bool operator ==(TaskItem task1, TaskItem? task2) => task1.Equals(task2);
        public static bool operator !=(TaskItem task1, TaskItem? task2) => !task1.Equals(task2);

        public override void markAsComplete()
        {
            IsCompleted = true;
            updateStatus();
        }
    }
}
