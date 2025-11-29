using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PassportOffice.Models; 
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebAppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 41))));

// Регистрация IHttpContextAccessor для использования HttpContext
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Настройка хранилища Session и Cookies
builder.Services.AddDistributedMemoryCache(); // Используем память сервера для хранения сессии
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Только сервер имеет доступ к cookies
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Использование HTTPS
});

// Аутентификация через файлы cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login"; // Куда переадресовать, если нет авторизации
        options.LogoutPath = "/User/Logout"; // Логаута пока нет, допишем позже
        options.AccessDeniedPath = "/AccessDenied"; // Страница запрета доступа
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

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

app.UseSession();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
