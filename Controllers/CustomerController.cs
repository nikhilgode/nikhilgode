using CityCustomerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using CityCustomerMVC.ViewModels;

namespace CityCustomerMVC.Controllers
{
    public class CustomerController : Controller
    {
        
        public ActionResult Index()
        {

           var context=new ApplicationDbContext();
            var allcust = context.customers.Include(c=>c.city).ToList();
            return View(allcust);
        }


        public ActionResult NewCustomerForm()
        {
            var context= new ApplicationDbContext();
            var cities1= context.cities.ToList();
            var viemodel = new CustomerCity()
            {
                cities=cities1,
            };
            
            return View(viemodel);
        }


        [HttpPost]
        public ActionResult Create(CustomerCity c1)
        {
            var context = new ApplicationDbContext();
            //context.customers.Add(c1.customer);


            //try
            //{
            //    context.SaveChanges();
            //}
            //catch (Exception ex) {
            //    Console.WriteLine(ex);
            //}

            //return RedirectToAction("Index","Customer");

            //----------------------------------------------------------------------//

            // Fetch the selected city based on the provided Id
            var selectedCity = context.cities.FirstOrDefault(c => c.Id == c1.customer.city.Id);

            // Assign the selected city to the customer's city property
            c1.customer.city = selectedCity;

            context.customers.Add(c1.customer);

            try
            {
                context.SaveChanges();
           }
            catch (Exception ex) {
                Console.WriteLine(ex);
           }

            return RedirectToAction("Index", "Customer");
        }
    }
}