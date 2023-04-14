using Microsoft.EntityFrameworkCore;
using PearReview.Data;
using Microsoft.AspNetCore.Identity;
using PearReview.Areas.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<WeatherForecastService>();

string? connString = builder.Configuration.GetConnectionString("Default");
if (connString != null)
{
    builder.Services.AddDbContext<DataContext>(
        opt =>
        {
            opt.UseSqlServer(connString);
            opt.EnableDetailedErrors();
            opt.EnableSensitiveDataLogging();
        }
    );

}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddTransient<CoursesService>();

builder.Services.AddScoped<TokenProvider>();

var app = builder.Build();

using (var context = app.Services.CreateScope().ServiceProvider.GetService<DataContext>())
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
