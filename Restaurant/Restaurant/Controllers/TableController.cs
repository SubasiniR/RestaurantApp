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
    public class TableController : Controller
    {
        private RestaurantContext db = new RestaurantContext();

        // GET: Table
        public ActionResult Index()
        {
            var tableViewModel = db.Tables.ToList().Select(c => new TableViewModel()
            {
                TableID = c.TableID,
                Available = c.Available,
                AvailableAtTime = c.AvailableAtTime,
                ChairCount = c.ChairCount
            });
            //if (tableViewModel.Any(t => t.AvailableAtTime <= DateTime.Now))
            //{
            //    var table = db.Set<Table>().FirstOrDefault(t => t.AvailableAtTime <= DateTime.Now);
            //}
            return View(tableViewModel);
        }


        //********************************************************//


        // GET: Table/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Table table = db.Tables.Find(id);

            var tableViewModel = new TableViewModel()
            {
                TableID = table.TableID,
                ChairCount = table.ChairCount,
                Available = table.Available,
                AvailableAtTime = table.AvailableAtTime
            };

            if (tableViewModel == null)
            {
                return HttpNotFound();
            }
           
            return View(tableViewModel);
        }

  
        //********************************************************//


        // GET: Table/Create
        public ActionResult Create()
        {
            return View();
        }


        //********************************************************//


        // POST: Table/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TableViewModel tableViewModel)
        {
            if (ModelState.IsValid)
            {
                Table table = new Table()
                {
                    TableID = tableViewModel.TableID,
                    Available = tableViewModel.Available,
                    ChairCount = tableViewModel.ChairCount,
                    AvailableAtTime = tableViewModel.AvailableAtTime
                };
                db.Tables.Add(table);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

             return View(tableViewModel);
        }


        //********************************************************//


        // GET: Table/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);

            if (table == null)
            {
                return HttpNotFound();
            }

            var tableViewModel = new TableViewModel()
            {
                TableID = table.TableID,
                Available = table.Available,
                AvailableAtTime = table.AvailableAtTime,
                ChairCount = table.ChairCount
            };
            
            return View(tableViewModel);
        }


        //********************************************************//
        

        // POST: Table/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TableViewModel tableViewModel)
        {
            if (ModelState.IsValid)
            {
                var table = db.Set<Table>().FirstOrDefault(t => t.TableID == tableViewModel.TableID);

                table.TableID = tableViewModel.TableID;
                table.ChairCount = tableViewModel.ChairCount;
                table.AvailableAtTime = tableViewModel.AvailableAtTime;
                table.Available = tableViewModel.Available;
                
                
               //Update table in database();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableViewModel);
        }


        //********************************************************//


        // GET: Table/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return HttpNotFound();
            }
            var tableViewModel = new TableViewModel()
            {
                TableID = table.TableID,
                ChairCount = table.ChairCount,
                Available = table.Available,
                AvailableAtTime = table.AvailableAtTime
            };
            return View(tableViewModel);
        }


        //********************************************************//


        // POST: Table/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TableViewModel tableViewModel)
        {
            var table = db.Set<Table>().FirstOrDefault(c => c.TableID == tableViewModel.TableID);

            table.TableID = tableViewModel.TableID;
            table.ChairCount = tableViewModel.ChairCount;
            table.Available = tableViewModel.Available;
            table.AvailableAtTime = tableViewModel.AvailableAtTime;

            db.Tables.Remove(table);
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
