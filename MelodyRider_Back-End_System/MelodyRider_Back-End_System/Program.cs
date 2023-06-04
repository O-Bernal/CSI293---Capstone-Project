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

// Map controllers
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
