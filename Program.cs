using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioBackend.Data;
using PortfolioBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<JwtService>();

// Optimized: AddDbContext ko AddDbContextPool mein badal diya taake instances reuse hon aur API fast chale
builder.Services.AddDbContextPool<AppDbContext>(options =>
{
    var connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException(
            "Database connection string is missing."
        );
    }

    options.UseNpgsql(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://az-developers.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("JWT key is missing.");
}

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
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
                Encoding.UTF8.GetBytes(jwtKey)
            ),

            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Production aur Development dono ke liye HTTPS secure redirection automatically manage ho sakti hai
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new
{
    status = "Portfolio CMS Backend is running",
    environment = app.Environment.EnvironmentName,
    timestamp = DateTime.UtcNow
}));

app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy"
}));

app.MapControllers();

app.Run();