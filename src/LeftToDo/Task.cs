namespace LeftToDo
{
    public abstract class Task
    {
        public string Description { get; set; }
        public bool isFinished { get; set; }

        public Task(string description)
        {
            Description = description;
            //All types of task are initially unfinished 
            isFinished = false;
        }

        public override string ToString()
        {
            string toString = $"Description: {Description} \n";
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