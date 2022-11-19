using EticaretWebCoreEntity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    switch (builder.Configuration.GetValue<string>("Application:DatabaseProvider"))
    {
        case "Mysql":
            options.UseMySql(
            builder.Configuration.GetConnectionString("Mysql"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Mysql")),
            config =>
            {
                config.MigrationsAssembly("EticaretWebCoreMigrationMysql");

            }).UseLazyLoadingProxies();
            break;
        case "SqlServer":
        default:
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("SqlServer"),
                config =>
                {
                    config.MigrationsAssembly("EticaretWebCoreMigrationSqlServer");
                }).UseLazyLoadingProxies();
            break;
    }
});



builder.Services.AddAuthentication(opt => { opt.DefaultScheme = "UyeAuth"; })
                  .AddCookie("AdminAuth", opt => { opt.Cookie.SameSite = SameSiteMode.None; opt.SlidingExpiration = true; opt.Cookie.MaxAge = opt.ExpireTimeSpan = TimeSpan.FromDays(7); opt.LoginPath = "/Admin/Account/GirisYap"; opt.LogoutPath = "/Admin/Account/CikisYap"; opt.AccessDeniedPath = "/Account/AccessDenied"; })
                  .AddCookie("UyeAuth", opt => { opt.Cookie.SameSite = SameSiteMode.None; opt.SlidingExpiration = true; opt.Cookie.MaxAge = opt.ExpireTimeSpan = TimeSpan.FromDays(7); opt.LoginPath = "/Account/GirisYap"; opt.LogoutPath = "/Account/CikisYap"; opt.AccessDeniedPath = "/Account/AccessDenied"; });





builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews()
.AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
});


builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());

builder.Services.AddSingleton<IFileProvider>(physicalProvider);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseBrowserLink();

}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    //HTTPS YE YÖNLENDÝRMEK ÝÇÝN AÞAÐIDAKÝ KODLARI AKTÝF ET

    //var options = new RewriteOptions();
    //options.AddRedirectToHttps();
    //options.Rules.Add(new RedirectToWwwRule());
    //app.UseRewriter(options);
}

app.UseHttpsRedirection();

app.UseSession();

//app.UseBrowserLink();

app.UseSession();

app.UseDefaultFiles();
app.UseStaticFiles(); // shortcut for HostEnvironment.WebRootFileProvider
app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = new PathString("/filemanager"),
    FileProvider = new PhysicalFileProvider(Path.GetFullPath(Path.Combine(Assembly.GetEntryAssembly().Location, "../filemanager"))),
});

app.UseResponsiveFileManager();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseCookiePolicy();


//Proje baþlayýnca tablolarý otomatik oluþturma
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dataContext.Database.Migrate();
}
//Proje baþlayýnca tablolarý otomatik oluþturma

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
