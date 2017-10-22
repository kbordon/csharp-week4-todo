using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ToDoList.Models;
using ToDoList;
using System;

namespace ToDoList.Tests
{

    [TestClass]
    public class TaskTests : IDisposable
    {
        public void Dispose()
        {
            Task.DeleteAll();
        }
        public TaskTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=todo_test;";
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
          //Arrange, Act
          int result = Task.GetAll().Count;

          //Assert
          Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_TaskList()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn");

          //Act
          testTask.Save();
          List<Task> result = Task.GetAll();
          List<Task> testList = new List<Task>{testTask};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Task()
        {
          // Arrange, Act
          Task firstTask = new Task("Mow the lawn");
          Task secondTask = new Task("Mow the lawn");

          // Assert
          Assert.AreEqual(firstTask, secondTask);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_Id()
        {
          //Arrange
          Task testTask = new Task("Mow the lawn");

          //Act
          testTask.Save();
          Task savedTask = Task.GetAll()[0];

          int result = savedTask.GetId();
          int testId = testTask.GetId();

          //Assert
          Assert.AreEqual(testId, result);
        }

    }
}
