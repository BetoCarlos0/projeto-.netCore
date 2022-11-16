using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CadastroCarroApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
var MyAllowSpecificOrigins = "http://127.0.0.1:5500";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CadastroCarroApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadastroCarroApiContext")));

//builder.Services.AddDbContext<SistemaDbContext>(
//    options =>
//    {
//        var connectionString = builder.Configuration.GetConnectionString("MySql");
//        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//    });

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy(MyAllowSpecificOrigins, builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
