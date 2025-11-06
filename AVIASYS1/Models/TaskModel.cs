public class TaskModel
{
	public int Id { get; set; }
	public string Title { get; set; } = "";
	public string Description { get; set; } = "";
	public int? AssignedEmployeeId { get; set; }
	public string Status { get; set; } = "Новое";
	public DateTime CreatedDate { get; set; }
	public DateTime? Deadline { get; set; }
}
