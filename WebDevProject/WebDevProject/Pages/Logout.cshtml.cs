using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevProject.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        { 
            // SessionExten
            if (HttpContext.Session.GetString("cookie") != null)
            {
                HttpContext.Session.Remove("cookie");
              
                return new RedirectToPageResult("/Index");
            }
            // return new RedirectToPageResult("/Index");
            return Page();
           
        }
    }
}
