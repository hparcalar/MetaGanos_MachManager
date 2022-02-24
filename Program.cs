using Microsoft.EntityFrameworkCore;
using MachManager.Context;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MetaGanosSchema>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("GanosData")));

// AUTO-MIGRATE ON STARTUP
SchemaFactory.ConnectionString = builder.Configuration.GetConnectionString("GanosData");
SchemaFactory.ApplyMigrations();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();
