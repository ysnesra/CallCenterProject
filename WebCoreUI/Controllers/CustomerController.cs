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
        public IActionResult Index()
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

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(CustomerLoginDto model)
        {
            //Mail ve şifre databasede var mı
            var customerdb = _customerService.GetByLoginFilter(model);

            if (customerdb == null)
            {
                return RedirectToAction(nameof(Login));
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("token", customerdb.Token); // Oluştuyrulan tokeni cookie ye basmak için kullanılır 


            //eğer çağrı merkezi personeli ise // x viewine eğer müşteri ise y viewine redirect ediceksin 
            if (customerdb.Role == "customerRep")
            {
                return RedirectToAction("Index", "CustomerRep");

            }
            else
            {
                return RedirectToAction("Index", "Customer");
            }


        }


        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);  //Cookieyi kapat
            return RedirectToAction(nameof(Login));
        }
    }
}
