using MvcNetCoreEFMultiples.Models;

namespace MvcNetCoreEFMultiples.Repositories;

public interface IRepositoryEmpleados
{
    Task<List<Empleado>> GetEmpleadosDepartamentoAsync();
    
    Task<Empleado> GetEmpleadoByIdAsync(int idEmpleado);

    Task InsertEmpleadoAsync(string apellido,string oficio,int dir,
        int salario,int comision,string departamento);
}

