using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
    public class Task
    {
        private string _description;
        private int _id;
        // private static List<Task> _instances = new List<Task> {};
        // changed w/database

        public Task (string Description, int Id = 0)
        {
            _description = Description;
            _id = Id;
            // _instances.Add(this);
            // _id = _instances.Count;  Changed w/database
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
            bool idEquality = (this.GetId() == newTask.GetId());
            bool descriptionEquality = (this.GetDescription() == newTask.GetDescription());
            return (idEquality && descriptionEquality);
          }
        }
        public string GetDescription()
        {
            return _description;
        }

        public void SetDescription(string newDescription)
        {
            _description = newDescription;
        }

        public int GetId()
        {
            return _id;
        }
        public static List<Task> GetAll()
        {
            // return _instances; Changed w/database
            List<Task> allTasks = new List<Task> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tasks;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
             int taskId = rdr.GetInt32(0);
             string taskDescription = rdr.GetString(1);
             Task newTask = new Task(taskDescription, taskId);
             allTasks.Add(newTask);
            }
            conn.Close();
            if (conn != null)
            {
               conn.Dispose();
            }
            return allTasks;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO `tasks` (`description`) VALUES (@TaskDescription);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@TaskDescription";
            description.Value = this._description;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }
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

        public static Task Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `tasks` WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int taskId = 0;
            string taskDescription = "";

            while (rdr.Read())
            {
                taskId = rdr.GetInt32(0);
                taskDescription = rdr.GetString(1);
            }

            Task foundTask= new Task(taskDescription, taskId);  // This line is new!

             conn.Close();
             if (conn != null)
             {
                 conn.Dispose();
             }

            return foundTask;  // This line is new!

        }
    }
}
