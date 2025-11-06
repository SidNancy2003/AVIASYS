using Microsoft.AspNetCore.Mvc.RazorPages;
using AVIASYS1.Services;
using AVIASYS1.Models;

namespace AVIASYS1.Pages.Employee
{
	public class IndexModel : PageModel
	{
		private readonly DatabaseService _dbService;

		public string UserName { get; set; } = string.Empty;
		public int UserId { get; set; }
		public List<TaskModel> MyTasks { get; set; } = new();
		public List<EmployeeModel> Employees { get; set; } = new();

		public IndexModel(DatabaseService dbService)
		{
			_dbService = dbService;
		}

		public void OnGet()
		{
			UserName = HttpContext.Session.GetString("UserName") ?? "Сотрудник";

			var userId = HttpContext.Session.GetInt32("UserId");

			if (userId.HasValue)
			{
				UserId = userId.Value;

				var allTasks = _dbService.GetTasks();
				Employees = _dbService.GetEmployees();

				MyTasks = allTasks.Where(t => t.AssignedEmployeeId == UserId).ToList();
			}
		}

		public string GetEmployeeName(int employeeId)
		{
			var employee = Employees.FirstOrDefault(e => e.Id == employeeId);
			return employee?.FullName ?? "Неизвестно";
		}
	}
}