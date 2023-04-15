using System;
using System.Collections.Generic;
using System.Text;


namespace Business.Utilities.Security.JWT
{
    //Configuration ile appsettings.json da okuduğu değerleri atayacağımız değişkenleri tanımladığım class
    public class TokenOptions
    {
        public string Audience { get; set; }  //kullanıcı kitlesi
        public string Issuer { get; set; }    //imzalayan(uygulayan)
        public int AccessTokenExpiration { get; set; }   //Token ın geçerlilik süresi
        public string SecurityKey { get; set; }    //Token ın kullanacağı anahtar
    }
}
