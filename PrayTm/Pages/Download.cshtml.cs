using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrayTm.Pages
{
    public class DownloadModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string LoggedUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool Login { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
            var flag = HttpContext.Session.GetString("HideLogin");
            Login = Convert.ToBoolean(flag);
            LoggedUser = HttpContext.Session.GetString("Name");
        }

        public IActionResult OnPostLogoutAsync()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("./Index");

        }
    }
}
