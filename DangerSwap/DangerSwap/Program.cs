using DangerSwap.DbContexts;
using DangerSwap.Interfaces;
using DangerSwap.Models;
using DangerSwap.Repositories;
using DangerSwap.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

ConfigurationManager configurations = builder.Configuration;

builder.Services.AddDbContext<DangerSwapContext>(
    options => options.UseSqlite
    (configurations.GetConnectionString("DangerSwapContext")
    ));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DangerSwapContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IConverterRepository, ConverterRepository>();
builder.Services.AddScoped<ICapitalRepository, CapitalRepository>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IConverterService, ConverterService>();
builder.Services.AddScoped<ICapitalService, CapitalService>();
builder.Services.AddSingleton<IScrapperService, ScrapperService>(config =>
new ScrapperService(configurations));
builder.Services.ConfigureApplicationCookie(config =>
{
    double expirationTimeSeconds;
    try
    {
        expirationTimeSeconds = double.Parse(configurations["SessionTime"]);
    }
    catch (Exception)
    {
        expirationTimeSeconds = 3600;
    }
    config.ExpireTimeSpan = TimeSpan.FromSeconds(expirationTimeSeconds);
    config.LoginPath = "/Authorization/Login";
    config.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Converter/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Converter}/{action=Index}/{id?}");

app.Run();
