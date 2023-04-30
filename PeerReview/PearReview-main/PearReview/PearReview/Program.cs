using PearReview;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PearReview.Areas.Courses.Services;
using PearReview.Areas.Identity;
using PearReview.Areas.Identity.Data;
using PearReview.Areas.Identity.Services;
using PearReview.Data;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("app");

builder.Services.AddSingleton(
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

string connString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(
    opt =>
    {
        opt.UseSqlServer(connString);
        opt.EnableDetailedErrors();
        opt.EnableSensitiveDataLogging();
    });

builder.Services.AddDefaultIdentity<AppUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<CoursesService>();
builder.Services.AddSingleton<TokenProvider>();


builder.Services.AddScoped<AuthenticationStateProvider>(
    sp => sp.GetRequiredService<RevalidatingIdentityAuthenticationStateProvider<AppUser>>());
builder.Services.AddScoped<NavigationManager>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(3));

var app = builder.Build();

if (app.Services.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

if (!app.Services.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();
}

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");


await app.RunAsync();
