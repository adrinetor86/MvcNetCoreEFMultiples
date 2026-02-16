using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiples.data;
using MvcNetCoreEFMultiples.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddTransient<IRepositoryEmpleados,RepositoryEmpleadosSqlServer>();
// string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
// builder.Services.AddDbContext<DataContext>
//     (options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IRepositoryEmpleados,RepositoryEmpleadosMySql>();
string connectionString = builder.Configuration.GetConnectionString("MySqlHospital");
builder.Services.AddDbContext<DataContext>
    (options => options.UseMySQL(connectionString));

// builder.Services.AddTransient<IRepositoryEmpleados,RepositoryEmpleadosOracle>();
// string connectionString = builder.Configuration.GetConnectionString("OracleHospital");
// builder.Services.AddDbContext<DataContext>
//     (options => options.UseOracle(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
