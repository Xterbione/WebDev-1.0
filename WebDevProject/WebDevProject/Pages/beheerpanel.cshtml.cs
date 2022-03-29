using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebDevProject.Pages
{
    public class Beheerpanel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("cookie") == null)
            {
                return new RedirectToPageResult("/Index");
            }
            // return new RedirectToPageResult("/Index");
            return Page();
        }
    }
}
