using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WikiPex.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options =>
    {
        options.LoginPath = "/Home/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddDbContext<sistem21_wikiapexContext>(
    optionsBuilder => optionsBuilder.UseMySql("server=198.38.83.169;database=sistem21_wikiapex;user=sistem21_WikiApexAdmina1;password=WikiApexAdmina1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb"))
    );
builder.Services.AddMvc();
var app = builder.Build();
app.UseFileServer();
app.UseRouting();
app.UseAuthentication();//Esto debe de estar antes de los endpoints
app.UseAuthorization();//Esto tambien debe de estar antes de los endpoints
//Para el administrador ---> Usuario: Prueba Contraseña: Admina1
app.UseEndpoints(x =>
{
    x.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
    x.MapDefaultControllerRoute();
});
app.Run();