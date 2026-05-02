using Clinica.API.Models;
using Clinica.API.Repositories;
using Clinica.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
 
// ── Configurações ────────────────────────────────────────────────────────────
var connStr     = builder.Configuration.GetConnectionString("DefaultConnection")!;
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey   = jwtSettings["SecretKey"]!;
 
// ── Registro de dependências ─────────────────────────────────────────────────
builder.Services.AddSingleton(_ => new AuthRepository(connStr));
builder.Services.AddScoped<IAuthService, AuthService>();
 
// ── JWT Authentication ───────────────────────────────────────────────────────
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = jwtSettings["Issuer"],
            ValidAudience            = jwtSettings["Audience"],
            IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });
 
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
 
var app = builder.Build();
 
if (app.Environment.IsDevelopment()) app.MapOpenApi();
 
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();




// 4. Controllers + Swagger
// builder.Services.AddAuthorization();
// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 5. Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // ← deve vir antes do Authorization
app.UseAuthorization();
app.MapControllers();

app.Run();


// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;

// 1. Configurar SQLite
// builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlite(builder.Configuration.GetConnectionString("ClinicaDb")));

// 2. Repositório e serviço de autenticação
// builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
// builder.Services.AddScoped<IAuthService, AuthService>();

// 3. JWT
// var jwtSection = builder.Configuration.GetSection("Jwt");
// var secretKey  = jwtSection["SecretKey"]!;

// builder.Services
//     .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer           = true,
//             ValidateAudience         = true,
//             ValidateLifetime         = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer              = jwtSection["Issuer"],
//             ValidAudience            = jwtSection["Audience"],
//             IssuerSigningKey         = new SymmetricSecurityKey(
//                                            Encoding.UTF8.GetBytes(secretKey))
//         };
//     });

