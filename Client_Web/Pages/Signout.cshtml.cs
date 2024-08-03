using Client_Web.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Namespace
{
    public class SignoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, MyConstants.AuthenticationScheme);
        }
    }
}
