using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StaffCoreRD.Models;

namespace StaffCoreRD.Controllers;

[Authorize(Roles = "Administrador")]
public class UsuariosController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // GET: /Usuarios
    public async Task<IActionResult> Index()
    {
        var usuarios = _userManager.Users.ToList();
        var lista = new List<UsuarioRolViewModel>();

        foreach (var u in usuarios)
        {
            var roles = await _userManager.GetRolesAsync(u);
            lista.Add(new UsuarioRolViewModel
            {
                Id = u.Id,
                Email = u.Email,
                RolActual = roles.FirstOrDefault() ?? "(sin rol)"
            });
        }

        return View(lista);
    }

    // POST: /Usuarios/CambiarRol
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarRol(string id, string nuevoRol)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        var rolesActuales = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, rolesActuales);
        await _userManager.AddToRoleAsync(user, nuevoRol);

        TempData["Exito"] = $"Rol de \"{user.Email}\" actualizado a {nuevoRol}.";
        return RedirectToAction(nameof(Index));
    }
}