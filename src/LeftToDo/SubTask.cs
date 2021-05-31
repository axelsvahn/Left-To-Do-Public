namespace LeftToDo
{
    public class SubTask : Task
    {
        //Inherits everything from "Task", no attributes of its own.
        //This child class exists to make class structure clearer, since "Task" itself is abstract
        //It is stored in a list within each instance of ChecklistTask. 

        public SubTask(string description) : base(description)
        {
            //"description" is passed into base constructor
            //"isFinished" is initialized as "false" in base constructor
        }

        public override string ToString()
        {
            string toString = $"{Description}, ";
            if (isFinished)
            {
                toString += $"Finished: yes \n";
            }
            else if (!isFinished)
            {
                toString += $"Finished: no \n";
            }
            return toString;
        }
    }
}