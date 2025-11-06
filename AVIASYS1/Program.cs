using AVIASYS1.Services;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddSingleton<DatabaseService>();

var app = builder.Build();

InitializeDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapRazorPages();
app.Run();

void InitializeDatabase()
{
	using var connection = new SqliteConnection("Data Source=aviasys.db");
	connection.Open();

	var command = connection.CreateCommand();
	command.CommandText = "PRAGMA foreign_keys = ON;";
	command.ExecuteNonQuery();

	command.CommandText = @"
	CREATE TABLE IF NOT EXISTS Workshops (
		Id INTEGER PRIMARY KEY AUTOINCREMENT,
		Name TEXT NOT NULL,
		Description TEXT,
		ManagerId INTEGER,
		FOREIGN KEY (ManagerId) REFERENCES Employees(Id) ON DELETE SET NULL
	)";
	command.ExecuteNonQuery();

	command.CommandText = @"
	CREATE TABLE IF NOT EXISTS Employees (
		Id INTEGER PRIMARY KEY AUTOINCREMENT,
		FullName TEXT NOT NULL,
		Login TEXT NOT NULL UNIQUE,
		Password TEXT NOT NULL,
		Role TEXT NOT NULL,
		WorkshopId INTEGER,
		FOREIGN KEY (WorkshopId) REFERENCES Workshops(Id) ON DELETE SET NULL
	)";
	command.ExecuteNonQuery();

	command.CommandText = @"
	CREATE TABLE IF NOT EXISTS Tasks (
		Id INTEGER PRIMARY KEY AUTOINCREMENT,
		Title TEXT NOT NULL,
		Description TEXT,
		AssignedEmployeeId INTEGER,
		Status TEXT DEFAULT 'Новое',
		CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
		Deadline DATETIME,
		FOREIGN KEY (AssignedEmployeeId) REFERENCES Employees(Id) ON DELETE SET NULL
	)";
	command.ExecuteNonQuery();

	command.CommandText = "SELECT COUNT(*) FROM Employees";
	var count = (long)command.ExecuteScalar();

	if (count == 0)
	{
		command.CommandText = @"
		INSERT INTO Employees (FullName, Login, Password, Role) VALUES
		('Данил Смолков', 'admin', 'admin', 'Admin'),
		('Иван Петров', 'ivan', '123', 'Employee'),
		('Мария Сидорова', 'maria', '123', 'Manager')";
		command.ExecuteNonQuery();

		command.CommandText = @"
		INSERT INTO Workshops (Name, Description, ManagerId) VALUES
		('Цех сборки', 'Сборка самолётов', 3),
		('Цех испытаний', 'Испытания техники', NULL)";
		command.ExecuteNonQuery();

		command.CommandText = @"
		INSERT INTO Tasks (Title, Description, AssignedEmployeeId, Status, Deadline) VALUES
		('Проверить сборку', 'Проверка готовности деталей', 2, 'Новое', '2025-12-10')";
		command.ExecuteNonQuery();
	}
}
