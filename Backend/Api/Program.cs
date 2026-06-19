using Api.EndPoints;
using Api.MiddleWare;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Récupérer la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

// Ajouter les dépendances Core et Infrastructure
builder.Services.AddCoreDependencies();
builder.Services.AddInfrastructureDependencies(connectionString);

// Configurer JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// Configurer CORS pour Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();

// Enregistrer tous les endpoints
app.MapJoueursEndPoints();
app.MapEquipesEndPoints();
app.MapMatchsEndPoints();
app.MapStaffEndPoints();
app.MapContratsEndPoints();
app.MapEntrainementsEndPoints();
app.MapPresencesEndPoints();
app.MapStatistiquesEndPoints();
app.MapBlessuresEndPoints();
app.MapAuthEndPoints();

app.Run();
