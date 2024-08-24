using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProjectCleanArchiticture.Core;
using SchoolProjectCleanArchiticture.Core.Filter;
using SchoolProjectCleanArchiticture.Core.Middleware;
using SchoolProjectCleanArchiticture.Data;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
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
    .AddServicesCore()
    .JwtRegistering(builder.Configuration)
    .EmailServiceReg(builder.Configuration);
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

#region Identity
builder.Services.AddIdentity<SUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
#endregion

#region addUrlAction
builder.Services.AddTransient<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});
#endregion

builder.Services.AddTransient<AuthenticationActionFilter>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scope = app.Services.CreateScope())
{
  var userManager=  scope.ServiceProvider.GetRequiredService<UserManager<SUser>>();
   var roleManager=scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await IdentitySeeder.SeedRole(roleManager);
    await IdentitySeeder.SeedUser(userManager);

}

var options=app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseMiddleware<SchoolProjectCleanArchiticture.Core.Middleware.ErrorHandlingMiddleware>();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors(CorsName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
