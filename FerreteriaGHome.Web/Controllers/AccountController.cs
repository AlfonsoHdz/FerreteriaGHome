using FerreteriaGHome.Web.Helper;
using FerreteriaGHome.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly IUserHelper userHelper;

    public AccountController(IUserHelper userHelper)
    {
        this.userHelper = userHelper;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (this.User.Identity.IsAuthenticated)
        {
            return this.RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await this.userHelper.LoginAsync(model.Email,
                model.Password, model.RememberMe);
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }
            this.ModelState.AddModelError(string.Empty, "Error");
            return this.View(model);
        }
        return this.View(model);
    }
    public async Task<IActionResult> Logout()
    {
        await this.userHelper.LogoutAsync();
        return this.RedirectToAction("Index", "Home");
    }
}
