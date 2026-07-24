using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffCoreRD.Models;

namespace StaffCoreRD.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    // GET: /Account/Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            // El primer usuario registrado en el sistema será Administrador; el resto, Viewer.
            bool esPrimerUsuario = _userManager.Users.Count() == 1;
            string rol = esPrimerUsuario ? "Administrador" : "Viewer";

            await _userManager.AddToRoleAsync(user, rol);
            await _signInManager.SignInAsync(user, isPersistent: false);

            TempData["Exito"] = $"Cuenta creada correctamente. Rol asignado: {rol}.";
            return RedirectToAction("Index", "Staff");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RecordarMe, lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Staff");
        }

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "Cuenta bloqueada temporalmente por intentos fallidos. Intenta más tarde.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
        }

        return View(model);
    }

    // POST: /Account/Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }
}