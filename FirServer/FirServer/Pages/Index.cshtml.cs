using Microsoft.AspNetCore.Mvc.RazorPages;

namespace kickit_web.Pages
{
    public class IndexModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your index page.";
        }
    }
}
