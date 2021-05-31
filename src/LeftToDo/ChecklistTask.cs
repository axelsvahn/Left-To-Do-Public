using System.Collections.Generic;

namespace LeftToDo
{
    public class ChecklistTask : Task
    {
        private List<Task> subTasks = new List<Task>();

        public List<Task> SubTasks
        {
            set { subTasks = value; }
            get { return subTasks; }
        }

        public ChecklistTask(string description, List<Task> subtasks) : base(description)
        {
            //"description" is passed into base constructor
            //"isFinished" is initialized as "false" in base constructor
            //"subtasks" is the input parameter, "SubTasks" is the property corresponding to the private list, and
            //"subTasks" is the private property.
            subTasks = subtasks;
        }

        private string ListSubTasks()
        {
            string output = "";
            int count = 1;
            foreach (Task subtask in subTasks)
            {
                output += $"Subtask {count}: {subtask.ToString()}";
                count++;
            }

            return output;
        }

        public override string ToString()
        {
            return base.ToString() + ListSubTasks();
        }
    }
}