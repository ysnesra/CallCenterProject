using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        //Admin tarafta Müşteriler Listesi
        public IActionResult GetAllCustomer()
        {
            var response = _adminService.GetAllCustomerList();
            return View(response);
        }

        
        //Admin tarafta Müşterinin Talepleri
        public IActionResult GetRequestListByCustomerId(int customerId)
        {            
            ViewBag.CustomerName = _adminService.GetByIdCustomerName(customerId);
            var response = _adminService.GetRequestsByCustomerId(customerId);
            return View(response);
        }


        //Admin tarafta Müşteririnin Taleplerini Asenkron getirme
        [HttpGet]
        public async Task<IActionResult> GetRequestListByCustomerIdAsync(int id)
        {
            ViewBag.CustomerName = await _adminService.GetByIdCustomerNameAsync(id);
            var response = _adminService.GetRequestsByCustomerId(id);
            return View(response);
            
        }
    }
}
