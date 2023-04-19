using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreUI.Controllers
{
    public class CallController : Controller
    {
        ICallService _callService;

        public CallController(ICallService callService)
        {
            _callService = callService;
        }

        //Müşteri Temsilcisi Görüşme Form Ekranı
        [HttpGet]
        public IActionResult CallCreate(int requestId)
        {
            CallDto callDto = new CallDto();
            return View(callDto);     
        }
        [HttpPost]
        public IActionResult CallCreate(CallDto model)
        {
            //Claimdeki: 
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string emailForClaim = User.FindFirstValue("Email");

            _callService.AddCallDto(model, customerId, emailForClaim);
            return View("ProfileCustRep", "CustomerRep");
        }
    }
}
