using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebCoreUI.Controllers
{
    public class RequestController : Controller
    {
        IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Hangi müşteri login olduysa onun Taleplerini gösteren method
        public IActionResult RequestListByCustomerId()
        {
            string emailForClaim = User.FindFirstValue("Email");   
            var response = _requestService.GetRequestByEmail(emailForClaim);

            return View(response);
        }

        //Talep Ekleme Form Ekranı
        public IActionResult RequestCreate()
        {
            ViewBag.RequestTypes = _requestService.GetBySelectListRequestTypes();
            return View("RequestCreateForm", new RequestCreateDto());
        }
        [HttpPost]
        public IActionResult RequestCreate(RequestCreateDto model)
        {
            //Claimdeki Id : 
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            _requestService.AddRequestCreateDto(model,customerId);
            return RedirectToAction(nameof(RequestListByCustomerId));
        }

        //Bütün Taleplerin Listesi
        public IActionResult RequestList()
        {
            var response = _requestService.GetAllRequestList();
            return View(response);
        }

        //Müşteri Temsilcisinin Talebi Aldığı Method
        //Talep Detay sayfasına geçer 
        public IActionResult RequestCheckByCustomerRep(int requestId)
        {
            string emailForClaim = User.FindFirstValue("Email");
            ViewBag.RequestId = requestId;

            var response = _requestService.RequestDetailByCustomerRep(requestId, emailForClaim);
            return View("RequestDetailForm", response);
           
        }
       
        //Kapanan Talepleri Listeleyen method
         public IActionResult RequestCompletedList()
        {
            var response = _requestService.GetRequestCompletedList();
            return View(response);
        }

        //Yeni açılan Talepleri Listeleyen method
        public IActionResult RequestNewList()
        {
            var response = _requestService.GetRequestNewList();
            return View(response);
        }

        //Değerlendirmede olan Talepleri Listeleyen method
        public IActionResult RequestContinueList()
        {
            var response = _requestService.GetRequestContinueList();
            return View(response);
        }

       
        //Müşteri Temsilcisinin çözdüğü Taleplerin Listesi
        public IActionResult CustomerRepRequestCompletedList()
        {
            string emailForClaim = User.FindFirstValue("Email");
            var response = _requestService.GetCustomerRepRequestCompletedList(emailForClaim);
            return View(response);
        }

    }
}
