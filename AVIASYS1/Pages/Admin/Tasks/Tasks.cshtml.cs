using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AVIASYS1.Pages.Admin.Tasks
{
	public class TasksModel : PageModel
	{
		private readonly DatabaseService _db;
		public TasksModel(DatabaseService db) => _db = db;

		public List<TaskModel> Tasks { get; set; } = new();

		public List<EmployeeModel> Employees { get; set; } = new();

		public string? FilterStatus { get; set; }
		public int? FilterEmployeeId { get; set; }

		public void OnGet(string? status, int? employeeId)
		{
			Employees = _db.GetEmployees();
			var allTasks = _db.GetTasks();

			if (!string.IsNullOrEmpty(status))
				allTasks = allTasks.Where(t => t.Status == status).ToList();

			if (employeeId.HasValue)
				allTasks = allTasks.Where(t => t.AssignedEmployeeId == employeeId.Value).ToList();

			Tasks = allTasks;

			FilterStatus = status;
			FilterEmployeeId = employeeId;
		}

		public string GetEmployeeName(int? id)
		{
			if (id == null) return "—";
			var emp = Employees.FirstOrDefault(e => e.Id == id);
			return emp?.FullName ?? "—";
		}
	}
}
