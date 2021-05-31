using System;
using System.Threading;

namespace LeftToDo
{
    public class Menu
    {
        public void RunMenu()
        {
            //creates instance of storage class - having it be static would make things less flexible.
            Storage myStorage = new Storage();
            bool run = true;
            while (run)
            {
                WriteMenu();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        //List non-archived tasks
                        myStorage.ListDailyTasks();
                        break;

                    case "2":
                        //Add new daily task 
                        myStorage.InputDailyTask();
                        break;

                    case "3":
                        //Add new task with deadline  
                        myStorage.InputDeadlineTask();
                        break;

                    case "4":
                        //Add new task with checklist
                        myStorage.AddChecklistTask();
                        break;

                    case "5":
                        //Archive all finished tasks
                        myStorage.ArchiveFinishedTasks();
                        break;

                    case "6":
                        //List all archived tasks
                        myStorage.ListArchivedTasks();
                        break;

                    case "7":
                        //quit
                        run = false;
                        Console.Clear();
                        System.Console.WriteLine("Thank you for using \"Left To Do\"!");
                        break;

                    default:
                        Console.WriteLine("Input error! Please try again.");
                        Thread.Sleep(750);
                        Console.Clear();
                        continue;
                }
            }
        }
        private void WriteMenu()
        {
            //prints menu text
            Console.Clear();
            Console.WriteLine("--Left To Do--\n");
            Console.WriteLine("Input a number and press \"enter\" to pick an alternative.\n");
            Console.WriteLine("[1] List all non-archived tasks\n");
            Console.WriteLine("[2] Add new task for today");
            Console.WriteLine("[3] Add new task with deadline");
            Console.WriteLine("[4] Add new complex task with checklist\n");
            Console.WriteLine("[5] Archive all finished tasks");
            Console.WriteLine("[6] List all archived tasks\n");
            Console.WriteLine("[7] Quit");
            Console.Write("\nWrite here: ");
        }
        public static void PressAnyKey()
        {
            //handles return to menu; reduces repetition of code
            Console.WriteLine("Press any key to return to menu.");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}