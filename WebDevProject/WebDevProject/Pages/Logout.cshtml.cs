using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevProject.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        { 
            // SessionExten
            if (HttpContext.Session.GetString("cockie") != null)
            {
                HttpContext.Session.Remove("cockie");
              
                return new RedirectToPageResult("/Index");
            }
            // return new RedirectToPageResult("/Index");
            return Page();
           
        }
    }
}
