using AVIASYS1.Models;
using AVIASYS1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace AVIASYS1.Pages.Admin.Workshops
{
	public class WorkshopsModel : PageModel
	{
		private readonly DatabaseService _db;
		public WorkshopsModel(DatabaseService db) => _db = db;

		public List<WorkshopModel> Workshops { get; set; } = new();
		public List<EmployeeModel> Employees { get; set; } = new();

		public void OnGet()
		{
			Workshops = _db.GetWorkshops();
			Employees = _db.GetEmployees();
		}

		public string GetManagerName(int? managerId)
		{
			if (managerId == null) return "—";
			var m = Employees.FirstOrDefault(e => e.Id == managerId);
			return m?.FullName ?? "—";
		}
	}
}
