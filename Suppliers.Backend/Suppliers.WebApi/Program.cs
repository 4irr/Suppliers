using Suppliers.Application;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Interfaces;
using Suppliers.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ISuppliersDbContext).Assembly));
});

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<SuppliersDbContext>();
        DbInitializer.Initialize(context);
    }
    catch(Exception)
    {

    }
}

app.Run();