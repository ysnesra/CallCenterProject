using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System.Security.Claims;

namespace WebCoreUI.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("[controller]/[action]")]
    public class AdminController : Controller
    {
        IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }  
        
        // AdminLayout.cshtml      
        [HttpGet]
        public IActionResult AdminIndex()
        {
            return View();
        }

        //Admin tarafta Müşteriler Listesi
        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            var response = _adminService.GetAllCustomerList();
            return View(response);
        }

        //Admin tarafta Müşterinin Talepleri
        [HttpGet]
        public IActionResult GetRequestListByCustomerId(int customerId)
        {            
            ViewBag.CustomerName = _adminService.GetByIdCustomerName(customerId);
            var response = _adminService.GetRequestsByCustomerId(customerId);
            return View(response);
        }

        //Admin tarafta Müşteririnin Taleplerini Asenkron getirme
        //[HttpGet]
        //public async Task<IActionResult> GetRequestListByCustomerIdAsync(int customerId)
        //{
        //    ViewBag.CustomerName = await _adminService.GetByIdCustomerNameAsync(customerId);
        //    var response = _adminService.GetRequestsByCustomerId(customerId);
        //    return View(response);

        //}


        //Admin tarafta MüşteriTemsilcilerini Listeleme
        [HttpGet]
        public IActionResult GetAllCustomerRep()
        {
            var response = _adminService.GetAllCustomerRepList();
            return View(response);
        }

        //Müşteri Temsilcisi Kayıt Ekran Formu
        [AllowAnonymous]
        public IActionResult CreateCustomerRep()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateCustomerRep(CustomerRepRegisterDto model)
        {
            if (ModelState.IsValid)
            {
                _adminService.AddCustomerRepDto(model);
                return RedirectToAction(nameof(GetAllCustomerRep));
            }

            ModelState.AddModelError("", "Üye olma işlemi gerçekleşmedi");
            return View(model);
        }

        [HttpGet]
        //[Route("{customerId?}/{customerRepId?}")]
        public IActionResult EditCustomerRep(int customerId)
        {
            CustomerRepEditDto model = _adminService.GetCustomerRepByCustomerId(customerId);

            //ViewBag.CustomerId = customerId;
            //ViewBag.CustomerRepId = customerRepId;

            return View(model);
        }
        [HttpPost]
        //[Route("{customerId?}/{customerRepId?}")]
        public IActionResult EditCustomerRep(CustomerRepEditDto model)
        {
            _adminService.EditCustomerRepDto(model);
            return RedirectToAction(nameof(GetAllCustomerRep));
        }

        public IActionResult DeleteCustomerRep(int customerId)
        {
            _adminService.DeleteCustomerRepDto(customerId);
            return RedirectToAction(nameof(GetAllCustomerRep));
        }

        //Müşteri Temsilcisi Rapor Ekranı
        public IActionResult GetReport()
        {        
            return View(_adminService.GetReportList());
        }
    }
}
