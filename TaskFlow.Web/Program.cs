using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using TaskFlow.Data;
using TaskFlow.Data.Models;
using TaskFlow.Data.Repository.Interfaces;
using TaskFlow.Services.Interfaces;
using TaskFlow.Web.Infrastructure.Identity;
using TaskFlow.Web.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<IEmailSender, DevelopmentEmailSender>();
builder.Services
    .AddRepositories(typeof(ITeamRepository).Assembly)
    .AddUserDefinedServices(typeof(ITeamService).Assembly);

var app = builder.Build();

await IdentityRoleSeeder.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
