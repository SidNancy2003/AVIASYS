using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages.Admin.Employees
{
	public class EmployeesModel : PageModel
	{
		private readonly DatabaseService _db;
		public EmployeesModel(DatabaseService db) => _db = db;

		public List<EmployeeModel> Employees { get; set; } = new();
		private List<WorkshopModel> Workshops { get; set; } = new();

		public void OnGet()
		{
			Employees = _db.GetEmployees();
			Workshops = _db.GetWorkshops();
		}

		public string GetWorkshopName(int? workshopId)
		{
			if (workshopId == null)
				return "—";
			var w = Workshops.FirstOrDefault(x => x.Id == workshopId);
			return w?.Name ?? "—";
		}
	}
}
