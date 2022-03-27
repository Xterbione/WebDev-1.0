using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevProject.Pages
{
    public class LogoutModel : PageModel
    {
       // public string Message { get; set; }
        

        public IActionResult OnPost()
        {
            HttpContext.Session.Remove("cockie");
            return new RedirectToPageResult("/Index");
        }

    }
}
