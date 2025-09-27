using Grupo04.Custom;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Models;
using Services.Interfaces;
using Services.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


//Conexion con el Front
var MyAllowSpecificOrigins = "_myAlloeSpecificOrigins";

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3000/")
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});


builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:ClavePrivada"]!)),
    };
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DonTito API", Version = "v1" });

    // Configuración para JWT en Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});



//BDContext
builder.Services.AddDbContext<comunidadsolidariaContext>(option =>
option.UseNpgsql(builder.Configuration.GetConnectionString("Conection")));

//Services
builder.Services.AddScoped<IChatServer,ChatService>();
builder.Services.AddScoped<IDetalleDonacionService,DetalleDonacionService>();
builder.Services.AddScoped<IDonacionService, DonacionService>();
builder.Services.AddScoped<IEnvioServer,EnvioService>();
builder.Services.AddScoped<IEstadoTipoServer,EstadoTipoService>();
builder.Services.AddScoped<ILocalidadService,LocalidadService>();
builder.Services.AddScoped<IMensajeService,MensajeService>();
builder.Services.AddScoped<INotificacionService,NotificacionService>();
builder.Services.AddScoped<INovedadService, NovedadService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();
builder.Services.AddScoped<IProvinciaService, ProvinciaService>();
builder.Services.AddScoped<IPublicacionService, PublicacionService>();
builder.Services.AddScoped<ITipoDonacionService, TipoDonacionService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddSingleton<Utilidades>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("_myAlloeSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
