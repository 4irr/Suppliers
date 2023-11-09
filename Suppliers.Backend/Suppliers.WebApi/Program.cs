using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Suppliers.Application;
using Suppliers.Application.Common.Mappings;
using Suppliers.Application.Interfaces;
using Suppliers.Persistence;
using Suppliers.WebApi;
using Suppliers.WebApi.Middleware;
using Suppliers.WebApi.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ISuppliersDbContext).Assembly));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddUsersHttpClientServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7073";
        options.Audience = "SuppliersWebAPI";
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSession();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix= string.Empty;
    config.SwaggerEndpoint("swagger/Suppliers API/swagger.json", "Suppliers API");
});
app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

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