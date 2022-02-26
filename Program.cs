using Microsoft.EntityFrameworkCore;
using MachManager.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MachManager.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MetaGanosSchema>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("GanosData")));

// AUTO-MIGRATE ON STARTUP
SchemaFactory.ConnectionString = builder.Configuration.GetConnectionString("GanosData");
SchemaFactory.ApplyMigrations();

// Apply JWT authentication configuration
string apiKey = "MetaGanos2022Automat";
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer=false,
            ValidateAudience=false,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(apiKey)),
        };
    });
builder.Services.AddSingleton<MgAuth>(new MgAuth(apiKey));
builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("Dealer", policy => policy.RequireClaim(ClaimTypes.Role, "Dealer"));
   options.AddPolicy("Employee", policy => policy.RequireClaim(ClaimTypes.Role, "Employee"));
   options.AddPolicy("Machine", policy => policy.RequireClaim(ClaimTypes.Role, "Machine"));
});

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

app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();
