using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.DAL;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class CustomerController : Controller
    {
        private RestaurantContext db = new RestaurantContext();


        
        // GET: Customer
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Table);
            var customerViewModel = customers.ToList().Select(c => new CustomerViewModel()
            {
                CustomerName = c.CustomerName,
                DinersCount = c.DinersCount,
                CustomerID = c.CustomerID,
                TimeIn = c.TimeIn,
                TableID = c.TableID,
                Table = c.Table

            });
            
        //    viewModel.Enrollments = viewModel.Courses.Where(
        ////    x => x.CourseID == courseID).Single().Enrollments;
            return View(customerViewModel);
        }
       
        //*************************************************************//

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var customerViewModel = new CustomerViewModel()
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                DinersCount = customer.DinersCount,
                TableID = customer.TableID,
                TimeIn = customer.TimeIn
            };
            return View(customerViewModel);
        }

        //********************************************************//


        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.TableID = new SelectList(db.Tables, "ID", "WaitTime");
            return View();
        }


        //********************************************************//


        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( CustomerViewModel customerViewModel)
        {
            //var test = customerViewModel;
            if (ModelState.IsValid)
            {
                
                Customer customer = new Customer();
                customerViewModel.TimeIn = DateTime.Now.ToString("t");
                customer.TimeIn = customerViewModel.TimeIn;
                customer.DinersCount = customerViewModel.DinersCount;
                customer.CustomerName = customerViewModel.CustomerName;
                customer.CustomerID = customerViewModel.CustomerID;

                var tableId = 0;
                if (db.Set<Table>().Any(t => t.Available == true && t.ChairCount >= customerViewModel.DinersCount))
                {
                    Table table = db.Set<Table>().FirstOrDefault(t => t.Available == true && t.ChairCount >= customerViewModel.DinersCount);

                    tableId = table.TableID;
                    var time = customerViewModel.DinersCount * 10;
                    table.AvailableAtTime = DateTime.Now.AddMinutes(30 + time);
                    table.Available = false;

                }
                else if (db.Set<Table>().Any(t => t.Available == false && t.ChairCount >= customerViewModel.DinersCount))
                {
                    
                    var table = db.Set<Table>().OrderBy(t => t.AvailableAtTime).FirstOrDefault(t => t.ChairCount >= customerViewModel.DinersCount);
                    tableId = table.TableID;

                }
                else
                {
                    ModelState.AddModelError("Error", "We're sorry, there are not enough seats available, please try again later.");
                    return View();
                }
                              
                customer.TableID = tableId;

                db.Customers.Add(customer);
                //customerViewModel.TimeIn = DateTime.Now.ToString("t");
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.TableID = new SelectList(db.Tables, "ID", "WaitTime", customerViewModel.TableID);
            return View(customerViewModel);
        }


        //********************************************************//
        

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            var customerViewModel = new CustomerViewModel()
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                DinersCount = customer.DinersCount,
                TableID = customer.TableID,
                TimeIn = customer.TimeIn
            };

            if (customerViewModel == null)
            {
                return HttpNotFound();
            }
            
            //ViewBag.TableID = new SelectList(db.Tables, "ID", "WaitTime", customer.TableID);
            return View(customerViewModel);
        }


        //********************************************************//


        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Set<Customer>().FirstOrDefault(c => c.CustomerID == customerViewModel.CustomerID);

                customer.CustomerID = customerViewModel.CustomerID;
                customer.CustomerName = customerViewModel.CustomerName;
                customer.DinersCount = customerViewModel.DinersCount;
                customer.TimeIn = customerViewModel.TimeIn;
                customer.TableID = customerViewModel.TableID;

                //db.Entry(customerViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TableID = new SelectList(db.Tables, "ID", "WaitTime", customerViewModel.TableID);
            return View(customerViewModel);
        }


        //********************************************************//


        // GET: Customer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            var customerViewModel = new CustomerViewModel()
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                DinersCount = customer.DinersCount,
                TableID = customer.TableID,
                TimeIn = customer.TimeIn
            };
            if (customerViewModel == null)
            {
                return HttpNotFound();
            }
           
            return View(customerViewModel);
        }


        //********************************************************//


        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CustomerViewModel customerViewModel)
        {
            var customer = db.Set<Customer>().FirstOrDefault(c => c.CustomerID == customerViewModel.CustomerID);

            customer.CustomerID = customerViewModel.CustomerID;
            customer.CustomerName = customerViewModel.CustomerName;
            customer.DinersCount = customerViewModel.DinersCount;
            customer.TimeIn = customerViewModel.TimeIn;
            customer.TableID = customerViewModel.TableID;

            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }


        //********************************************************//


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
