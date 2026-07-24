using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffCoreRD.Data;

namespace StaffCoreRD.Controllers;

[Authorize] // cualquier usuario logueado puede ver el listado; Módulo 11 refina esto por acción
public class StaffController : Controller
{
    private readonly StaffDbContext _context;

    public StaffController(StaffDbContext context)
    {
        _context = context;
    }

    // GET: /Staff
    public async Task<IActionResult> Index()
    {
        var personal = await _context.Personal
            .OrderBy(s => s.Nombre)
            .ToListAsync();

        return View(personal);
    }
}