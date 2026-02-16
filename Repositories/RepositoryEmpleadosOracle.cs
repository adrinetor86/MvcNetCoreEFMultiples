using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiples.data;
using MvcNetCoreEFMultiples.Models;
using Oracle.ManagedDataAccess.Client;

namespace MvcNetCoreEFMultiples.Repositories;


#region PROCEDURES AND VIEWS
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
// create or replace PROCEDURE SP_ALL_VEMPLEADOS
//     (p_cursor_empleados out SYS_REFCURSOR)
// AS
//     BEGIN
// open p_cursor_empleados for
//     SELECT * FROM V_EMPLEADOS_DEPT;
// END;

// create or replace PROCEDURE SP_INSERT_EMPLEADO
// (
//     p_apellido    IN EMP.APELLIDO%TYPE,
//     p_oficio      IN EMP.OFICIO%TYPE,
//     p_dir         IN EMP.DIR%TYPE,
//     p_salario     IN EMP.SALARIO%TYPE,
//     p_comision    IN EMP.COMISION%TYPE,
//     p_nombredept  IN DEPT.DNOMBRE%TYPE
// )
// AS
//     v_id      EMP.EMP_NO%TYPE;
// v_fecha   DATE;
// v_iddept  DEPT.DEPT_NO%TYPE;
// begin
//
//     SELECT NVL(MAX(EMP_NO), 0) + 1, SYSDATE 
// INTO v_id, v_fecha 
// FROM EMP;
//     
// -- 2. Obtener el ID del departamento
// SELECT DEPT_NO 
// INTO v_iddept 
// FROM DEPT 
// WHERE DNOMBRE = p_nombredept;
//
//
// INSERT INTO EMP 
// VALUES (v_id, p_apellido, p_oficio, p_dir, v_fecha, p_salario, p_comision, v_iddept);
//
// COMMIT;
// END;

#endregion



public class RepositoryEmpleadosOracle :IRepositoryEmpleados
{
    private DataContext _context;

    public RepositoryEmpleadosOracle(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync()
    {
        
        //begin
        // sp_procedure(:param1,param2)
        //end
        string sql = "begin ";
        sql += " SP_ALL_VEMPLEADOS (:p_cursor_empleados); ";
        sql += " end;";
        OracleParameter pamCursor = new OracleParameter();
        pamCursor.ParameterName = "p_cursor_empleados";
        pamCursor.Value = null;
        pamCursor.Direction = ParameterDirection.Output;
        //INDICAMOS EL TIPO DE ORACLE
        pamCursor.OracleDbType= OracleDbType.RefCursor;
         var consulta= _context.Empleados.FromSqlRaw(sql, pamCursor);
         
         return await consulta.ToListAsync();
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

        string sql = "begin ";
        sql += " SP_INSERT_EMPLEADO (:p_apellido,:p_oficio,:p_dir,:p_salario,:p_comision,:p_nombredept); ";
        sql += " end;";
        
            
        OracleParameter pamApe=new OracleParameter(":p_apellido",apellido);
        OracleParameter pamOficio=new OracleParameter(":p_oficio",oficio);
        OracleParameter pamDir=new OracleParameter(":p_dir",dir);
        OracleParameter pamSalario=new OracleParameter(":p_salario",salario);
        OracleParameter pamComision=new OracleParameter(":p_comision",comision);
        OracleParameter pamDept=new OracleParameter(":p_nombredept",departamento);
   
        await _context.Database.ExecuteSqlRawAsync(sql, pamApe,pamOficio, pamDir, pamSalario, pamComision, pamDept);
            
    }

}