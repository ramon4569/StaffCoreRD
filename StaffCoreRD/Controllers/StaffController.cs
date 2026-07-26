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

    // GET: /Staff/Edit/5
    [Authorize(Roles = "Administrador,RRHH")]
    public async Task<IActionResult> Edit(int id)
    {
        var staff = await _context.Personal.FindAsync(id);

        if (staff == null)
            return NotFound();

        return View(staff);
    }

    // POST: /Staff/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador,RRHH")]
    public async Task<IActionResult> Edit(int id, Staff staff)
    {
        if (id != staff.Id)
            return NotFound();

        if (!ModelState.IsValid)
            return View(staff);

        try
        {
            _context.Personal.Update(staff);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Personal.AnyAsync(s => s.Id == id))
                return NotFound();
            throw;
        }

        TempData["Exito"] = $"Empleado \"{staff.Nombre}\" actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}