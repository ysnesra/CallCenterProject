using Business.Abstract;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdminManager : IAdminService
    {
        ICustomerDal _customerDal;
        IRequestDal _requestDal;
        ICustomerRepDal _customerRepDal;
        ICallDal _callDal;

        public AdminManager(ICustomerDal customerDal, IRequestDal requestDal, ICustomerRepDal customerRepDal, ICallDal callDal)
        {
            _customerDal = customerDal;
            _requestDal = requestDal;
            _customerRepDal = customerRepDal;
            _callDal = callDal;
        }

        //Admin Tarafta Bütün müşterileri listeleme 
        public List<CustomerAllListDto> GetAllCustomerList()
        {
            var customerListDb = _customerDal.GetAll(x => x.Role == "customer");

            List<CustomerAllListDto> response = customerListDb.Select(x => new CustomerAllListDto()
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                Phone = x.Phone,
                Role = x.Role
            }).ToList();

            return response;
        }

        //Admin tarafta Müşterinin Taleplerini listeleme
        public List<RequestListByCustomerDto> GetRequestsByCustomerId(int customerId)
        {
            var requestListdb = _requestDal.GetRequestsByCustomerId(customerId);
            if (requestListdb == null)
            {
                throw new Exception("Bu müşterinin Talebi bulunmamaktadır.");
            }

            return requestListdb;
        }
        //müşteri Id'sine göre ismini getirme 
        public string GetByIdCustomerName(int customerId)
        {
            var customer = _customerDal.Get(x => x.CustomerId == customerId);

            return $"{customer.FirstName} {customer.LastName}";
        }

        public async Task<string> GetByIdCustomerNameAsync(int customerId)
        {
            var customer = await _customerDal.GetAsync(x => x.CustomerId == customerId);

            return $"{customer.FirstName} {customer.LastName}";
        }


        //Admin Tarafta Bütün Müşteri Temsilcilerini listeleme 
        public List<CustomerRepAllListDto> GetAllCustomerRepList()
        {
            var customerListDb = _customerDal.GetAll(x => x.Role == "customerRep");

            List<CustomerRepAllListDto> response = customerListDb.Select(x => new CustomerRepAllListDto()
            {
                CustomerId = x.CustomerId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                Phone = x.Phone,
                Role = x.Role
            }).ToList();

            return response;
        }

        //Müşteri Temsilcisi Kayıt Ekranı
        public void AddCustomerRepDto(CustomerRepRegisterDto model)
        {
            //Bu mail adresi kullanılıyor mu
            var dbCustomer = _customerDal.Get(x => x.Email == model.Email);
            if (dbCustomer != null)
            {
                throw new Exception("Aynı mail adresinde kullanıcı bulunmaktadır");
            }
            else
            {
                Customer newCustomer = new Customer()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = model.Password,
                    Role = model.Role
                };

                _customerDal.Add(newCustomer);
                _customerDal.SaveChanges();

                CustomerRep newCustomerRep = new CustomerRep()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Password = model.Password,

                };
                _customerRepDal.Add(newCustomerRep);
                _customerRepDal.SaveChanges();
            }
        }

        //Müşteri Temsilcisi Güncelleme formunu CustomerId ye göre dolu getirme
        public CustomerRepEditDto GetCustomerRepByCustomerId(int customerId)
        {
            //Böyle bir müşteri temsilcisi sistemde var mı 
            var existCustomer = _customerDal.Get(x => x.CustomerId == customerId && x.Role == "customerRep");
            var dbCustomerRepId = _customerRepDal.Get(x => x.Email == existCustomer.Email).CustomerRepId;
            if (existCustomer is null)
            {
                throw new Exception("Müşteri Temsilcisi bulunmamaktadır");
            }
            CustomerRepEditDto response = new CustomerRepEditDto()
            {
                CustomerId = existCustomer.CustomerId,
                FirstName = existCustomer.FirstName,
                LastName = existCustomer.LastName,
                Email = existCustomer.Email,
                Phone = existCustomer.Phone,
                CustomerRepId = dbCustomerRepId
            };
            return response;
        }
        public void EditCustomerRepDto(CustomerRepEditDto model)
        {
            //Böyle bir müşteri temsilcisi sistemde var mı 
            Customer existCustomer = _customerDal.Get(x => x.CustomerId == model.CustomerId && x.Role == "customerRep");
            CustomerRep existCustomerRep = _customerRepDal.Get(x => x.CustomerRepId == model.CustomerRepId);
            if (existCustomer == null && existCustomerRep == null)
            {
                throw new Exception("Müşteri Temsilcisi bulunmamaktadır");
            }

            existCustomer.CustomerId = model.CustomerId;
            existCustomer.FirstName = model.FirstName;
            existCustomer.LastName = model.LastName;
            existCustomer.Email = model.Email;
            existCustomer.Password = model.Password;
            existCustomer.Phone = model.Phone;
            existCustomer.Role = "customerRep";

            _customerDal.Update(existCustomer);
            _customerDal.SaveChanges();


            existCustomerRep.CustomerRepId = model.CustomerRepId;
            existCustomerRep.FirstName = model.FirstName;
            existCustomerRep.LastName = model.LastName;
            existCustomerRep.Email = model.Email;
            existCustomerRep.Password = model.Password;
            existCustomerRep.Phone = model.Phone;

            _customerRepDal.Update(existCustomerRep);
            _customerRepDal.SaveChanges();

        }

        public void DeleteCustomerRepDto(int customerId)
        {
            //Böyle bir müşteri temsilcisi sistemde var mı 
            var existCustomer = _customerDal.Get(x => x.CustomerId == customerId && x.Role == "customerRep");
            var dbCustomerRep = _customerRepDal.Get(x => x.Email == existCustomer.Email);
            if (existCustomer is null)
            {
                throw new Exception("Müşteri Temsilcisi bulunmamaktadır");
            }

            _customerDal.Delete(existCustomer);
            _customerDal.SaveChanges();
            _customerRepDal.Delete(dbCustomerRep);
            _customerRepDal.SaveChanges();
        }

        //Müşteri Temsilcisi Raporları
        public List<ReportDto> GetReportList()
        {
            //Öncelikle Tüm kapanan talepler listelenip onun üzerinden CustomerRepId ye göre gruplanır 
            //ToDictionary() ile her bir müşteri temsilcisinin kapattığı talep sayısını hesaplanır. [1,13] [2,2]
            List<Request> completedRequestDb = _requestDal.GetAll(x => x.StatusId == 3);
            var completedRequestByCustomerRep = completedRequestDb.GroupBy(x => x.CustomerRepId)
                                                                 .ToDictionary(g => g.Key, g => g.Count());

            var callTimeByCustomerRep = _callDal.GetAll().GroupBy(x => x.CustomerRepId)
                                                      .ToDictionary(g => g.Key, g => g.Sum(x => x.CallTime));

            //Toplam görüşme süresi ve Toplam Kapanan Talep Sayısı
            var allCallTimeTotal = _callDal.GetAll().Sum(x => x.CallTime);  
            float allCompletedRequestCount = completedRequestDb.Count;

            List<ReportDto> response = _customerRepDal.GetAll().Select(x => new ReportDto()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                CompletedRequestCount = completedRequestByCustomerRep.ContainsKey(x.CustomerRepId)
                                            ? completedRequestByCustomerRep[x.CustomerRepId]    // Keyi [1] olanın değerin Countını getir 
                                            : 0,

                CallTimeTotal = callTimeByCustomerRep.ContainsKey(x.CustomerRepId)
                                            ? callTimeByCustomerRep[x.CustomerRepId]    // Keyi [1] olanın değerin Toplam süresini getir 
                                            : 0,

                AllCallTimeTotal = allCallTimeTotal,
                AllCompletedRequestCount = allCompletedRequestCount,

                AverageCallTimeTotal= callTimeByCustomerRep.ContainsKey(x.CustomerRepId)
                                            ? callTimeByCustomerRep[x.CustomerRepId] / allCallTimeTotal * 100  
                                            : 0,

                AverageCompletedRequestCount = completedRequestByCustomerRep.ContainsKey(x.CustomerRepId)
                                            ? completedRequestByCustomerRep[x.CustomerRepId] / allCompletedRequestCount * 100   
                                            : 0 ,    
            }).ToList();
           
            return response;
        }

        #region BusinessRules

        /// İş kuralları parçacığı 

        //Böyle bir müşteri temsilcisi sistemde kayıtlı mı
        //Null Check
        //private IResult CheckIfCustomerIdExists(int customerId)
        //{
        //    var result = _customerDal.Get(x => x.CustomerId == customerId && x.Role == "customerRep");

        //    if (result is null)
        //    {
        //        //throw new Exception("Müşteri Temsilcisi bulunmamaktadır");
        //        return ErrorResult("Müşteri Temsilcisi bulunmamaktadır");
        //    }
        //    else
        //        return result;
        //}
        #endregion}

    }
}