using Business.Abstract;
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

        //ÜyeOl formu
        public IActionResult Register()
        {
            return View();
        }

        //GirişYap formu
        public IActionResult Login()
        {
            return View();
        }
    }
}
