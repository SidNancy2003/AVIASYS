using AVIASYS1.Models;
using Microsoft.Data.Sqlite;

namespace AVIASYS1.Services
{
	public class DatabaseService
	{
		private readonly string _connectionString = "Data Source=aviasys.db";

		public List<EmployeeModel> GetEmployees()
		{
			var employees = new List<EmployeeModel>();
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT Id, FullName, Login, Password, Role, WorkshopId FROM Employees";
			using var r = cmd.ExecuteReader();

			while (r.Read())
			{
				employees.Add(new EmployeeModel
				{
					Id = r.GetInt32(0),
					FullName = r.GetString(1),
					Login = r.GetString(2),
					Password = r.GetString(3),
					Role = r.GetString(4),
					WorkshopId = r.IsDBNull(5) ? null : r.GetInt32(5)
				});
			}
			return employees;
		}

		public EmployeeModel? GetEmployee(int id)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT Id, FullName, Login, Password, Role, WorkshopId FROM Employees WHERE Id = $id";
			cmd.Parameters.AddWithValue("$id", id);

			using var r = cmd.ExecuteReader();
			if (r.Read())
			{
				return new EmployeeModel
				{
					Id = r.GetInt32(0),
					FullName = r.GetString(1),
					Login = r.GetString(2),
					Password = r.GetString(3),
					Role = r.GetString(4),
					WorkshopId = r.IsDBNull(5) ? null : r.GetInt32(5)
				};
			}
			return null;
		}

