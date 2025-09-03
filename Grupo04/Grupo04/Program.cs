using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using Services.Services;

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


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



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
