using AutoMapper;
using CityCustomerMVC.Dto;
using CityCustomerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;




namespace CityCustomerMVC.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController() 
        {
          _context = new ApplicationDbContext();
        }

        public IEnumerable <Customer> GetCustomers() 
        {
             return _context.customers.ToList();
        }

        public Customer GetCustomer(int id) 
        {
           var cus = _context.customers.SingleOrDefault(x => x.Id == id);

          if(cus == null) 
            {
              throw new  HttpResponseException(HttpStatusCode.NotFound);
            }

          return cus;
        }

        [HttpPost]
        public Customer CreateCustomer(Customer c1)
        {
            if(!ModelState.IsValid) 
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.customers.Add(c1);
            _context.SaveChanges();

            return c1;           
        }


        [HttpPut]
        public void UpdateCustomer(int id, Customer c2)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var ExistingCusomer= _context.customers.SingleOrDefault(c => c.Id == id);

            if(ExistingCusomer == null) 
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            ExistingCusomer.Name = c2.Name;
            ExistingCusomer.city = c2.city;

            _context.SaveChanges();
        }

        
        [HttpDelete]
 
        public void DeleteCustomer(int id) 
        {
            var cus = _context.customers.SingleOrDefault(x => x.Id == id);

            if (cus == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.customers.Remove(cus);
            _context.SaveChanges();
        }

        //-----------------------USING AUTOMAPPER--------------------------//


         public IEnumerable<CustomerDto> GetCustomers1()
        {
            return _context.customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }


        public CustomerDto GetCustomer1(int id)
        {
            var cus = _context.customers.SingleOrDefault(x => x.Id == id);

            if (cus == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<Customer,CustomerDto>(cus) ;
        }




    }
}
