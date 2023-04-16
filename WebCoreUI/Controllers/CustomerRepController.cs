using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreUI.Controllers
{
    public class CustomerRepController : Controller
    {
       [Authorize(Roles ="customerRep")]  //login olmuş ve rolü=customerRep olanlar girebilir
        public IActionResult ProfileCustRep()
        {
            return View();
        }
    }
}
