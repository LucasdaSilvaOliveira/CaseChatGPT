using AutoMapper;
using CaseChatGPT.App.Mappings;
using CaseChatGPT.App.UseCases;
using CaseChatGPT.Domain.Entities;
using CaseChatGPT.Domain.Interfaces.UseCases;
using CaseChatGPT.Infra.Context;
using CaseChatGPT.Infra.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfraDependencies(builder.Configuration);

// CONFIG AUTO MAPPER
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProdutoProfile());
    mc.AddProfile(new PedidoProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// ==================

builder.Services.AddScoped<IProdutoUseCases, ProdutoUseCases>();
builder.Services.AddScoped<IPedidoUseCases, PedidoUseCases>();
builder.Services.AddScoped<IUsuarioUseCases, UsuarioUseCases>();

builder.Services.AddIdentity<Usuario, IdentityRole>()
         .AddEntityFrameworkStores<BancoContext>()
         .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

//builder.Services.AddIdentityApiEndpoints<Usuario>()

var app = builder.Build();

//    .AddEntityFrameworkStores<BancoContext>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//app.MapSwagger().RequireAuthorization();
//app.MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
