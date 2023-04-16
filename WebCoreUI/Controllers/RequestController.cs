using Business.Abstract;
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
            string emailForClaim = User.FindFirstValue("Email");   //Email bilgisini direk cookieden alırız
            //var response = _requestService.GetRequestByEmail(emailForClaim);

            return View(response);

            //Claimdeki Id yi almak istesydik:   
            //int userId=int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }


       
    }
}