		public void AddEmployee(EmployeeModel e)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = @"
                INSERT INTO Employees (FullName, Login, Password, Role, WorkshopId)
                VALUES ($FullName, $Login, $Password, $Role, $WorkshopId)";
			cmd.Parameters.AddWithValue("$FullName", e.FullName);
			cmd.Parameters.AddWithValue("$Login", e.Login);
			cmd.Parameters.AddWithValue("$Password", e.Password);
			cmd.Parameters.AddWithValue("$Role", e.Role);
			cmd.Parameters.AddWithValue("$WorkshopId", e.WorkshopId ?? (object)DBNull.Value);
			cmd.ExecuteNonQuery();
		}

		public void UpdateEmployee(EmployeeModel e)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = @"
                UPDATE Employees
                SET FullName = $FullName,
                    Login = $Login,
                    Password = $Password,
                    Role = $Role,
                    WorkshopId = $WorkshopId
                WHERE Id = $Id";
			cmd.Parameters.AddWithValue("$FullName", e.FullName);
			cmd.Parameters.AddWithValue("$Login", e.Login);
			cmd.Parameters.AddWithValue("$Password", e.Password);
			cmd.Parameters.AddWithValue("$Role", e.Role);
			cmd.Parameters.AddWithValue("$WorkshopId", e.WorkshopId ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("$Id", e.Id);
			cmd.ExecuteNonQuery();
		}

		public void DeleteEmployee(int id)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "DELETE FROM Employees WHERE Id = $id";
			cmd.Parameters.AddWithValue("$id", id);
			cmd.ExecuteNonQuery();
		}

		public List<WorkshopModel> GetWorkshops()
		{
			var list = new List<WorkshopModel>();
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT Id, Name, Description, ManagerId FROM Workshops";
			using var r = cmd.ExecuteReader();
			while (r.Read())
			{
				list.Add(new WorkshopModel
				{
					Id = r.GetInt32(0),
					Name = r.GetString(1),
					Description = r.IsDBNull(2) ? "" : r.GetString(2),
					ManagerId = r.IsDBNull(3) ? null : r.GetInt32(3)
				});
			}
			return list;
		}

		public WorkshopModel? GetWorkshop(int id)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT Id, Name, Description, ManagerId FROM Workshops WHERE Id = $id";
			cmd.Parameters.AddWithValue("$id", id);

			using var r = cmd.ExecuteReader();
			if (r.Read())
			{
				return new WorkshopModel
				{
					Id = r.GetInt32(0),
					Name = r.GetString(1),
					Description = r.IsDBNull(2) ? "" : r.GetString(2),
					ManagerId = r.IsDBNull(3) ? null : r.GetInt32(3)
				};
			}
			return null;
		}

		public void AddWorkshop(WorkshopModel w)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = @"
                INSERT INTO Workshops (Name, Description, ManagerId)
                VALUES ($Name, $Description, $ManagerId)";
			cmd.Parameters.AddWithValue("$Name", w.Name);
			cmd.Parameters.AddWithValue("$Description", w.Description ?? "");
			cmd.Parameters.AddWithValue("$ManagerId", w.ManagerId ?? (object)DBNull.Value);
			cmd.ExecuteNonQuery();
		}

		public void UpdateWorkshop(WorkshopModel w)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = @"
                UPDATE Workshops
                SET Name = $Name,
                    Description = $Description,
                    ManagerId = $ManagerId
                WHERE Id = $Id";
			cmd.Parameters.AddWithValue("$Name", w.Name);
			cmd.Parameters.AddWithValue("$Description", w.Description ?? "");
			cmd.Parameters.AddWithValue("$ManagerId", w.ManagerId ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("$Id", w.Id);
			cmd.ExecuteNonQuery();
		}

		public void DeleteWorkshop(int id)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "DELETE FROM Workshops WHERE Id = $id";
			cmd.Parameters.AddWithValue("$id", id);
			cmd.ExecuteNonQuery();
		}

		public List<TaskModel> GetTasks()
		{
			var list = new List<TaskModel>();
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT Id, Title, Description, AssignedEmployeeId, Status, CreatedDate, Deadline FROM Tasks";
			using var r = cmd.ExecuteReader();

			while (r.Read())
			{
				list.Add(new TaskModel
				{
					Id = r.GetInt32(0),
					Title = r.GetString(1),
					Description = r.IsDBNull(2) ? "" : r.GetString(2),
					AssignedEmployeeId = r.IsDBNull(3) ? null : r.GetInt32(3),
					Status = r.IsDBNull(4) ? "Новое" : r.GetString(4),
					CreatedDate = r.IsDBNull(5) ? DateTime.Now : r.GetDateTime(5),
					Deadline = r.IsDBNull(6) ? null : r.GetDateTime(6)
				});
			}
			return list;
		}

		public TaskModel? GetTask(int id)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "SELECT Id, Title, Description, AssignedEmployeeId, Status, CreatedDate, Deadline FROM Tasks WHERE Id = $id";
			cmd.Parameters.AddWithValue("$id", id);

			using var r = cmd.ExecuteReader();
			if (r.Read())
			{
				return new TaskModel
				{
					Id = r.GetInt32(0),
					Title = r.GetString(1),
					Description = r.IsDBNull(2) ? "" : r.GetString(2),
					AssignedEmployeeId = r.IsDBNull(3) ? null : r.GetInt32(3),
					Status = r.IsDBNull(4) ? "Новое" : r.GetString(4),
					CreatedDate = r.IsDBNull(5) ? DateTime.Now : r.GetDateTime(5),
					Deadline = r.IsDBNull(6) ? null : r.GetDateTime(6)
				};
			}
			return null;
		}

		public void AddTask(TaskModel t)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = @"
                INSERT INTO Tasks (Title, Description, AssignedEmployeeId, Status, Deadline)
                VALUES ($Title, $Description, $AssignedEmployeeId, $Status, $Deadline)";
			cmd.Parameters.AddWithValue("$Title", t.Title);
			cmd.Parameters.AddWithValue("$Description", t.Description ?? "");
			cmd.Parameters.AddWithValue("$AssignedEmployeeId", t.AssignedEmployeeId ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("$Status", t.Status ?? "Новое");
			cmd.Parameters.AddWithValue("$Deadline", t.Deadline ?? (object)DBNull.Value);
			cmd.ExecuteNonQuery();
		}

		public void UpdateTask(TaskModel t)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = @"
                UPDATE Tasks
                SET Title = $Title,
                    Description = $Description,
                    AssignedEmployeeId = $AssignedEmployeeId,
                    Status = $Status,
                    Deadline = $Deadline
                WHERE Id = $Id";
			cmd.Parameters.AddWithValue("$Title", t.Title);
			cmd.Parameters.AddWithValue("$Description", t.Description ?? "");
			cmd.Parameters.AddWithValue("$AssignedEmployeeId", t.AssignedEmployeeId ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("$Status", t.Status ?? "Новое");
			cmd.Parameters.AddWithValue("$Deadline", t.Deadline ?? (object)DBNull.Value);
			cmd.Parameters.AddWithValue("$Id", t.Id);
			cmd.ExecuteNonQuery();
		}

		public void DeleteTask(int id)
		{
			using var connection = new SqliteConnection(_connectionString);
			connection.Open();

			var cmd = connection.CreateCommand();
			cmd.CommandText = "DELETE FROM Tasks WHERE Id = $id";
			cmd.Parameters.AddWithValue("$id", id);
			cmd.ExecuteNonQuery();
		}
	}
}
