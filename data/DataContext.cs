using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiples.Models;

namespace MvcNetCoreEFMultiples.data;

public class DataContext :DbContext
{
    
    public DataContext(DbContextOptions<DataContext> options) : base(options){}
    
    public DbSet<Empleado> Empleados { get; set; }
    
    public DbSet<Emp> Emp { get; set; }
    
}