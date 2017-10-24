using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ToDoList.Models;

namespace ToDoList.Tests
{

    [TestClass]
    public class TaskTests : IDisposable
    {
        public TaskTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
        }
        public void Dispose()
        {
          Task.DeleteAll();
          Category.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForSameDescription_Task()
        {
          //Arrange, Act
          Task firstTask = new Task("Mow the lawn", 1);
          Task secondTask = new Task("Mow the lawn", 1);

          //Assert
          Assert.AreEqual(firstTask, secondTask);
        }

        [TestMethod]
        public void Save_SavesTaskToDatabase_TaskList()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn", 1);
          testTask.Save();

          //Act
          List<Task> result = Task.GetAll();
          List<Task> testList = new List<Task>{testTask};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }
       [TestMethod]
        public void Save_DatabaseAssignsIdToObject_Id()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn", 1);
          testTask.Save();

          //Act
          Task savedTask = Task.GetAll()[0];

          int result = savedTask.GetId();
          int testId = testTask.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsTaskInDatabase_Task()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn", 1);
          testTask.Save();

          //Act
          Task foundTask = Task.Find(testTask.GetId());

          //Assert
          Assert.AreEqual(testTask, foundTask);
        }

        [TestMethod]
        public void GetTasks_RetrievesAllTasksWithCategory_TaskList()
        {
          Category testCategory = new Category("Household chores");
          testCategory.Save();

          Task firstTask = new Task("Mow the lawn", testCategory.GetId());
          firstTask.Save();
          Task secondTask = new Task("Do the dishes", testCategory.GetId());
          secondTask.Save();


          List<Task> testTaskList = new List<Task> {firstTask, secondTask};
          List<Task> resultTaskList = testCategory.GetTasks();

          CollectionAssert.AreEqual(testTaskList, resultTaskList);
        }
    }
}
