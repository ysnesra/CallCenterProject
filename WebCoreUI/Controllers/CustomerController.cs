using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Entities.Concrete;

namespace WebCoreUI.Controllers
{
    // [Authorize(Roles ="customer")]
    public class CustomerController : Controller
    {
        ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CustomerController(ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            this._httpContextAccessor = httpContextAccessor;
        }
        //Müşteri Ekranı 
        public IActionResult Profile()
        {
            return View();
        }
        
        //ÜyeOl Ekranı formu
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(CustomerRegisterDto model)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomerDto(model);
                return RedirectToAction(nameof(Login));
            }

            ModelState.AddModelError("", "Üye olma işlemi gerçekleşmedi");
            return View(model);
        }

        //Giriş Ekranı formu
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        #region JwtToken_LoginOlma
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Login(CustomerLoginDto model)
        //{
        //    //Mail ve şifre databasede var mı
        //    var customerdb = _customerService.GetByLoginFilter(model);

        //    if (customerdb == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    _httpContextAccessor.HttpContext.Response.Cookies.Append("token", customerdb.Token , new CookieOptions()
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.Now.AddDays(1),
        //        Secure = true,
        //        IsEssential = true,
        //        SameSite = SameSiteMode.None,
        //    }); // Oluştuyrulan tokeni cookie ye basmak için kullanılır 


        //    //eğer çağrı merkezi personeli ise // x viewine eğer müşteri ise y viewine redirect ediceksin 
        //    if (customerdb.Role == "customerRep")
        //    {
        //        return RedirectToAction("Index", "CustomerRep");

        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Customer");
        //    }

        //} 
        #endregion

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(CustomerLoginDto model)
        {
            //Mail ve şifre eşleşiyor mu
            var existCustomer = _customerService.GetByLoginFilter(model);
            if (existCustomer == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //Claims listesi oluşturp cookide neleri tutulacağı tanımlandı
            //ClaimTypes.... diyerek hazır claim nesnelerinde tutar
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, existCustomer.CustomerId.ToString()));
            claims.Add(new Claim("Email", existCustomer.Email));
            claims.Add(new Claim("Password", existCustomer.Password));
            claims.Add(new Claim(ClaimTypes.Role, existCustomer.Role));

            //ClaimsIdentity nesnesi içerisine claimleri ekliyor.Hangi Authentication ı kullanılıyorsa onu da parametre olarak verilir//CookieAuthentication
            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme
                );
            //ClaimsPrincipal bizden ClaimsIdentity nesnemizi ister
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");


            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  //Cookieyi kapat
            return RedirectToAction(nameof(Login));
        }
    }
}
