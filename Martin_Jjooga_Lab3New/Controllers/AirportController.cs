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
    public class AirportController : Controller
    {



        //connection settings for CosmoDB
        string EndpointUrl;
        private string PrimaryKey;
        private DocumentClient client;

        public AirportController()
        {
            EndpointUrl = "https://localhost:8081";
            PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";


            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

        }







        private dataUsed db = new dataUsed();

        // GET: Tmp_Airport
        public async Task<ActionResult> Index()
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database() { Id = "CharterResor" });
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CharterResor"),
                new DocumentCollection { Id = "Airport" });

            FeedOptions queryOptions = new FeedOptions { EnableCrossPartitionQuery = true };
            IQueryable<Airport> airports = this.client.CreateDocumentQuery<Airport>(
                    UriFactory.CreateDocumentCollectionUri("CharterResor", "Airport"), queryOptions)
                ;


            return View(airports); // --CsomoDB contentList


            return View(db.Tmp_Airport.ToList()); //SQL DBlist
        }

        // GET: Tmp_Airport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airport tmp_Airport = db.Tmp_Airport.Find(id);
            if (tmp_Airport == null)
            {
                return HttpNotFound();
            }
            return View(tmp_Airport);
        }

        // GET: Tmp_Airport/Create
        public ActionResult Create()
        {
            return View();
            
        }

        // POST: Tmp_Airport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Airport_Code,city,latitude,logitude")] Airport tmp_Airport)
        { 
            if (ModelState.IsValid)
            {
                db.Tmp_Airport.Add(tmp_Airport);
                db.SaveChanges();
                return RedirectToAction("Index");

              //  this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("CharterResor", "Airport"), tmp_Airport); //To create document in CosmoDB
            }

            return View(tmp_Airport);
        }

        // GET: Tmp_Airport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airport tmp_Airport = db.Tmp_Airport.Find(id);
            if (tmp_Airport == null)
            {
                return HttpNotFound();
            }
            return View(tmp_Airport);
        }

        // POST: Tmp_Airport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Airport_Code,city,latitude,logitude")] Airport tmp_Airport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tmp_Airport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tmp_Airport);
        }

        // GET: Tmp_Airport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airport tmp_Airport = db.Tmp_Airport.Find(id);
            if (tmp_Airport == null)
            {
                return HttpNotFound();
            }
            return View(tmp_Airport);
        }

        // POST: Tmp_Airport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Airport tmp_Airport = db.Tmp_Airport.Find(id);
            db.Tmp_Airport.Remove(tmp_Airport);
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
