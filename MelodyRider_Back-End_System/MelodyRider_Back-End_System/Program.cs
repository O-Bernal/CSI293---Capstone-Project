using MelodyRider_Back_End_System.Data;
using MelodyRider_Back_End_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registers the DbContext and passes the connection string
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registers the Identity framework
builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<GameDbContext>()
	.AddDefaultTokenProviders();

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 0;
});

// Add controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Enable serving of static files
app.UseDefaultFiles();

// Configure static file options
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".br"] = "application/wasm";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider,
    OnPrepareResponse = ctx =>
    {
        var path = ctx.File.PhysicalPath;
        if (path.EndsWith(".br"))
        {
            ctx.Context.Response.Headers["Content-Encoding"] = "br";
        }
    }
});

// Enable routing
app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

//// Ensure admin user is created
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    await EnsureAdminUserCreated(services);
//}

app.Run();

//// Method to ensure admin user is created
//static async Task EnsureAdminUserCreated(IServiceProvider services)
//{
//    var userManager = services.GetRequiredService<UserManager<User>>();
//    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//    var adminRole = "Admin";
//    if (!await roleManager.RoleExistsAsync(adminRole))
//    {
//        await roleManager.CreateAsync(new IdentityRole(adminRole));
//    }

//    var adminUser = new User
//    {
//        UserName = "Admin",
//        Email = "admin@example.com",
//        Role = "Admin"
//    };

//    var user = await userManager.FindByNameAsync(adminUser.UserName);
//    if (user == null)
//    {
//        var result = await userManager.CreateAsync(adminUser, "AdminPa55");
//        if (result.Succeeded)
//        {
//            await userManager.AddToRoleAsync(adminUser, adminRole);
//        }
//    }
//}