using Microsoft.EntityFrameworkCore;
using MachManager.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MachManager.Authentication;
using System.Security.Claims;
using MachManager.Workers;

var builder = WebApplication.CreateBuilder(args);
#region PGSQL
// AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
// builder.Services.AddDbContext<MetaGanosSchema>(options =>
//                 options.UseNpgsql(builder.Configuration.GetConnectionString("GanosData")));
#endregion
#region MSSQL
builder.Services.AddDbContext<MetaGanosSchema>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("GanosData")));
#endregion

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
builder.Services.AddHostedService<LicenseWorker>();
builder.Services.AddSingleton<MgAuth>(new MgAuth(apiKey));
builder.Services.AddAuthorization(options =>
{
   options.AddPolicy("Dealer", policy => policy.RequireClaim(ClaimTypes.Role, "Dealer"));
   options.AddPolicy("FactoryOfficer", policy => policy.RequireClaim(ClaimTypes.Role, "FactoryOfficer"));
   options.AddPolicy("Employee", policy => policy.RequireClaim(ClaimTypes.Role, "Employee"));
   options.AddPolicy("Machine", policy => policy.RequireClaim(ClaimTypes.Role, "Machine"));
});

builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition");
                    });
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

app.UseHsts();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
