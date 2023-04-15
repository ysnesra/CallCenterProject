using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //Credentials-> anahtarımız
        //anahtarımızı(securityKey) vericez imzalama nesnesi olrak bize dönderecek 

        //ASP.Netin(webApi nin) Hangi anahtarı hangi şifreleme algoritmasını kullancak onu söylediğimiz metot
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            //anahtar olarak secirtyKey kullan şifreleme olarakta "SecurityAlgorithms.HmacSha512Signature"ı kullan.

        }
    }
}
