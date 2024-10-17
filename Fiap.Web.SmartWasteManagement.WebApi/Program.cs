using AutoMapper;
using Fiap.Web.SmartWasteManagement.Data.Contexts;
using Fiap.Web.SmartWasteManagement.Data.Repository;
using Fiap.Web.SmartWasteManagement.Models;
using Fiap.Web.SmartWasteManagement.Services;
using Fiap.Web.SmartWasteManagement.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString)
);
#endregion

#region Registro IServiceCollection

builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();
builder.Services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddScoped<IMoradorService, MoradorService>();
builder.Services.AddScoped<IMoradorRepository, MoradorRepository>();
builder.Services.AddScoped<INotificacaoService, NotificacaoService>();
builder.Services.AddScoped<INotificacaoRepository, NotificacaoRepository>();
builder.Services.AddScoped<IRecipienteService, RecipienteService>();
builder.Services.AddScoped<IRecipienteRepository, RecipienteRepository>();
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();
builder.Services.AddScoped<IAgendamentoColetaService, AgendamentoColetaService>();
builder.Services.AddScoped<IAgendamentoColetaRepository, AgendamentoColetaRepository>();

#endregion

#region Automapper

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AllowNullCollections = true;
    cfg.AllowNullDestinationValues = true;
    cfg.CreateMap<CaminhaoModel, CaminhaoViewModel>().ReverseMap();
    cfg.CreateMap<MoradorModel, MoradorViewModel>().ReverseMap();
    cfg.CreateMap<NotificacaoModel, NotificacaoViewModel>().ReverseMap();
    cfg.CreateMap<RecipienteModel, RecipienteViewModel>().ReverseMap();
    cfg.CreateMap<RotaModel, RotaViewModel>().ReverseMap();
    cfg.CreateMap<AgendamentoColetaModel, AgendamentoColetaViewModel>().ReverseMap();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

#endregion

#region Auth
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RmlhcFNtYXJ0V2FzdGVNYW5hZ2VtZW50")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
