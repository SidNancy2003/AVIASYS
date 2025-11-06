using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Tasks
{
	public class EditTaskModel : PageModel
	{
		private readonly DatabaseService _db;
		public EditTaskModel(DatabaseService db) => _db = db;

		[BindProperty]
		public TaskModel Task { get; set; } = new();

		public List<EmployeeModel> Employees { get; set; } = new();

		public void OnGet(int? id)
		{
			Employees = _db.GetEmployees();
			Task = id.HasValue ? _db.GetTask(id.Value) ?? new TaskModel() : new TaskModel();
		}

		public IActionResult OnPost()
		{
			if (string.IsNullOrWhiteSpace(Task.Title))
			{
				ModelState.AddModelError("Task.Title", "Название обязательно");
				Employees = _db.GetEmployees();
				return Page();
			}

			if (Task.Id == 0)
				_db.AddTask(Task);
			else
				_db.UpdateTask(Task);

			return RedirectToPage("Tasks");
		}

		public IActionResult OnPostDelete(int id)
		{
			if (id == 0)
				return BadRequest();

			_db.DeleteTask(id);
			return RedirectToPage("Tasks");
		}
	}
}
