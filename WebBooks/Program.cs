using Infrastructure.Data;
using Domin.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domin.Entity;
using Infrastructure.IReprository;
using Infrastructure.IReprository.ServicesRepository;
using Infrastructure.Seeds;

var builder = WebApplication.CreateBuilder(args);


//for sqlconnection :
var connectionString = builder.Configuration.GetConnectionString("BookConnection");

builder.Services.AddDbContext<FreeBookDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
//                .AddEntityFrameworkStores<FreeBookDbContext>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<FreeBookDbContext>();


//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredUniqueChars = 0;
//    options.Password.RequiredLength = 5;
//    options.Password.RequireNonAlphanumeric = false;
//});


// Add services to the container.


builder.Services.AddScoped<IServicesReprository<Category>, ServicesCategory>();

builder.Services.AddScoped<IServicesReprositoryLog<LogCategory>, ServicesLogCategory>();

// Set the path to redirect unauthenticated users to the custom login page at /Admin
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin";
});

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var app = builder.Build();


// Seed roles and users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        await DefaultRole.SeedAsync(roleManager);
        await DefaultUser.SeedSuperAdminAsync(userManager, roleManager);
        await DefaultUser.SeedBasicUserAsync(userManager, roleManager);
    }
    catch(Exception)
    {
        throw;
    }  
}



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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
