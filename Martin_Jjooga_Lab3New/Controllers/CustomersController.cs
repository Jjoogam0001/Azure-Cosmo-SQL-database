using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Martin_Jjooga_Lab3New.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Martin_Jjooga_Lab3New.Controllers
{
    public class CustomersController : Controller
    {
        //connection settings for CosmoDB
        string EndpointUrl;
        private string PrimaryKey;
        private DocumentClient client;

        public CustomersController()
        {
            EndpointUrl = "https://localhost:8081";
            PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";


            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

        }






        private dataUsed db = new dataUsed();

        // GET: Customers
        public async Task<ActionResult> Index()
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database() { Id = "CharterResor" });
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CharterResor"),
                new DocumentCollection { Id = "Customer" });

            FeedOptions queryOptions = new FeedOptions { EnableCrossPartitionQuery = true };
            IQueryable<Customer> customers = this.client.CreateDocumentQuery<Customer>(
                    UriFactory.CreateDocumentCollectionUri("CharterResor", "Customer"), queryOptions)
                ;


            return View(customers);





            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
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
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerId,Card_Number,Holder_Name,Expiry_Date,Balance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
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
            return View(customer);
        }

        // POST: Customers/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,Card_Number,Holder_Name,Expiry_Date,Balance")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
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
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
