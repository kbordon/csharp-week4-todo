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
          DateTime newDateTime = new DateTime(2016,12,31);
          Task firstTask = new Task("Mow the lawn", 1, newDateTime, newDateTime.ToString());
          Task secondTask = new Task("Mow the lawn", 1, newDateTime, newDateTime.ToString());

          //Assert
          Assert.AreEqual(firstTask, secondTask);
        }

        [TestMethod]
        public void Save_SavesTaskToDatabase_TaskList()
        {
          //Arrange
          DateTime newDateTime = new DateTime(2016,12,31);
          Task testTask = new Task("Mow the lawn", 1, newDateTime, newDateTime.ToString());
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
          DateTime newDateTime = new DateTime(2016,12,31);
          Task testTask = new Task("Mow the lawn", 1, newDateTime, newDateTime.ToString());
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
          DateTime newDateTime = new DateTime(2016,12,31);
          Task testTask = new Task("Mow the lawn", 1, newDateTime, newDateTime.ToString());
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

          DateTime newDateTime = new DateTime(2016,12,31);
          DateTime newSecondDateTime = new DateTime(2017,1,1);
          Task firstTask = new Task("Mow the lawn", testCategory.GetId(), newDateTime, newDateTime.ToString());
          firstTask.Save();
          Task secondTask = new Task("Do the dishes", testCategory.GetId(), newSecondDateTime, newSecondDateTime.ToString());
          secondTask.Save();


          List<Task> testTaskList = new List<Task> {firstTask, secondTask};
          List<Task> resultTaskList = testCategory.GetTasks();

          CollectionAssert.AreEqual(testTaskList, resultTaskList);
        }

        [TestMethod]
        public void GetTasks_MakeSureListOrderedByDate_TaskList()
        {
          Category testCategory = new Category("Household chores");
          testCategory.Save();

          DateTime newDateTime = new DateTime(2016,12,31);
          DateTime newSecondDateTime = new DateTime(2015,1,1);
          Task firstTask = new Task("Mow the lawn", testCategory.GetId(), newDateTime, newDateTime.ToString());
          firstTask.Save();
          Task secondTask = new Task("Do the dishes", testCategory.GetId(), newSecondDateTime, newSecondDateTime.ToString());
          secondTask.Save();

          List<Task> testTaskList = new List<Task> {secondTask, firstTask};
          List<Task> resultTaskList = testCategory.GetTasks();

          CollectionAssert.AreEqual(testTaskList, resultTaskList);
        }
    }
}
