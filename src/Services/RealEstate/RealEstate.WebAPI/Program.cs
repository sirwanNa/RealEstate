using RealEstate.WebAPI.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddOpenApi()
    .RegisterDbContext()
    .RegisterRepositories()
    .ConfigureMediatR("RealEstate.Application")
    .ConfigureAutoMapper()
    .ConfigureGlobalFilters()
    .RegisterJWTToken(builder);



builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:1610") // React dev server
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});



var app = builder.Build();



app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await app.AutoMigration();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
app.MapRazorPages();

app.Run();
