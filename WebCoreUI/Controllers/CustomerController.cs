using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreUI.Controllers
{
    public class CustomerController : Controller
    {
        ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult List()
        {
            return View();
        }

        //ÜyeOl Ekranı formu
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
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
        public IActionResult Login()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromBody] CustomerLoginDto model)
        {
            //if (ModelState.IsValid)
            //{
                //Mail ve şifre databasede var mı
                var customerdb = _customerService.GetByLoginFilter(model);
               Console.WriteLine(customerdb);
                if (customerdb == null)
                {
                    return RedirectToAction(nameof(Login));
                }

                //return View (customerdb);
               // Eğer giriş başarılıysa, veriyi JSON olarak döndürüyoruz
                return Json(new { success = true, customerdb = customerdb });
            //}
            //// eğer giriş başarısız olursa, hata mesajlarını JSON verisi olarak gönderiyoruz
            //var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            //return Json(new { success = false, errors = errors });
        }
    }
}
