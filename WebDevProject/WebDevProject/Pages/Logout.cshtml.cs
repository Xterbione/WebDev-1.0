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
                return new RedirectToPageResult("/Login");
            }
            // return new RedirectToPageResult("/Index");
            return Page();
           
        }
    }
}
