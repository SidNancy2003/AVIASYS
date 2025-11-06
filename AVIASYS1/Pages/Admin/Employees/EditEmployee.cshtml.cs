using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Employees
{
	public class EditEmployeeModel : PageModel
	{
		private readonly DatabaseService _db;
		public EditEmployeeModel(DatabaseService db) => _db = db;

		[BindProperty]
		public EmployeeModel Employee { get; set; } = new();

		public List<WorkshopModel> Workshops { get; set; } = new();

		public void OnGet(int id)
		{
			Employee = _db.GetEmployee(id) ?? new EmployeeModel();
			Workshops = _db.GetWorkshops();
		}

		public IActionResult OnPost()
		{
			Workshops = _db.GetWorkshops();

			if (!ModelState.IsValid)
				return Page();

			var existing = _db.GetEmployee(Employee.Id);
			if (existing == null)
				return NotFound();

			if (string.IsNullOrWhiteSpace(Employee.Password))
				Employee.Password = existing.Password;

			_db.UpdateEmployee(Employee);
			return RedirectToPage("Employees");
		}

		public IActionResult OnPostDelete(int id)
		{
			if (id == 0)
				return BadRequest();

			_db.DeleteEmployee(id);
			return RedirectToPage("Employees");
		}
	}
}
