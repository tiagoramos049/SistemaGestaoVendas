using Microsoft.Extensions.DependencyInjection;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Repository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<Dao>();
builder.Services.AddScoped<IVendedor, VendedorRepository>();
builder.Services.AddScoped<IProduto, ProdutoRepository>();
builder.Services.AddScoped<ICliente, ClienteRepository>();
builder.Services.AddScoped<ILogin, LoginRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();