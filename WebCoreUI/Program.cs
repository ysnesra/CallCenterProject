using Business.DependencyResolvers;
using Business.Utilities.Security.Encryption;
using Business.Utilities.Security.JWT;
using DataAccess.Concrete.Entityframework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//IocContainer 
builder.Services.AddBusinessService();   //.net in kendi Ioc Container�n� kulllan�r�z.//DependencyEnjection.cs 'de olu�turugum metotu ekliyorum
builder.Services.ServisRelationShip(builder.Configuration);  //.net in kendi IoC Container�nda katmanlar�n� ilil�ksini yazd���m extention metotu ekliyorum //ServisRelationShip() -> ServiceExtention.cs de olu�turdu�um metot

#region JwtToken
//JWTBearer kullan�laca�� belirtilir 
// Add configuration
//builder.Configuration.AddJsonFile("appsettings.json");
//var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddCookie(X=> X.Cookie.Name ="token")
//    .AddJwtBearer(options =>
//    {
//        options.SaveToken = true;
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidIssuer = tokenOptions.Issuer,
//            ValidAudience = tokenOptions.Audience,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
//        };
//        options.Events = new JwtBearerEvents
//        {

//            OnMessageReceived = context =>
//            {
//                context.Token = context.Request.Cookies["token"];
//                return Task.CompletedTask;
//            },

//            OnAuthenticationFailed = context =>
//            {
//                context.Response.Redirect("/Home/AccessDenied");
//                return Task.CompletedTask;
//            }
//        };

// }); 
#endregion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = ".CallCenterProject.auth";  //Kullan�c�n�n taray�c�s�nda cookie bilgilerini bu isimde sakla
        opts.ExpireTimeSpan = TimeSpan.FromDays(20);     //ne kadar s�re sonra bu cookie ge�ersiz olacak. 7 g�nde 1 yenilensin
        opts.LoginPath = "/Customer/Login";   //Cookie yi bulamazsa LoginPath e y�nledirmesi gerekiyor//Login de�ilse buraya atacak
        opts.LogoutPath = "/Customer/Logout";  //��k�� yapma
        opts.AccessDeniedPath = "/Home/AccessDenied";   //Yetkisi olmad���nda gidece�i sayfa//Rol� uymuyorsa bu sayfaya at�yor
    });

builder.Services.AddAuthorization();

//***Http yap�s�n� b�t�n katmanalarda kullanmam�z� sa�lar*******
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
