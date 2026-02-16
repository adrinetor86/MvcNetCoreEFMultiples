using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
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
// GO    

// alter PROCEDURE  SP_ALL_VEMPLEADOS
//     AS 
// SELECT * FROM V_EMPLEADOS_DEPT
//     GO


// alter PROCEDURE SP_INSERT_EMPLEADO
// (@apellido NVARCHAR(50),@oficio NVARCHAR(50),
//     @dir INT ,@salario INT,@comision INT,@nombredept NVARCHAR(50))
// AS
//     declare @id int;
// declare @fecha datetime;
// declare @iddept int; 
//     
// select @id= MAX(EMP_NO)+1 ,@fecha =GETDATE() FROM EMP;
//     
// select @iddept=dept_no from dept where DNOMBRE=@nombredept;
//     
// INSERT INTO EMP VALUES(@id,@apellido,@oficio,@dir,
//     @fecha,@salario,@comision,@iddept);
// GO

#endregion
public class RepositoryEmpleadosSqlServer :IRepositoryEmpleados
{
    
    private DataContext _context;

    public RepositoryEmpleadosSqlServer(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync()
    {
        string sql = "SP_ALL_VEMPLEADOS";
        
        var consulta = _context.Empleados.FromSqlRaw(sql);
        
        List<Empleado> data=await consulta.ToListAsync();
        
        return  data;
    }

    public async Task<Empleado> GetEmpleadoByIdAsync(int idEmpleado)
    {
        var consulta= from datos in _context.Empleados
            where datos.IdEmpleado==idEmpleado
                select datos;

        return await consulta.FirstOrDefaultAsync();
    }


    public async Task InsertEmpleadoAsync(string apellido, string oficio, int dir,
        int salario, int comision, string departamento)
    {
        
            string sql = "SP_INSERT_EMPLEADO @apellido,@oficio,@dir,@salario,@comision,@nombredept";
            
            SqlParameter pamApe=new SqlParameter("@apellido",apellido);
            SqlParameter pamOficio=new SqlParameter("@oficio",oficio);
            SqlParameter pamDir=new SqlParameter("@dir",dir);
            SqlParameter pamSalario=new SqlParameter("@salario",salario);
            SqlParameter pamComision=new SqlParameter("@comision",comision);
            SqlParameter pamDept=new SqlParameter("@nombredept",departamento);

            await _context.Database.ExecuteSqlRawAsync(sql, pamApe,pamOficio, pamDir, pamSalario, pamComision, pamDept);
            
    }
    
    
    
}