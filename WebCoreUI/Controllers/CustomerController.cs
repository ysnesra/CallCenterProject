using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebCoreUI.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
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
        public IActionResult Login([FromBody] CustomerLoginDto model)
        {  
            //Mail ve şifre databasede var mı
            var customerdb = _customerService.GetByLoginFilter(model);
              
            if (customerdb == null)
            {
                return RedirectToAction(nameof(Login));
            }

            //return View (customerdb);
            // Eğer giriş başarılıysa, veriyi JSON olarak döndürüyoruz
            return Json(new { success = true, customerdb = customerdb });           
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);  //Cookieyi kapat
            return RedirectToAction(nameof(Login));
        }
    }
}
