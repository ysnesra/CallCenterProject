using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
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
        
        [HttpGet]
        [Route("Call/CallCreate/{requestId?}/{customerId?}")]
        public IActionResult CallCreate(int requestId,int customerId)
        {
            CallDto callDto = new CallDto();

            ViewBag.RequestId = requestId;
            ViewBag.CustomerId = customerId;
          
            return View(callDto);
        }

        [HttpPost]
        [Route("Call/CallCreate/{requestId?}/{customerId?}")]
        public IActionResult CallCreate(CallDto model)
        {
            ModelState.Remove("CallId");
            ModelState.Remove("CustomerId");
            ModelState.Remove("CustomerRepId");
            if (ModelState.IsValid)
            {           
                string emailForClaim = User.FindFirstValue("Email");

                _callService.AddCallDto(model, emailForClaim);               
                return Redirect("/CustomerRep/ProfileCustRep");

            }
            return View(model);
 
        }     
    }
}
