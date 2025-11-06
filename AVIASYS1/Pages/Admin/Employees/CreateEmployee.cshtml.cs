using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Employees
{
	public class CreateEmployeeModel : PageModel
	{
		private readonly DatabaseService _db;
		public CreateEmployeeModel(DatabaseService db) => _db = db;

		[BindProperty] public EmployeeModel Employee { get; set; } = new();
		public List<WorkshopModel> Workshops { get; set; } = new();

		public void OnGet()
		{
			Workshops = _db.GetWorkshops();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				Workshops = _db.GetWorkshops();
				return Page();
			}

			_db.AddEmployee(Employee);
			return RedirectToPage("Employees");
		}
	}
}
