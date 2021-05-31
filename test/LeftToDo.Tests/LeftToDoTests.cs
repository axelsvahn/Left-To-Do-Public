using Xunit;
using System.Collections.Generic;

namespace LeftToDo.Tests
{
    public class LeftToDoTests
    {
        [Fact]
        public void CanAddDailyTaskToList()
        {
            //arrange:
            Storage testStorage1 = new Storage();
            string description = "Today I am going to test if this works";

            //act:
            testStorage1.ConstructDailyTask(description);

            //assert:
            Assert.Equal(description, testStorage1.dailyTasks[0].Description);
        }

        [Fact]
        public void CanMarkTaskAsFinished()
        {
            //This is not an optimal test, because it does not use a straightforward method call as the "act".
            //The logic for marking tasks as "finished" is too complex since it relies on user input, so it cannot easily
            //be reduced to a testable method in a way that makes sense in terms of control flow in the actual application

            //arrange:
            Storage testStorage2 = new Storage();
            string description = "test";
            DailyTask testDailyTask = new DailyTask(description);
            testStorage2.dailyTasks.Add(testDailyTask);

            //act:
            testStorage2.dailyTasks[0].isFinished = true;

            //assert:
            Assert.True(testStorage2.dailyTasks[0].isFinished);
        }

        [Fact]
        public void CanArchiveFinishedTasks()
        {
            //arrange:
            Storage testStorage3 = new Storage();
            string willNotBeAdded = "Will not be added";

            //isFinished is false by default, so this task will not be added to archive
            testStorage3.ConstructDailyTask(willNotBeAdded);

            //but this one will
            string willBeAdded = "Will be added";
            testStorage3.ConstructDailyTask(willBeAdded);
            testStorage3.dailyTasks[1].isFinished = true;

            //also tests if deadline tasks can be added:
            int daysleft = 10;
            willBeAdded = "Will also be added";
            testStorage3.ConstructDeadlineTask(willBeAdded, daysleft);
            testStorage3.deadlineTasks[0].isFinished = true;

            //act:
            testStorage3.AddFinishedToArchive(testStorage3.dailyTasks);
            testStorage3.AddFinishedToArchive(testStorage3.deadlineTasks);

            //assert:
            Assert.Collection(testStorage3.archivedTasks,
            item => Assert.Equal("Will be added", testStorage3.archivedTasks[0].Description),
            item => Assert.Equal("Will also be added", testStorage3.archivedTasks[1].Description));
        }

        [Fact]
        public void CanAddDeadlineTaskToList()
        {
            //arrange:
            Storage testStorage4 = new Storage();
            string description = "Become good at programming";
            int deadline = 10000;
            //act:
            testStorage4.ConstructDeadlineTask(description, deadline);
            //assert:
            Assert.Equal(description, testStorage4.deadlineTasks[0].Description);
        }

        [Fact]
        public void CanAddChecklistTaskToList()
        {
            //arrange:
            Storage testStorage5 = new Storage();
            string description = "test main desgcription";
            List<Task> fakeSubTaskList = PopulateFakeSubTaskList();

            //act:
            testStorage5.ConstructChecklistTask(description, fakeSubTaskList);

            //assert:
            System.Console.WriteLine("Hej hopp");
            Assert.Equal(description, testStorage5.checklistTasks[0].Description);

        }

        //fake method with hard coded values used to set up test
        public List<Task> PopulateFakeSubTaskList()
        {
            List<Task> returnList = new List<Task>();
            string subDesc = "test subtask description";
            returnList.Add(new SubTask(subDesc));
            subDesc = "another test subtask description";
            returnList.Add(new SubTask(subDesc));

            return returnList;
        }
    }
}
