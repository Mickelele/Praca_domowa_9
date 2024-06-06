using APBD8.Context;
using APBD8.Repositories;
using APBD8.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<TestContext>(
    options => options.UseSqlServer("Name=ConnectionStrings:Default").LogTo(Console.WriteLine, LogLevel.Information));

builder.Services.AddScoped<TripService>();
builder.Services.AddScoped<TripRepository>();
builder.Services.AddScoped<ClientsService>();
builder.Services.AddScoped<ClientsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();

