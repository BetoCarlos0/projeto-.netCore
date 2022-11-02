using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaChamados.Data;
using SistemaChamados.Data.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddDbContext<SistemaDbContext>(
    options =>
    {
        var connectionString = builder.Configuration.GetConnectionString("MySql");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

//builder.Services.AddDbContext<SistemaDbContext>(
//        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MySql")));

builder.Services.AddIdentity<UserCustom, IdentityRole>().AddEntityFrameworkStores<SistemaDbContext>().AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthentication/Login");

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
