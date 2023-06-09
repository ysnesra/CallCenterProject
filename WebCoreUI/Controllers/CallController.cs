﻿using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Core.Utilities.Helpers;

namespace WebCoreUI.Controllers
{
    [Route("Call/CallCreate/")]
    public class CallController : Controller
    {
        ICallService _callService;

        public CallController(ICallService callService)
        {
            _callService = callService;
        }
        
        [HttpGet]
        [Route("{requestId?}/{customerId?}")]
        public IActionResult CallCreate(int requestId,int customerId)
        {
            CallDto callDto = new CallDto();

            ViewBag.RequestId = requestId;
            ViewBag.CustomerId = customerId;
          
            return View(callDto);
        }

        [HttpPost]
        [Route("{requestId?}/{customerId?}")]
        public IActionResult CallCreate(CallDto model)
        {
            ModelState.Remove("CallId");            
            ModelState.Remove("CustomerRepId");
            if (ModelState.IsValid)
            {           
                string emailForClaim = User.FindFirstValue("Email");

                _callService.AddCallDto(model, emailForClaim);

                string email = _callService.CallwithCustomer(model.CustomerId);

                //Müşteriye otomatik olarak görüşme bilgilendirme Maili gönderme
                EmailToInformation emailHelper = new EmailToInformation();
                emailHelper.SendEmail(email);

                return Redirect("/CustomerRep/ProfileCustRep");

            }
            return View(model);
 
        }     
    }
}
