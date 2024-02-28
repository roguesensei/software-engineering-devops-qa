namespace software_engineering_devops_qa.Models;

public record Enrolment
{
	public int EnrolmentId { get; set; } 

	public int CourseId { get; init; } 

	public int UserId { get; init; }

	public DateTime CourseDate { get; init; }
}