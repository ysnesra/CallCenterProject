using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        //void dönen Run metotu --> iş kurallarını çalıştırsın
        //Params ile istediğmiz sayıda parametre verebiliriz. Bu parametreleri IResult tipindeki logics arayine(dizisine) atıyor 
        //Yani Run metotouna istediğimiz kadar iş kuralı gönderebiliriz
        public static string Run(params string[] logics)
        {
            foreach (var logic in logics)   //herbir iş kuralını gez
            {
                if (logic!=null)  //logic(iş kuralı) başarısız ise
                {
                    return logic;    //Business a şu iş kuralı hatalı diye haber veriyoruz
                }
            }
            return null;
        }
    }
}