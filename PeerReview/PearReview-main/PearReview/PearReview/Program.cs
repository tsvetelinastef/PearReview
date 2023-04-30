using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
 
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components;
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
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<AppUser>>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
    options.TokenLifespan = TimeSpan.FromHours(3));

// Add using directive for Microsoft.AspNetCore.Components
builder.Services.AddScoped<NavigationManager>();

var app = builder.Build();

if (app.Services.GetService<IWebHostEnvironment>().IsDevelopment())
{
    //app.UseWebAssemblyDebugging();
}

// UseHttpsRedirection and UseStaticFiles are optional
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.RunAsync();
