using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiples.data;
using MvcNetCoreEFMultiples.Models;
using MySql.Data.MySqlClient;

namespace MvcNetCoreEFMultiples.Repositories;


#region PROCEDURES AND VIEWS


// create view V_EMPLEADOS_DEPT
//     as
//     SELECT emp.EMP_NO as IDEMPLEADO,emp.APELLIDO,emp.OFICIO,
// emp.SALARIO,dept.DEPT_NO,dept.DNOMBRE AS NOMBRE, dept.LOC as LOCALIDAD
// FROM EMP 
// INNER JOIN DEPT ON 
// EMP.DEPT_NO= DEPT.DEPT_NO;

// DELIMITER //
//     CREATE PROCEDURE SP_ALL_VEMPLEADOS()
// BEGIN
//     SELECT * FROM V_EMPLEADOS_DEPT;
// END //
//     DELIMITER ;

// DELIMITER //
//
//     CREATE PROCEDURE SP_INSERT_EMPLEADO
// (
//     IN p_apellido VARCHAR(50),
// IN p_oficio   VARCHAR(50),
// IN p_dir      INT,
//     IN p_salario  INT,
//     IN p_comision INT,
//     IN p_nombredept VARCHAR(50)
//     )
// BEGIN
//     -- Declaración de variables locales (sin @)
// DECLARE v_id INT;
// DECLARE v_fecha DATETIME;
// DECLARE v_iddept INT;
//
// -- 1. Calcular ID y obtener fecha (usamos COALESCE por si la tabla está vacía)
// SELECT COALESCE(MAX(EMP_NO), 0) + 1, NOW()
// INTO v_id, v_fecha
// FROM EMP;
//
// -- 2. Obtener el ID del departamento
// SELECT DEPT_NO INTO v_iddept
// FROM DEPT
// WHERE DNOMBRE = p_nombredept
// LIMIT 1;
//
// -- 3. Insertar
// INSERT INTO EMP (EMP_NO, APELLIDO, OFICIO, DIR, FECHA_ALT, SALARIO, COMISION, DEPT_NO)
// VALUES (v_id, p_apellido, p_oficio, p_dir, v_fecha, p_salario, p_comision, v_iddept);
//
// END //
//
//     DELIMITER ;

#endregion
public class RepositoryEmpleadosMySql :IRepositoryEmpleados
{
    private DataContext _context;

    public RepositoryEmpleadosMySql(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync()
    {
        // var consulta = from datos in _context.Empleados
        //         select datos;
        string sql = "CALL SP_ALL_VEMPLEADOS()";
        
        List<Empleado> data=await _context.Empleados.FromSqlRaw(sql).ToListAsync();
        
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
        
        
        string sql = "CALL SP_INSERT_EMPLEADO(@p_apellido,@p_oficio,@p_dir,@p_salario,@p_comision,@p_nombredept)";
            
        MySqlParameter pamApe=new MySqlParameter("@p_apellido",apellido);
        MySqlParameter pamOficio=new MySqlParameter("@p_oficio",oficio);
        MySqlParameter pamDir=new MySqlParameter("@p_dir",dir);
        MySqlParameter pamSalario=new MySqlParameter("@p_salario",salario);
        MySqlParameter pamComision=new MySqlParameter("@p_comision",comision);
        MySqlParameter pamDept=new MySqlParameter("@p_nombredept",departamento);
    
        await _context.Database.ExecuteSqlRawAsync(sql, pamApe,pamOficio, pamDir, pamSalario, pamComision, pamDept);
            
    }
}