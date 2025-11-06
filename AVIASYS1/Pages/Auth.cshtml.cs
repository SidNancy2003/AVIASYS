using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AVIASYS1.Services;
using AVIASYS1.Models;

namespace AVIASYS1.Pages
{
	public class AuthModel : PageModel
	{
		private readonly DatabaseService _dbService;

		public AuthModel(DatabaseService dbService)
		{
			_dbService = dbService;
		}

		public IActionResult OnPost()
		{
			var login = Request.Form["login"];
			var password = Request.Form["password"];

			var employees = _dbService.GetEmployees();
			var employee = employees.FirstOrDefault(e => e.Login == login && e.Password == password);  // МЕНЯЕМ

			if (employee != null)
			{
				HttpContext.Session.SetInt32("UserId", employee.Id);
				HttpContext.Session.SetString("UserRole", employee.Role);
				HttpContext.Session.SetString("UserName", employee.FullName);

				if (employee.Role == "Admin")
				{
					return RedirectToPage("/Admin/Index");
				}
				else
				{
					return RedirectToPage("/Employees/Index");
				}

			}

			ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
			return Page();
		}

		public IActionResult OnGetLogout()
		{
			HttpContext.Session.Clear();
			return RedirectToPage("/Auth");
		}

	}
}