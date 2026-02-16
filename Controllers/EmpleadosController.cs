using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEFMultiples.Models;
using MvcNetCoreEFMultiples.Repositories;

namespace MvcNetCoreEFMultiples.Controllers;

public class EmpleadosController : Controller
{

    private IRepositoryEmpleados _repo;

    public EmpleadosController(IRepositoryEmpleados repo)
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


    public IActionResult Create()
    {
        return View();
    } 
     [HttpPost]
    public async Task<IActionResult> Create(Emp empleado,string deptnombre)
    {
        
        await _repo.InsertEmpleadoAsync(empleado.Apellido,
            empleado.Oficio,empleado.Dir,empleado.Salario,
            empleado.Comision,deptnombre);
        
        return RedirectToAction("Index");
    }
}