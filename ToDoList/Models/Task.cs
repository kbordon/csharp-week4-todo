using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
    public class Task
    {
        private string _description;
        private int _id;
        private int _categoryId;
        private DateTime _dueDate;
        private string _stringDueDate;

        public Task(string description, int categoryId, DateTime dueDate, string stringDueDate, int id = 0)
        {
            _description = description;
            _categoryId = categoryId;
            _dueDate = dueDate;
            _stringDueDate = stringDueDate;
            _id = id;
        }

        public override bool Equals(System.Object otherTask)
        {
          if (!(otherTask is Task))
          {
            return false;
          }
          else
          {
             Task newTask = (Task) otherTask;
             bool idEquality = this.GetId() == newTask.GetId();
             bool descriptionEquality = this.GetDescription() == newTask.GetDescription();
             bool categoryEquality = this.GetCategoryId() == newTask.GetCategoryId();
             return (idEquality && descriptionEquality && categoryEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetDescription().GetHashCode();
        }

        public string GetDueDate()
        {
          return _stringDueDate;
        }

        public string GetDescription()
        {
            return _description;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetCategoryId()
        {
            return _categoryId;
        }
        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tasks (description, category_id, due_date, due_date_string) VALUES (@description, @category_id, @due_date, @due_date_string);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@description";
            description.Value = this._description;
            cmd.Parameters.Add(description);

            MySqlParameter categoryId = new MySqlParameter();
            categoryId.ParameterName = "@category_id";
            categoryId.Value = this._categoryId;
            cmd.Parameters.Add(categoryId);

            MySqlParameter dueDate = new MySqlParameter();
            dueDate.ParameterName = "@due_date";
            dueDate.Value = this._dueDate;
            cmd.Parameters.Add(dueDate);

            MySqlParameter dueDateString = new MySqlParameter();
            dueDateString.ParameterName = "@due_date_string";
            dueDateString.Value = this._stringDueDate;
            cmd.Parameters.Add(dueDateString);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }



        public static List<Task> GetAll()
        {
            List<Task> allTasks = new List<Task> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tasks;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int taskId = rdr.GetInt32(0);
              string taskDescription = rdr.GetString(1);
              int taskCategoryId = rdr.GetInt32(2);
              string taskDueDate = rdr.GetString(4);
              DateTime dummyDateTime = new DateTime();
              Task newTask = new Task(taskDescription, taskCategoryId, dummyDateTime, taskDueDate, taskId);
              allTasks.Add(newTask);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Console.WriteLine(allTasks[0].GetDueDate());
            return allTasks;
        }
        public static Task Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tasks WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int taskId = 0;
            string taskName = "";
            int taskCategoryId = 0;
            string taskDueDate = "";
            DateTime dummyDateTime = new DateTime();

            while(rdr.Read())
            {
              taskId = rdr.GetInt32(0);
              taskName = rdr.GetString(1);
              taskCategoryId = rdr.GetInt32(2);
              taskDueDate = rdr.GetString(4);

            }
            Task newTask = new Task(taskName, taskCategoryId, dummyDateTime, taskDueDate, taskId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newTask;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tasks;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
