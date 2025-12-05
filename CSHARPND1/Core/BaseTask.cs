using CSHARPND1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
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

        public void updateStatus()
        {
            this.status = this switch
            {
                { DueDate: DateTime dueDate } when DateTime.Now.IsPastDueDate(dueDate) => TaskStatus.Late,
                { IsCompleted: true } => TaskStatus.Completed,
                _ => TaskStatus.InProgress
            };
        }

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
