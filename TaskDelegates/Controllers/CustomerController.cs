using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using TaskDelegates.Data;
using TaskDelegates.Models;

namespace TaskDelegates.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        //Custome Delegate Declaration
        private delegate List<customer> CNameLahore(IList<customer> _customers);
        private delegate List<customer> CName(IList<customer> _customers);
        private delegate List<customer> CustomerAccordingToAge(IList<customer> _customers);
        private delegate String TotalOfGrandTotal(IList<customer> _customers);
        //Custome Delegate Declaration End

        public CustomerController(ApplicationDbContext dbcontext)
        {
            _dbcontext= dbcontext;
        }
        public IActionResult Index()
        {
            TotalOFGrandTotal();
            var customers = _dbcontext.Customers.ToList();
            return View(customers);
        }

        //SingleCast Delegate / Custom Delegate
        [HttpGet]
        public IActionResult DisplayNameLahore()
        {
            CNameLahore cNamehandler = delegate (IList<customer> _customer)
            {
               var customer = _customer.Where(c => c.City.Equals("Lahore")||c.City.Equals("lahore")).ToList();
               return customer;
            };
           var customers = cNamehandler(_dbcontext.Customers.ToList());
            return View(customers);  
        }

       


        [HttpGet]
    public IActionResult DisplayName()
    {
        //var customers = _dbcontext.Customers.ToList();
        CNameLahore cNamehandler = delegate (IList<customer> _customers)
        {
            var customer = _customers.Where(c => c.City.Equals("Lahore") || c.City.Equals("lahore")).ToList();
            //var cNames = customer;
            return customer;
        };

        var customers = cNamehandler(_dbcontext.Customers.ToList());
            //Print print = delegate (int val) {
            //    Console.WriteLine("Inside Anonymous method. Value: {0}", val);
            //};
            List<String> CNames = new List<string>();
        foreach(var c in customers)
            {
               CNames = CNames.Append(c.Name).ToList();
            }
        return View(CNames);
    }

        [HttpGet]
        public IActionResult DisplayCustomersAge55To60()
        {
            CNameLahore cNamehandler = delegate (IList<customer> _customer)
            {
                var customer = _customer.Where(c => Convert.ToInt32(c.Age) >= 55 && Convert.ToInt32(c.Age) <= 60).ToList();
                return customer;
            };
            var customers = cNamehandler(_dbcontext.Customers.ToList());
            return View(customers);
        }

        //public void TotalOFGrandTotal()
        //{
        //    TotalOfGrandTotal chandler = delegate (IList<customer> _customer)
        //    {
        //        int Total=0;
        //        foreach(var c in _customer)
        //        {
        //            Total += Convert.ToInt32(c.GrandTotal);
        //        }

                
        //        return Total.ToString();

        //    };

        //    var total = chandler(_dbcontext.Customers.ToList());
        //    ViewBag.Total=total;
        //    //return View();
        //}
        //SingleCast Delegate / Custom Delegate End




        //New Section
        //Generic Delegate: Func Section
        //[HttpGet]
        //public IActionResult GetRecordsLahoreFunc()
        //{
        //    Func<IList<customer>, IList<customer>> chandler = delegate (IList<customer> _customer)
        //    {
        //        return _customer.Where(c => c.City.Equals("Lahore") || c.City.Equals("lahore")).ToList();

        //    };
        //    return View(chandler(_dbcontext.Customers.ToList()));
        //}

        //Generic Delegate: Func Section With Lambda Expresion
        [HttpGet]
        public IActionResult GetRecordsLahoreFunc()
        {
            Func<IList<customer>, IList<customer>> chandler = (IList<customer> _customer)=>
                _customer.Where(c => c.City.Equals("Lahore") || c.City.Equals("lahore")).ToList();
            return View(chandler(_dbcontext.Customers.ToList()));
        }

        //Generic Delegate: Func Section End

        //Generic Delegate: Func Section With Anonymas Method


        //public void TotalOFGrandTotal()
        //{
        //    Func<IList<customer>,String> chandler = delegate (IList<customer> _customer)
        //    {
        //        int Total = 0;
        //        foreach (var c in _customer)
        //        {
        //            Total += Convert.ToInt32(c.GrandTotal);
        //        }

        //        return Total.ToString();

        //    };
        //    var total = chandler(_dbcontext.Customers.ToList());
        //    ViewBag.Total = total;
        //}

        //Generic Delegate: Func Section With Anonymos Method End

        ////Generic Delegate: Action Section With Anonymus Method
        public void TotalOFGrandTotal()
        {
            Action<IList<customer>> chandler = delegate (IList<customer> _customer)
            {
                int Total = 0;
                foreach (var c in _customer)
                {
                    Total += Convert.ToInt32(c.GrandTotal);
                }
                ViewBag.Total = Total.ToString();
            };
            chandler(_dbcontext.Customers.ToList());
        }

        //Generic Delegate: Action Section With Lambda Expresion

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var customer = _dbcontext.Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(customer);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _dbcontext.Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = _dbcontext.Customers.Where(x => x.Id == id).FirstOrDefault();
            return View(customer);
        }

        ///// <summary>
        ///// Post Secion of course
        ///// </summary>
        ///// <returns></returns>

        [HttpPost]
        public IActionResult Create(customer model)
        {
            _dbcontext.Customers.Add(model);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(customer model)
        {
            _dbcontext.Customers.Update(model);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(customer model)
        {
            _dbcontext.Customers.Remove(model);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
