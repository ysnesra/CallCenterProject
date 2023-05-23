using Business.Abstract;
using DataAccess.Abstract;
using Entities.DTOs;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Entities.Concrete;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace WebCoreUI.Controllers
{
    // [Authorize(Roles ="customer")]
    public class CustomerController : Controller
    {
        ICustomerService _customerService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CustomerController(ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            this._httpContextAccessor = httpContextAccessor;
        }
        //Müşteri Ekranı 
        public IActionResult ProfileCust()
        {
            return View();
        }
        
        //ÜyeOl Ekranı formu
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        #region JwtToken_LoginOlma
        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult Login(CustomerLoginDto model)
        //{
        //    //Mail ve şifre databasede var mı
        //    var customerdb = _customerService.GetByLoginFilter(model);

        //    if (customerdb == null)
        //    {
        //        return RedirectToAction(nameof(Login));
        //    }

        //    _httpContextAccessor.HttpContext.Response.Cookies.Append("token", customerdb.Token , new CookieOptions()
        //    {
        //        HttpOnly = true,
        //        Expires = DateTime.Now.AddDays(1),
        //        Secure = true,
        //        IsEssential = true,
        //        SameSite = SameSiteMode.None,
        //    }); // Oluştuyrulan tokeni cookie ye basmak için kullanılır 


        //    //eğer çağrı merkezi personeli ise // x viewine eğer müşteri ise y viewine redirect ediceksin 
        //    if (customerdb.Role == "customerRep")
        //    {
        //        return RedirectToAction("Index", "CustomerRep");

        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Customer");
        //    }

        //} 
        #endregion

        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(CustomerLoginDto model)
        {
            ModelState.Remove("CustomerId");
            ModelState.Remove("Role");

            if (ModelState.IsValid)
            {
                //Mail ve şifre eşleşiyor mu
                var existCustomer = _customerService.GetByLoginFilter(model);
                if (existCustomer == null)
                {
                    return RedirectToAction(nameof(Login));
                }

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, existCustomer.CustomerId.ToString()));
                claims.Add(new Claim("Email", existCustomer.Email));
                claims.Add(new Claim("Password", existCustomer.Password));
                claims.Add(new Claim(ClaimTypes.Role, existCustomer.Role));

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);  //Cookieyi kapat
            return RedirectToAction(nameof(Login));
        }

        //Müşterinin Bilgi formunu CustomerId ye göre dolu getirme
        [HttpGet]
        public IActionResult CustomerInformation()
        {
           //Claimdeki Id : 
           int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
           CustomerInformationDto model = _customerService.GetCustomerInformation(customerId);
           return View(model);
        }

        //Müşteri Bilgileri Güncelleme
        [HttpPost]
        public IActionResult CustomerInformation(CustomerInformationDto model)
        {
            _customerService.GetCustomerInformationDto(model);
            return RedirectToAction(nameof(CustomerInformation));
        }
      
        //Müşterinin Bilgi formunu CustomerId ye göre dolu getirme
        [HttpGet]
        public IActionResult CustomerInformationProfile()
        {
            ProfileInfoLoader();
            return View();
        }

        private void ProfileInfoLoader()      //Açılışta değerleri dolu getiren metodumuz
        {
            //Claimdeki Id : 
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            CustomerInformationDto model = _customerService.GetCustomerInformation(customerId);
            ViewData["FirstName"] = model.FirstName;
            ViewData["LastName"] = model.LastName;
            ViewData["Phone"] = model.Phone;
            ViewData["Email"] = model.Email;
            ViewData["Password"] = model.Password;         
        }


        //Müşteri AdıSoyadı Güncelleme
        //Veriler model olmadan nasıl gelir? --Inputların (name="firstName") name içindeki ismini parametre olarak verebiliriz
        [HttpPost]
        public IActionResult ProfileChangeFullName([Required][MinLength(2)][StringLength(50)]string? firstName, [Required][StringLength(50)] string? lastName)
        {
            //Claimdeki Id : 
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));     
            _customerService.GetProfileChangeFullName(firstName,lastName,customerId);

            ViewData["fullNameResult"] = "AdSoyad bilginiz değişti";
            ProfileInfoLoader();
            return View(nameof(CustomerInformationProfile));
        }

        //Müşteri Telefon Güncelleme
        [HttpPost]
        public IActionResult ProfileChangePhone([Required] string? phone)
        {
            //Claimdeki Id : 
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _customerService.GetProfileChangePhone(phone, customerId);

            ViewData["phoneResult"] = "Telefonunuz değişti";
            ProfileInfoLoader();
            return View(nameof(CustomerInformationProfile));
        }
        
        //Müşteri Telefon Güncelleme
        [HttpPost]
        public IActionResult ProfileChangeEmail([Required][EmailAddress] string? email)
        {
            //Claimdeki Id : 
            int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _customerService.GetProfileChangeEmail(email, customerId);

            ViewData["emailResult"] = "Mailiniz değişti";
            ProfileInfoLoader();
            return View(nameof(CustomerInformationProfile));
        }

        //Müşteri Parola Güncelleme
        [HttpPost]
        public IActionResult ProfileChangePassword([Required]
             [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"
             ,ErrorMessage ="Parolanız en az sekiz karakter, en az bir harf ve bir sayı içermelidir!")] string? password)
        {
            if (ModelState.IsValid)
            {
                //Claimdeki Id : 
                int customerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _customerService.GetProfileChangePassword(password, customerId);

                ViewData["passwordResult"] = "Parolanız değişti";                    
            }
            ProfileInfoLoader();
            return View(nameof(CustomerInformationProfile));
        }
    }
}
