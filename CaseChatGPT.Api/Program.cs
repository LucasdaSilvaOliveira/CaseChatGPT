using AutoMapper;
using CaseChatGPT.App.Mappings;
using CaseChatGPT.App.UseCases;
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

//builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//         .AddEntityFrameworkStores<BancoContext>()
//         .AddDefaultTokenProviders();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<BancoContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//app.MapSwagger().RequireAuthorization();
app.MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
