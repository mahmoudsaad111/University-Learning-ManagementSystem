using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class PasswordResetModel : PageModel
{
    public string ResetPasswordUrl { get; set; }
    public string Name { get; set; }

}
