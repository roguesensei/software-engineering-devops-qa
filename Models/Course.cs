namespace software_engineering_devops_qa.Models;

public record Course
{
	public int CourseId { get; set; }

	public required string Name { get; init; }

	public int InstructorId { get; init; }

	public string? Description { get; init; }
}