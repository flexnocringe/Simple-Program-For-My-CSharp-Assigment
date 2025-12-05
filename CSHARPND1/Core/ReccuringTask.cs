using CSHARPND1.Exceptions;
using CSHARPND1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
    sealed class ReccuringTask : BaseTask, IFormattable, ICloneable
    {
        private DateTime nextDueDate;
        private int reccurenceIntervalDays;

        public int ReccurenceIntervalDays
        {
            get { return reccurenceIntervalDays; }
            set { reccurenceIntervalDays = value; }
        }
        public ReccuringTask(string taskName, string? taskDescripiton, DateTime dueDate, Priority priority, int reccurenceIntervalDays) : base(taskName, taskDescripiton, dueDate, priority)
        {
            if(reccurenceIntervalDays <= 0)
            {
                throw new InvalidTaskActionException("Reccurence interval must be greater than zero.");
            }
            this.reccurenceIntervalDays = reccurenceIntervalDays;
            this.nextDueDate = this.DueDate.AddDays(reccurenceIntervalDays);
        }

        public DateTime NextDueDate
        {
            get { return this.nextDueDate; }
            set { this.nextDueDate = value; }
        }

        public void updateNextDueDate()
        {
            this.DueDate = nextDueDate;
            this.nextDueDate = this.DueDate.AddDays(reccurenceIntervalDays);
        }

        public override void markAsComplete()
        {
            IsCompleted = true;
            this.nextDueDate = this.DueDate;
            updateStatus();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch(format)
            {
                case "detailed":
                    return $"[Reccuring Task]\nid: {id}\nName: {taskName}\nDescription: {taskDescription}\nCreated On: {dateCreated.FormatLithuanian()}\nDue Date: {dueDate.FormatLithuanian()}\nNext Due Date: {nextDueDate}\nPriority: {priority}\nStatus: {status}\nReccurence Interval (days): {reccurenceIntervalDays}";
                case "status":
                    return this.status.ToString();
                default:
                    return $"[Reccuting Task] {taskName} - Due: {dueDate.ToShortDateString()}- Repeats every {reccurenceIntervalDays} days - Priority: {priority}";
            }
        }

        public object Clone()
        {
            return new ReccuringTask(this.TaskName, this.TaskDescription, this.DueDate, this.Priority, this.ReccurenceIntervalDays)
            {
                id = this.Id,
                isCompleted = this.IsCompleted,
                status = this.Status,
                dateCreated = this.dateCreated,
                nextDueDate = this.NextDueDate
            };
        }
    }
}
