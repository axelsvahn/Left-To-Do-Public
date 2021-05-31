using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace LeftToDo
{
    ///<summary>
    ///stores stuff
    ///</summary>
    public class Storage
    {
        //Lists that contain each basic subtype of Task, except for SubTask, which is stored inside
        //instances of ChecklistTask.
        //All lists are typed as the base type "Task"
        public List<Task> dailyTasks = new List<Task>();
        public List<Task> archivedTasks = new List<Task>();
        public List<Task> deadlineTasks = new List<Task>();
        public List<Task> checklistTasks = new List<Task>();

        public void ListDailyTasks()
        {
            Console.Clear();
            System.Console.WriteLine("Your current tasks are as follows:\n");

            //Calls method to loop through each list separately and prints the content

            if (dailyTasks.Count > 0)
            {
                System.Console.WriteLine("---Tasks for today---");
                PrintLoop(dailyTasks);
            }

            if (deadlineTasks.Count > 0)
            {
                System.Console.WriteLine("---Tasks with deadlines---");
                PrintLoop(deadlineTasks);
            }

            if (checklistTasks.Count > 0)
            {
                System.Console.WriteLine("---Tasks with checklists---");
                PrintLoop(checklistTasks);
            }
            //gives user option to marks tasks as finished; handled in other method
            //along with return to menu

            if (dailyTasks.Count == 0 && deadlineTasks.Count == 0 && checklistTasks.Count == 0)
            {
                System.Console.WriteLine("None! You'd better add some tasks.");
                Menu.PressAnyKey();
            }
            else
            {
                System.Console.WriteLine("Would you like to mark one or more tasks as finished?" +
                " \nPress Y and \"enter\" to begin marking tasks as finished." +
                " \nIf not, press \"enter\" to return to menu.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "Y":
                    case "y":
                        MarkAsFinished();
                        break;
                    default:
                        break;
                }
            }
        }
        ///<summary>
        ///prints stuff as a loop
        ///</summary>
        public void PrintLoop(List<Task> list)
        {
            int count = 1;
            foreach (Task task in list)
            {
                System.Console.WriteLine($"{count}: {task.ToString()}");
                count++;
            }
        }

        ///<summary>
        ///Parses stuff
        ///</summary>
        /// <returns>an int</returns>
        public int ParseIntegerInput()
        {
            int input = 0;
            bool inputSuccess = false;
            while (!inputSuccess)
            {
                Console.Write("Please input the amount as an integer:");
                inputSuccess = int.TryParse(Console.ReadLine(), out input);
                if (!inputSuccess)
                    System.Console.WriteLine("Input was not an integer!");
            }
            return input;
        }

        public void InputDailyTask()
        {
            //introduction
            Console.Clear();
            Console.WriteLine($"Hello {Environment.UserName}!\n\nWhere Do You Want To Go\u00A9" +
            " ... uh ... I mean ... what do you want to do today?\n");

            //data input and construction of object
            Console.Write("Please describe your task:");
            string input = Console.ReadLine();
            ConstructDailyTask(input);

            //user feedback, return to menu
            System.Console.WriteLine($"You have added the following task" +
            $" to your list of things to do today:\n{dailyTasks[dailyTasks.Count - 1].ToString()}");

            //return to menu
            Menu.PressAnyKey();
        }

        public void ConstructDailyTask(string input)
        {
            //separate user input from construction for easier testing
            dailyTasks.Add(new DailyTask(input));
        }

        public void ArchiveFinishedTasks()
        {
            Console.Clear();

            //calls external methods separately

            AddFinishedToArchive(dailyTasks);
            RemoveFinishedTasks(dailyTasks);

            AddFinishedToArchive(deadlineTasks);
            RemoveFinishedTasks(deadlineTasks);

            AddFinishedToArchive(checklistTasks);
            RemoveFinishedTasks(checklistTasks);

            //feedback and return to menu
            System.Console.WriteLine("All your finished tasks (if any) have now been archived!");
            Menu.PressAnyKey();
        }

        public void AddFinishedToArchive(List<Task> list)
        {
            //the method reacts to the top-level isFinished in each ChecklistTask, so no special handling
            //is needed

            foreach (var task in list.ToList())
            {
                if (task.isFinished)
                    archivedTasks.Add(task);
            }
        }

        public void RemoveFinishedTasks(List<Task> list)
        {
            // ToList() and "using System.Linq" is needed to avoid "System.InvalidOperationException"
            //because the size of the list changes during iteration

            foreach (var task in list.ToList())
            {
                if (task.isFinished)
                    list.Remove(task);
            }
        }

        public void ListArchivedTasks()
        {
            Console.Clear();
            if (archivedTasks.Count == 0)
            {
                System.Console.WriteLine("You have no archived tasks.");
                Menu.PressAnyKey();
            }
            else
            {
                //loops through the list four times and displays content by runtime type
                //not the best solution; implemented due to deadline

                System.Console.WriteLine("---Archived daily tasks---\n");
                int count = 1;
                foreach (Task task in archivedTasks)
                {
                    if (task.GetType() == typeof(DailyTask))
                    {
                        System.Console.WriteLine($"{count}: {task.ToString()}");
                        count++;
                    }
                }

                System.Console.WriteLine("---Archived deadline tasks---\n");
                count = 1;
                foreach (Task task in archivedTasks)
                {
                    if (task.GetType() == typeof(DeadlineTask))
                    {
                        System.Console.WriteLine($"{count}: {task.ToString()}");
                        count++;
                    }
                }

                System.Console.WriteLine("---Archived checklist tasks---\n");
                count = 1;
                foreach (Task task in archivedTasks)
                {
                    if (task.GetType() == typeof(ChecklistTask))
                    {
                        System.Console.WriteLine($"{count}: {task.ToString()}");
                        count++;
                    }
                }
                //returns to menu
                Menu.PressAnyKey();
            }
        }

        public void InputDeadlineTask()
        {
            //introduction
            Console.Clear();
            Console.WriteLine("You have chosen to add a task with a set deadline.");

            //description input
            Console.Write("Please describe your task:");
            string description = Console.ReadLine();

            Console.WriteLine("How many days are left to complete the task?");

            //days left input
            int daysLeft = ParseIntegerInput();

            //construct object instance and add to list
            ConstructDeadlineTask(description, daysLeft);

            //user feedback, return to menu
            Console.WriteLine($"\nYou have added the following task with a set deadline:\n{deadlineTasks[deadlineTasks.Count - 1].ToString()}");
            Menu.PressAnyKey();
        }

        ///<summary>
        ///separates user input from construction for easier testing
        ///</summary>
        public void ConstructDeadlineTask(string description, int daysLeft)
        {
            deadlineTasks.Add(new DeadlineTask(description, daysLeft));
        }

        public void AddChecklistTask()
        {
            //introduction
            Console.Clear();
            Console.WriteLine("You have chosen to add a task with a checklist containing several subtasks.");

            //description input
            Console.Write("Please provide an overall description for the task:");
            string description = Console.ReadLine();

            //populate local list of subtasks and construct checklist task
            System.Console.WriteLine("How many subtasks would you like to include?");
            int subTaskAmount = ParseIntegerInput();

            if (subTaskAmount < 1) //avoids errors
            {
                System.Console.WriteLine("Invalid input. The number of subtasks has been set to 1.");
                subTaskAmount = 1;
            }

            //using an array is easy way of getting a fixed count, so no "would you like to add another" etc.
            Task[] inputArray = new Task[subTaskAmount];
            List<Task> inputList = PopulateSubTaskList(inputArray);
            ConstructChecklistTask(description, inputList);

            //user feedback, return to menu
            Console.Clear();
            Console.WriteLine($"\nYou have added the following checklist task:\n{checklistTasks[checklistTasks.Count - 1].ToString()}");
            Menu.PressAnyKey();
        }

        public List<Task> PopulateSubTaskList(Task[] inputarr)
        {
            List<Task> returnList = new List<Task>();
            int count = 1;
            while (count < inputarr.Length + 1)
            {
                Console.Write($"Please describe subtask {count}:");
                string description = Console.ReadLine();
                returnList.Add(new SubTask(description));
                count++;
            }
            return returnList;
        }

        public void ConstructChecklistTask(string description, List<Task> inputlist)
        {
            //separates user input from construction for easier testing
            checklistTasks.Add(new ChecklistTask(description, inputlist));
        }

        public void MarkAsFinished()
        {
            //submenu to mark tasks as finished, called at the end of ListDailyTasks()
            //since this is a submenu, should perhaps be part of "menu" class due to separation of concerns, but is here
            //to reduce coupling between "Menu" and "Storage".
            //calls FinishedLooper() to handle the logic of looping through lists

            bool loop = true;
            while (loop)
            {
                Console.Clear();
                System.Console.WriteLine("Which type of task would you like to mark as finished?");
                System.Console.WriteLine("[1] One of today's tasks");
                System.Console.WriteLine("[2] A deadline task");
                System.Console.WriteLine("[3] A checklist task");
                System.Console.WriteLine("[4] None");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        FinishedLooper(dailyTasks);
                        break;
                    case "2":
                        FinishedLooper(deadlineTasks);
                        break;
                    case "3":
                        FinishedLooper(checklistTasks);
                        break;
                    case "4":
                        loop = false;
                        System.Console.WriteLine("Returning to menu...");
                        Thread.Sleep(1500);
                        break;
                    default:
                        System.Console.WriteLine("Invalid input!");
                        Thread.Sleep(1500);
                        break;
                }
            }
        }

        public void FinishedLooper(List<Task> list)
        {
            //loops through lists and allows user to mark tasks as finished; called by MarkAsFinished()
            if (list.Count == 0)
            {
                //if there are no non-archived tasks of this type
                System.Console.WriteLine("There are no active tasks of this type!");
                Thread.Sleep(1500);
            }
            else //if there are non-archived tasks
            {
                var count = 1;
                foreach (Task task in list)
                {
                    Console.Clear();
                    System.Console.WriteLine($"{count}: {task.ToString()}");
                    count++;
                    if (task.GetType() == typeof(ChecklistTask) && !task.isFinished)
                    {
                        //Adding another method reduces the complexity of FinishedLooper(), because behavior of ChecklistTask is different
                        //we have to cast it in order to use internal subtask list because it is not part of the base class "List"
                        LoopChecklistTask((ChecklistTask)task);
                    }
                    else if (!task.isFinished)
                    {
                        //ask if user would like to mark task as finished
                        FinishDialogue(task);
                    }
                    else //if a task is already finished
                    {
                        System.Console.WriteLine($"\"{task.Description}\" is already finished.");
                        Thread.Sleep(1500);
                    }
                }
            }
        }
        public void FinishDialogue(Task task)
        {
            System.Console.WriteLine($"Would you like to mark \"{task.Description}\" as finished? Type Y and press enter for Yes or press enter for No.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "Y":
                case "y":
                    task.isFinished = true;
                    System.Console.WriteLine($"\"{task.Description}\" is now marked as finished!");
                    Thread.Sleep(1500);
                    break;
                default:
                    System.Console.WriteLine($"\"{task.Description}\" will not be marked as finished.");
                    Thread.Sleep(1500);
                    break;
            }
        }
        public void LoopChecklistTask(ChecklistTask ChecklistTask)
        {
            foreach (Task subTask in ChecklistTask.SubTasks)
            {
                if (!subTask.isFinished)
                {
                    FinishDialogue(subTask);
                }
                else
                {
                    System.Console.WriteLine($"\"{subTask.Description}\" is already finished!");
                    Thread.Sleep(1500);
                }
            }
            //check if all are finished, if so, mark main task as finished

            bool allFinished = true;
            foreach (Task subTask in ChecklistTask.SubTasks)
            {
                if (!subTask.isFinished)
                    allFinished = false;
            }
            if (allFinished)
            {
                Console.Clear();
                System.Console.WriteLine($"All subtasks in \"{ChecklistTask.Description}\" are finished!");
                System.Console.WriteLine($"This means that \"{ChecklistTask.Description}\" has been completed! \nWell done, {Environment.UserName}!");
                ChecklistTask.isFinished = true;
                System.Console.WriteLine("Press any key to return to submenu.");
                Console.ReadKey(true);
            }
        }
    }
}