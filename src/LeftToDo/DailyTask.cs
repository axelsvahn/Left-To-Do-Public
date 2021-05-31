namespace LeftToDo
{
    public class DailyTask : Task
    {
        //Inherits everything from "Task", no attributes of its own.
        //Exists to make class structure clearer, since "Task" itself is abstract

        public DailyTask(string description) : base(description)
        {
            //"description" is passed into base constructor
            //"isFinished" is initialized as "false" in base constructor
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}