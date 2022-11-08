using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SistemaChamados.Data;
using SistemaChamados.Data.Identity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.AllowedForNewUsers = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

//builder.Services.AddDbContext<SistemaDbContext>(
//    options =>
//    {
//        var connectionString = builder.Configuration.GetConnectionString("MySql");
//        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//    });


builder.Services.AddDbContext<SistemaDbContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("Sqlserver")));

builder.Services.AddIdentity<UserCustom, IdentityRole>().AddEntityFrameworkStores<SistemaDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<SistemaDbContext>();
        var userManager = services.GetRequiredService<UserManager<UserCustom>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedData.SeedRolesAsync(userManager, roleManager);
        await SeedData.SeedSuperAdminAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
