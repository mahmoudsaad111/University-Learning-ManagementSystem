using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class AssigmentsNotificationModel : PageModel
{
    public string AssignmentLink { get; set; }
    public string SectionName { get; set; }

    public string CourseCycleName { get; set; }
    public DateTime Deadline { get; set; }



}
