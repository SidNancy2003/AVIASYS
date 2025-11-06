using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AVIASYS1.Pages
{
	public class IndexModel : PageModel
	{
		public string? UserName { get; set; }
		public string? UserRole { get; set; }

		public void OnGet()
		{
			UserName = HttpContext.Session.GetString("UserName");
			UserRole = HttpContext.Session.GetString("UserRole");
		}
	}
}