using CSHARPND1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
    // Base Task yra abstrakti klasė, ja paveldi TaskItem ir ReccuringTask (0,5 t)
    abstract class BaseTask : IComparable<BaseTask> , IEquatable<BaseTask>
    {
        private static uint idCounter = 0;
        protected uint id;
        protected string taskName;
        protected string? taskDescription;
        protected DateTime dateCreated;
        protected DateTime dueDate;
        protected bool isCompleted;
        protected Priority priority;
        protected TaskStatus status;

        // Konstruktoriuje panaudotas ?? operatorius aprašymo null reikšmei pakeisti (0,5 t)
        // Bitinė operacija |= pritaikyta prioriteto priskirimui (1 t)
        public BaseTask(string taskName, string? taskDescripiton, DateTime dueDate, Priority priority)
        {
            this.id = giveId();
            this.taskName = taskName;
            this.taskDescription = taskDescripiton ?? (taskDescripiton="No description");
            this.dateCreated = DateTime.Now;
            this.dueDate = dueDate;
            this.isCompleted = false;
            this.priority |= priority;
            this.status = TaskStatus.InProgress;
        }

        protected BaseTask(string taskName, DateTime dueDate)
        {
            this.taskName = taskName;
            this.dueDate = dueDate;
        }

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }

        public string? TaskDescription
        {
            get { return taskDescription; }
            set { taskDescription = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        public bool IsCompleted
        {
            get { return isCompleted; }
            set { isCompleted = value; }
        }

        public Priority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public TaskStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public uint Id
        {
            get { return id; }
        }

        public abstract void markAsComplete();

        public static uint giveId()
        {
            return ++idCounter;
        }

        // Statusu atnaujinimui salygų tikrinimui pritaikytas switch su when raktažodžiu + salygos yra šablonų atitikimai (0.5 t + 1 t)
        public void updateStatus()
        {
            this.status = this switch
            {
                { DueDate: DateTime dueDate } when DateTime.Now.IsPastDueDate(dueDate) => TaskStatus.Late,
                { IsCompleted: true } => TaskStatus.Completed,
                _ => TaskStatus.InProgress
            };
        }

        // Sort funkcijai implementuotas IComparable interfeisas (0,5 t)
        public int CompareTo(BaseTask? other)
        {
            if (other == null)
            {
                return 1;
            }
            if (this.dueDate > other.DueDate)
            {
                return 1;
            }
            else if (this.dueDate < other.DueDate)
            {
                return -1;
            }
            else
            {
                if (this.priority > other.Priority)
                {
                    return 1;
                }
                else if (this.priority < other.Priority)
                {
                    return -1;
                }
                else
                {
                    if (this.taskName.CompareTo(other.TaskName) > 0)
                    {
                        return 1;
                    }
                    else if (this.taskName.CompareTo(other.TaskName) < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        // Equals metodo perkrovimui implementuotas IEquatable interfeisas, objektai lyginami pagal unikalų ID (0,5 t)
        public bool Equals(BaseTask? other)
        {
            if (other == null)
            {
                return false;
            }
            if (this.id == other.Id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
