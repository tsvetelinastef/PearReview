using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PearReview.Areas.Courses.Services;
using PearReview.Areas.Identity;
using PearReview.Areas.Identity.Data;
using PearReview.Areas.Identity.Services;
using PearReview.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<WeatherForecastService>();

string? connString = builder.Configuration.GetConnectionString("Default");
if (connString != null)
{
    builder.Services.AddDbContextFactory<AppDbContext>(
        opt =>
        {
            opt.UseSqlServer(connString);
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
        }
    );
}

builder.Services.AddDefaultIdentity<AppUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
    // Use a custom UserStore that uses the DbContextFactory and creates a new DbContext for reach request.
    // This avoids getting the following error since the same DbContext instance is not used by mutliple threads:
    // "InvalidOperationException: A second operation started on this context before a previous operation completed. This is usually caused by different threads using the same instance of DbContext.'
    //.AddUserStore<AppUserStore>();

//builder.Services.AddScoped<AuthenticationStateProvider, AppAuthStateProvider<AppUser>>();

// Transient = new instance every time one is requested
// Scoped = new instance per client request but in Blazor there's only one request - the one that establishes the WebSockets connection
// This means that scope services will live as long as the connection lives which is until a manual reload (manual uri change is a reload)
// Scoped or Transient?
builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddTransient<UsersService>();
builder.Services.AddTransient<CoursesService>();
builder.Services.AddTransient<ResourcesService>();

builder.Services.AddScoped<TokenProvider>();

var app = builder.Build();

using (var context = app.Services.CreateScope().ServiceProvider.GetService<AppDbContext>())
{
    if (context != null)
    {
        // Creates the db if it doesn't exist and applies the added migrations to it. More migrations are allow after that.
        context.Database.Migrate();

        // Creates the db if it doesn't exist but does not allow updates with migrations after that.
        //context.Database.EnsureCreated();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
