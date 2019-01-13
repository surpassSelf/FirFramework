using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirServer.Defines;

namespace FirServer.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login(string UserName, string Password, string returnUrl = null)
        {
            const string badUserNameOrPasswordMessage = "Username or password is incorrect.";
            if (UserName == null || Password == null || returnUrl == null)
            {
                return BadRequest(badUserNameOrPasswordMessage);
            }
            if (UserName == AppConst.adminUser && Password == AppConst.adminPass)
            {
                var data = Encoding.ASCII.GetBytes(AppConst.loginPass);
                HttpContext.Session.Set(AppConst.loginKey, data);
                return Redirect("/Admin");
            }
            return Redirect("/Error");
        }

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction(nameof(HomeController.Index), "Home");
        //}
    }
}
