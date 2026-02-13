using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEFMultiples.Models;
using MvcNetCoreEFMultiples.Repositories;

namespace MvcNetCoreEFMultiples.Controllers;

public class EmpleadosController : Controller
{

    private RepositoryEmpleados _repo;

    public EmpleadosController(RepositoryEmpleados repo)
    {
        _repo = repo;
    }
    
    public async Task<IActionResult> Index()
    {
        List<Empleado> empleados = await _repo.GetEmpleadosDepartamentoAsync();
        return View(empleados);
    }

    public async Task<IActionResult> Details(int idEmpleado)
    {
        Empleado empleado= await _repo.GetEmpleadoByIdAsync(idEmpleado);
        return View(empleado);
    }
}