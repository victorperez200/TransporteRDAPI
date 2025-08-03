using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TransporteDigitalRD.Application.UseCases;
using TransporteDigitalRD.Infraestructure.Interfaces;
using TransporteDigitalRD.Infraestructure.Repositories;
using TransporteDigitalRD.Infrastructure.Services;
using TransporteDigitalRD.Data;

namespace TransporteDigitalRD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connString = "Server=gfedo.database.windows.net;" +
        "Database=TransRD;" +
        "User Id=gfedo;" +
        "Password=2210Gabi#;";
            var builder = WebApplication.CreateBuilder(args);

            // 🧩 Inyección de dependencias
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<UsuariosService>();
            builder.Services.AddScoped<ViajesService>();
            builder.Services.AddScoped<TipoTransporteService>();
            builder.Services.AddAntiforgery(); // Opcional en APIs
            builder.Services.AddTransient<TransRDDataContext>(_ => new TransRDDataContext(connString));

            // 🔐 Configurar autenticación con JWT
            var jwtKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT Key is missing in appsettings.json");

            var key = Encoding.UTF8.GetBytes(jwtKey);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // ✅ Swagger con soporte para JWT
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce el token JWT como: **Bearer &lt;tu_token&gt;**"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // 🌐 Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // debe ir antes de UseAuthorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
