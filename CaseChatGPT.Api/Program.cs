using AutoMapper;
using CaseChatGPT.App.Mappings;
using CaseChatGPT.App.UseCases;
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
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// ==================

builder.Services.AddScoped<ProdutoUseCases>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
         .AddEntityFrameworkStores<BancoContext>()
         .AddDefaultTokenProviders();

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
