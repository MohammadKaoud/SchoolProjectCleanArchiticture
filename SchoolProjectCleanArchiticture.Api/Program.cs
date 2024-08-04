using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProjectCleanArchiticture.Core;
using SchoolProjectCleanArchiticture.Core.Middleware;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Infrastructure;
using SchoolProjectCleanArchiticture.Services;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configuration for Addind DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Di Container 
#region DependencyInjection

builder.Services
    .AddService()
    .AddServicesInfrastructure()
    .AddServicesCore();
#endregion

#region Localization

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> SupportedCulters = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG")
    };
    options.SupportedCultures=SupportedCulters;
    options.SupportedUICultures=SupportedCulters;
    options.DefaultRequestCulture=new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
});


#endregion
# region AddCors
var CorsName = "KaoudCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsName, policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
        
    });
}); 

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var options=app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseMiddleware<SchoolProjectCleanArchiticture.Core.Middleware.ErrorHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors(CorsName);

app.UseAuthorization();

app.MapControllers();

app.Run();
