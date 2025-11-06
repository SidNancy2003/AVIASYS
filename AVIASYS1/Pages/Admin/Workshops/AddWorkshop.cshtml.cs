using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Workshops
{
	public class AddWorkshopModel : PageModel
	{
		private readonly DatabaseService _db;
		public AddWorkshopModel(DatabaseService db) => _db = db;

		[BindProperty] public WorkshopModel Workshop { get; set; } = new();
		public List<EmployeeModel> Managers { get; set; } = new();

		public void OnGet()
		{
			Managers = _db.GetEmployees().Where(e => e.Role == "Manager").ToList();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) return Page();
			_db.AddWorkshop(Workshop);
			return RedirectToPage("Workshops");
		}
	}
}
