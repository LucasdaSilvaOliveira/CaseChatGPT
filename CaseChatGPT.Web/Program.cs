using AutoMapper;
using CaseChatGPT.Infra.Context;
using CaseChatGPT.Web.AutoMapper;
using CaseChatGPT.Web.Services.Auth;
using CaseChatGPT.Web.Services.Produto;
using CaseChatGPT.Web.Services.Usuario;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BancoContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme);

// =========================== CONFIGURAÇÃO ALTERNATIVA PARA AUTENTICAÇÃO ===========================
//var key = Encoding.UTF8.GetBytes("secretkeytestsecretkeytestsecretkeytestsecretkeytest");
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(options =>
//    {
//        options.RequireHttpsMetadata = false; // só desative em dev
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false) // EXIGIR EMAIL NA CONTA
    .AddEntityFrameworkStores<BancoContext>()
    .AddDefaultTokenProviders();

// CONFIG AUTO MAPPER
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new ProdutoProfile());
    mc.AddProfile(new UsuarioProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// CONFIGURAÇÃO DO HTTPCLIENT
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// CONFIGURAÇÃO DO HTTPCLIENT
string? httpClientName = builder.Configuration["HttpClientName"]!;

builder.Services.AddHttpClient(httpClientName, client =>
{
    client.BaseAddress = new Uri("https://localhost:7085/api/");
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// CONFIGURAÇÃO DO HTTPCLIENT
app.UseSession();

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
