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

    // GET: /Staff/Details/5
    [Authorize]
    public async Task<IActionResult> Details(int id)
    {
        var staff = await _context.Personal.FindAsync(id);

        if (staff == null)
            return NotFound();

        return View(staff);
    }

    // GET: /Staff/Reporte
    [Authorize]
    public async Task<IActionResult> Reporte()
    {
        var resumen = await _context.Personal
            .GroupBy(s => s.Departamento)
            .Select(g => new ResumenDepartamentoViewModel
            {
                Departamento = g.Key,
                CantidadEmpleados = g.Count(),
                TotalSalarios = g.Sum(s => s.Salario),
                PromedioSalario = g.Average(s => s.Salario)
            })
            .OrderByDescending(r => r.TotalSalarios)
            .ToListAsync();

        return View(resumen);
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

    // GET: /Staff/Delete/5 (solo muestra confirmación, NUNCA borra en GET)
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var staff = await _context.Personal.FindAsync(id);

        if (staff == null)
            return NotFound();

        return View(staff);
    }

    // POST: /Staff/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var staff = await _context.Personal.FindAsync(id);

        if (staff != null)
        {
            _context.Personal.Remove(staff);
            await _context.SaveChangesAsync();
            TempData["Exito"] = $"Empleado \"{staff.Nombre}\" eliminado correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

}