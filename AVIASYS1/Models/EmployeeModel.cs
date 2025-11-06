namespace AVIASYS1.Models
{
	public class EmployeeModel
	{
		public int Id { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public int? WorkshopId { get; set; }
	}
}
