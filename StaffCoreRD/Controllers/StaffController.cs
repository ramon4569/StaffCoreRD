using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffCoreRD.Data;
using StaffCoreRD.Models;

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

    // GET: /Staff/Create
    [Authorize(Roles = "Administrador,RRHH")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Staff/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,RRHH")]
    public async Task<IActionResult> Create(Staff staff)
    {
        if (!ModelState.IsValid)
            return View(staff);

        _context.Personal.Add(staff);
        await _context.SaveChangesAsync();

        TempData["Exito"] = $"Empleado \"{staff.Nombre}\" agregado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}