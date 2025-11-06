using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Tasks
{
	public class AddTaskModel : PageModel
	{
		private readonly DatabaseService _db;
		public AddTaskModel(DatabaseService db) => _db = db;

		[BindProperty] public TaskModel Task { get; set; } = new();
		public List<EmployeeModel> Managers { get; set; } = new();

		public void OnGet()
		{
			Managers = _db.GetEmployees().Where(e => e.Role == "Manager").ToList();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				Managers = _db.GetEmployees().Where(e => e.Role == "Manager").ToList();
				return Page();
			}

			_db.AddTask(Task);
			return RedirectToPage("Tasks");
		}
	}
}
