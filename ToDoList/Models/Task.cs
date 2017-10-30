using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
    public class Task
    {
        private string _description;
        private int _id;
        private DateTime _dueDate;
        private bool _completed = false;

        public Task(string description, DateTime dueDate, int id = 0)
        {
            _description = description;
            _dueDate = dueDate;
            _id = id;
        }

        public void SetTaskCompleted()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"UPDATE tasks SET completed = true WHERE id = @searchId;";

          MySqlParameter searchId = new MySqlParameter();
          searchId.ParameterName = "@searchId";
          searchId.Value = this._id;
          cmd.Parameters.Add(searchId);

          cmd.ExecuteNonQuery();
          this._completed = true;

          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
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
             return (idEquality && descriptionEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetDescription().GetHashCode();
        }

        public DateTime GetDueDate()
        {
          return _dueDate;
        }

        public string GetDescription()
        {
            return _description;
        }
        public int GetId()
        {
            return _id;
        }
        public bool GetCompleted()
        {
          return _completed;
        }

        public List<Category> GetCategories()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT categories.* FROM tasks JOIN categories_tasks ON (tasks.id = categories_tasks.task_id) JOIN categories ON (categories_tasks.category_id = categories.id) WHERE tasks.id = @taskId;";

          // @"SELECT category_id FROM categories_tasks WHERE task_id = @taskId;";

          MySqlParameter taskIdParameter = new MySqlParameter();
          taskIdParameter.ParameterName = "@taskId";
          taskIdParameter.Value = _id;
          cmd.Parameters.Add(taskIdParameter);

          var rdr = cmd.ExecuteReader() as MySqlDataReader;

          List<Category> categories = new List<Category> {};
          while(rdr.Read())
          {
            int returnId = rdr.GetInt32(0);
            string returnName = rdr.GetString(1);
            Category returnedCategory = new Category(returnName, returnId);
            categories.Add(returnedCategory);
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return categories;
        }

        public void AddCategory(Category newCategory)
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO categories_tasks (category_id, task_id) VALUES (@CategoryId, @TaskId);";

          MySqlParameter category_id = new MySqlParameter();
          category_id.ParameterName = "@CategoryId";
          category_id.Value = newCategory.GetId();
          cmd.Parameters.Add(category_id);

          MySqlParameter task_id = new MySqlParameter();
          task_id.ParameterName = "@TaskId";
          task_id.Value = _id;
          cmd.Parameters.Add(task_id);

          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tasks (description, due_date) VALUES (@description, @due_date);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@description";
            description.Value = this._description;
            cmd.Parameters.Add(description);

            MySqlParameter dueDate = new MySqlParameter();
            dueDate.ParameterName = "@due_date";
            dueDate.Value = this._dueDate.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add(dueDate);

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
              DateTime taskDueDate = DateTime.Parse(rdr.GetString(2));
              Task newTask = new Task(taskDescription, taskDueDate, taskId);
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
            DateTime taskDueDate = new DateTime();

            while(rdr.Read())
            {
              taskId = rdr.GetInt32(0);
              taskName = rdr.GetString(1);
              taskDueDate = DateTime.Parse(rdr.GetString(2));

            }
            Task newTask = new Task(taskName, taskDueDate, taskId);
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

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tasks WHERE id = @TaskId; DELETE FROM categories_tasks WHERE task_id = @TaskId;";

            MySqlParameter taskIdParameter = new MySqlParameter();
            taskIdParameter.ParameterName = "@TaskId";
            taskIdParameter.Value = this.GetId();

            cmd.Parameters.Add(taskIdParameter);
            cmd.ExecuteNonQuery();

            if (conn != null)
            {
              conn.Close();
            }
        }
    }
}
