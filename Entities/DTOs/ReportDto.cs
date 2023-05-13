using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ReportDto: IDto
    {
        //public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? CompletedRequestCount { get; set; }  //Kapanan Talep Sayısı
        public float? AllCompletedRequestCount { get; set; }  //Toplam Kapanan Talep Sayısı
        public float? AverageCompletedRequestCount { get; set; }  //Ortalama Kapanan Talep Sayısı
        public float? CallTimeTotal { get; set; }  //Her müşteri temsilcisinin Toplam Görüşme Süresi
        public float? AllCallTimeTotal { get; set; }  //Toplam Görüşme Süresi
        public float? AverageCallTimeTotal { get; set; }  //Ortalama Görüşme Süresi

    }
}
