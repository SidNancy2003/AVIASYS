using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Workshops
{
	public class EditWorkshopModel : PageModel
	{
		private readonly DatabaseService _db;
		public EditWorkshopModel(DatabaseService db) => _db = db;

		[BindProperty] public WorkshopModel Workshop { get; set; } = new();
		public List<EmployeeModel> Managers { get; set; } = new();

		public IActionResult OnGet(int id)
		{
			Workshop = _db.GetWorkshops().FirstOrDefault(w => w.Id == id) ?? new WorkshopModel();
			Managers = _db.GetEmployees().Where(e => e.Role == "Manager").ToList();
			return Page();
		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid) return Page();
			_db.UpdateWorkshop(Workshop);
			return RedirectToPage("Workshops");
		}

		public IActionResult OnPostDelete(int id)
		{
			_db.DeleteWorkshop(id);
			return RedirectToPage("Workshops");
		}
	}
}
