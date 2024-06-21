using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ExamNotificationModel : PageModel
{
    public string ExamLink { get; set; }
    public string? SectionName { get; set; }

    public string? CourseCycleName { get; set; }

    public string? CourseName { get; set; }
    public DateTime ExamDateTime { get; set; }

}
