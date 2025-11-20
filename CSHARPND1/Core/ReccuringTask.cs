using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHARPND1.Core
{
    sealed class ReccuringTask : BaseTask, IFormattable
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
                    return $"[Reccuring Task]\nid: {id}\nName: {taskName}\nDescription: {taskDescription}\nCreated On: {dateCreated}\nDue Date: {dueDate}\nNext Due Date: {nextDueDate}\nPriority: {priority}\nStatus: {status}\nReccurence Interval (days): {reccurenceIntervalDays}";
                case "status":
                    return this.status.ToString();
                default:
                    return $"[Reccuting Task] {taskName} - Due: {dueDate.ToShortDateString()}- Repeats every {reccurenceIntervalDays} days - Priority: {priority}";
            }
        }

    }
}
