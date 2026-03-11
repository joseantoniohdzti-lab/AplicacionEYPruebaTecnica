using AccesoDatos.Data;
using AccesoDatos.UnidadTrabajos;
using AccesoDatos.UnidadTrabajos.IUnidadTrabajos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Models.ApiMapper;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var cs = builder.Configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


//agregamos servico al contexto que emplearemos

builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(cs,s => s.EnableRetryOnFailure()));

//agremaos el AutoMapper

builder.Services.AddAutoMapper( _ => { }, typeof(MapearEntidades).Assembly);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles)
    .AddRazorRuntimeCompilation();


builder.Services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();

//configurar login 

// Agregar servicios de autenticación
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login/LoginIndex";
        options.LogoutPath = "/Admin/Login/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });


builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
