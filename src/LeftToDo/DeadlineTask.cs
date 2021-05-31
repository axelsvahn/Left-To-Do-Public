using System;

namespace LeftToDo
{
    public class DeadlineTask : Task
    {
        public int DaysLeft { get; set; }

        public DeadlineTask(string description, int daysleft) : base(description)
        {
            if (daysleft > 999999 || daysleft < 0)
            {
                //prevents ArgumentOutofRanceException and negative values 
                DaysLeft = 0;
            }
            else
            {
                DaysLeft = daysleft;
            }
        }
        private string CalculateDeadline()
        {
            DateTime deadlineDate = DateTime.Today.AddDays(DaysLeft); //calculate date from today
            string deadlineString = deadlineDate.ToString("dd MMM yyyy"); //date formatting
            return deadlineString;
        }

        public override string ToString()
        {
            return base.ToString()
            + $"Deadline: {CalculateDeadline()}\n"
            + $"Days left: {DaysLeft}\n";
        }
    }
}