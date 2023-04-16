using Business.DependencyResolvers;
using Business.Utilities.Security.Encryption;
using Business.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//IocContainer 
builder.Services.AddBusinessService();   //.net in kendi Ioc Containerýný kulllanýrýz.//DependencyEnjection.cs 'de oluþturugum metotu ekliyorum
builder.Services.ServisRelationShip();  //.net in kendi IoC Containerýnda katmanlarýný ililþksini yazdýðým extention metotu ekliyorum //ServisRelationShip() -> ServiceExtention.cs de oluþturduðum metot


//JWTBearer kullanýlacaðý belirtilir 
// Add configuration
builder.Configuration.AddJsonFile("appsettings.json");
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                context.Response.Redirect("/Home/AccessDenied");
                return Task.CompletedTask;
            }
        };

    });

//***Http yapýsýný bütün katmanalarda kullanmamýzý saðlar*******
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
