using Business.DependencyResolvers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//IocContainer 
builder.Services.AddBusinessService();   //.net in kendi Ioc Container�n� kulllan�r�z.//DependencyEnjection.cs 'de olu�turugum metotu ekliyorum
builder.Services.ServisRelationShip();  //.net in kendi IoC Container�nda katmanlar�n� ilil�ksini yazd���m extention metotu ekliyorum //ServisRelationShip() -> ServiceExtention.cs de olu�turdu�um metot


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