using Business.Abstract;
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
        public IActionResult Login(CustomerLoginDto model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}
