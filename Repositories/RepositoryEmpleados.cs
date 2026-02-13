using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiples.data;
using MvcNetCoreEFMultiples.Models;

namespace MvcNetCoreEFMultiples.Repositories;

#region PROCEDURES AND VIEWS
// CREATE VIEW V_EMPLEADOS_DEPT
//     AS
// SELECT
// EMP.EMP_NO AS IDEMPLEADO,
//     EMP.APELLIDO,
// EMP.OFICIO,
// EMP.SALARIO,
// DEPT.DEPT_NO,
// DEPT.DNOMBRE AS NOMBRE,
//     DEPT.LOC AS LOCALIDAD
//     FROM EMP
//     INNER JOIN DEPT ON 
// EMP.DEPT_NO= DEPT.DEPT_NO
//         
//         
// GO    


// --ORACLE
// create or REPLACE VIEW V_EMPLEADOS_DEPT
//     AS
// SELECT
// EMP.EMP_NO AS IDEMPLEADO,
//     EMP.APELLIDO,
// EMP.OFICIO,
// EMP.SALARIO,
// DEPT.DEPT_NO,
// DEPT.DNOMBRE AS NOMBRE,
//     DEPT.LOC AS LOCALIDAD
//     FROM EMP
//     INNER JOIN DEPT ON 
// EMP.DEPT_NO= DEPT.DEPT_NO
//     
// commit;  

#endregion
public class RepositoryEmpleados
{
    
    private DataContext _context;

    public RepositoryEmpleados(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync()
    {
        var consulta = from datos in _context.Empleados
            select datos;
        
        return  await consulta.ToListAsync();
    }

    public async Task<Empleado> GetEmpleadoByIdAsync(int idEmpleado)
    {
        var consulta= from datos in _context.Empleados
            where datos.IdEmpleado==idEmpleado
                select datos;

        return await consulta.FirstOrDefaultAsync();
    }
    
}