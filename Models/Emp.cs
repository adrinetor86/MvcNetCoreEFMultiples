using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCoreEFMultiples.Models;

[Table("EMP")]
public class Emp
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int IdEmpleado { get; set; }
    
    [Column("APELLIDO")]
    public string Apellido { get; set; }
    
    [Column("OFICIO")]
    public string Oficio { get; set; }  
    
    [Column("DIR")]
    public int Dir { get; set; }
    
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // [Column("FECHA_ALT")]
    // public DateTime Fecha { get; set; }
    
    [Column("SALARIO")]
    public int Salario { get; set; }
    
    [Column("COMISION")]
    public int Comision { get; set; }
    
    // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    // [Column("DEPT_NO")]
    // public int IdDept { get; set; }
    
}