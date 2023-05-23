using Core.DataAccess.Entityframework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;


namespace DataAccess.Concrete.Entityframework
{
    public class EfCustomerRepDal : EfEntityRepositoryBase<CustomerRep, CallCenterDbContext>, ICustomerRepDal
    {
        private readonly CallCenterDbContext _context;

        public EfCustomerRepDal(CallCenterDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ReportByDateDto> GetAllWithRequest(DateTime startDate, DateTime endDate)
        {
            #region SonBirAylıkRapor
            //if (true)
            //{
            //    var date = DateTime.Now;
            //    var sonbiray = date.AddMonths(-1);

            //    startDate = sonbiray;
            //    endDate = date;
            //} 
            #endregion

            //MüşteriTemsilcisi kaç talebe cevap vermiş
            var completedRequestByCustomerRep = _context.Requests
                                                        .Include(x => x.Calls)
                                                        .Where(x => x.StatusId == 3 &&
                                                                x.Calls.First().CallDate >= startDate && x.Calls.First().CallDate <= endDate).ToList()
                                                        .GroupBy(x => x.CustomerRepId)
                                                                .ToDictionary(g => g.Key, g => g.Count());

            //MüşteriTemsişcisinin talepleri cevaplama süresi toplamı
            var callTimeByCustomerRep = _context.Calls
                                                .Include(x => x.Request)
                                                .Where(x => x.Request.StatusId == 3 &&
                                                        x.CallDate >= startDate && x.CallDate <= endDate).ToList()
                                                .GroupBy(x => x.CustomerRepId)
                                                            .ToDictionary(g => g.Key, g => g.Sum(x => x.CallTime));

            //Toplam görüşme süresi ve Toplam Kapanan Talep Sayısı
            var allCallTimeTotal = _context.Calls
                                           .Where(x => x.CallDate >= startDate && x.CallDate <= endDate)
                                           .Sum(x => x.CallTime);

            float allCompletedRequestCount = _context.Requests
                                                     .Include(x => x.Calls)
                                                     .Where(x => x.StatusId == 3 &&
                                                            x.Calls.First().CallDate >= startDate && x.Calls.First().CallDate <= endDate).ToList().Count;

            var result = _context.Calls
                                .Include(x => x.CustomerRep)
                                .Include(x => x.Request)
                                .Where(x => x.CallDate >= startDate && x.CallDate <= endDate)
                                .GroupBy(x => x.CustomerRepId)
                                .Select(r => new ReportByDateDto
                                {
                                    FirstName = r.First().CustomerRep.FirstName,
                                    LastName = r.First().CustomerRep.LastName,
                                    Email = r.First().CustomerRep.Email,

                                    CompletedRequestCount = completedRequestByCustomerRep.GetValueOrDefault(r.First().CustomerRepId, 0),
                                    CallTimeTotal = callTimeByCustomerRep.GetValueOrDefault(r.First().CustomerRepId, 0),

                                    AllCallTimeTotal = allCallTimeTotal,
                                    AllCompletedRequestCount = allCompletedRequestCount,

                                    AverageCallTimeTotal = (callTimeByCustomerRep
                                                .GetValueOrDefault(r.First().CustomerRepId, 0) / allCallTimeTotal * 100).ToString("0.00"),
                                    AverageCompletedRequestCount = (completedRequestByCustomerRep
                                                .GetValueOrDefault(r.First().CustomerRepId, 0) / allCompletedRequestCount * 100).ToString("0.00"),

                                    StartDate = startDate,
                                    EndDate = endDate

                                }).ToList();
            return result;
        }

        //.GetValueOrDefault(r.First().CustomerRepId, 0) -> Dictionary tipinde liste olarak bulunan bir değişkende ;(Key değeri) CustomerRepId varsa değerini verir yoksa 0 dönderir


        #region farklı_Sql

        //public List<ReportByDateDto> GetAllWithRequest(DateTime startDate, DateTime endDate)
        //{
        //    //Öncelikle Tüm kapanan talepler listenir ve onun üzerinden CustomerRepId ye göre gruplanır 
        //    //ToDictionary() ile her bir müşteri temsilcisinin kapattığı talep sayısını hesaplanır. [1,13] [2,2]
        //    List<Request> completedRequestDb = _context.Requests.Where(x => x.StatusId == 3).ToList();
        //    var completedRequestByCustomerRep = completedRequestDb.GroupBy(x => x.CustomerRepId)
        //                                                         .ToDictionary(g => g.Key, g => g.Count());

        //    var callTimeByCustomerRep = _context.Calls.ToList().GroupBy(x => x.CustomerRepId)
        //                                              .ToDictionary(g => g.Key, g => g.Sum(x => x.CallTime));

        //    //Toplam görüşme süresi ve Toplam Kapanan Talep Sayısı
        //    var allCallTimeTotal = _context.Calls.Sum(x => x.CallTime);
        //    float allCompletedRequestCount = completedRequestDb.Count;

        //    var result = _context.Requests
        //                                .Include(x => x.CustomerRep)
        //                                .Include(x => x.Calls)
        //                                .Where(x => x.Calls.Any(c => c.CallDate >= startDate && c.CallDate <= endDate))  
        //                                .GroupBy(x => x.CustomerRepId)  
        //                                .Select(r => new ReportByDateDto
        //                                {
        //                                    FirstName = r.First().CustomerRep.FirstName,
        //                                    LastName = r.First().CustomerRep.LastName,
        //                                    Email = r.First().CustomerRep.Email,
        //                                    CompletedRequestCount = completedRequestByCustomerRep.ContainsKey(r.Key)
        //                                                          ? completedRequestByCustomerRep[r.Key]    // Keyi [1] olanın değerin Countını getir 
        //                                                          : 0,
        //                                    CallTimeTotal = callTimeByCustomerRep.ContainsKey(r.First().CustomerRep.CustomerRepId)
        //                                                ? callTimeByCustomerRep[r.First().CustomerRep.CustomerRepId]    // Keyi [1] olanın değerin Toplam süresini getir 
        //                                                : 0,

        //                                    AllCallTimeTotal = allCallTimeTotal,
        //                                    AllCompletedRequestCount = allCompletedRequestCount,

        //                                    AverageCallTimeTotal = callTimeByCustomerRep.ContainsKey(r.First().CustomerRep.CustomerRepId)
        //                                                        ? callTimeByCustomerRep[r.First().CustomerRep.CustomerRepId] / allCallTimeTotal * 100
        //                                                        : 0,

        //                                    AverageCompletedRequestCount = completedRequestByCustomerRep.ContainsKey(r.First().CustomerRep.CustomerRepId)
        //                                                                ? completedRequestByCustomerRep[r.First().CustomerRep.CustomerRepId] / allCompletedRequestCount * 100
        //                                                                : 0,

        //                                    StartDate = startDate,
        //                                    EndDate = endDate

        //                                }).ToList();
        //    return result;
        //} 
        #endregion

    }
}
