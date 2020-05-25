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
using Database = System.Data.Entity.Database;

namespace Martin_Jjooga_Lab3New.Controllers
{
    public class AirlinesController : Controller
    {


        //connection settings for CosmoDB
        string EndpointUrl;
        private string PrimaryKey;
        private DocumentClient client;

        public AirlinesController()
        {
            EndpointUrl = "https://localhost:8081";
            PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";


            client = new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

        }


        private dataUsed db = new dataUsed();

        // GET: Airlines
        public async Task<ActionResult> Index()
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Microsoft.Azure.Documents.Database(){ Id = "CharterResor" });
            await client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("CharterResor"),
                new DocumentCollection { Id = "Airline" });

            FeedOptions queryOptions = new FeedOptions { EnableCrossPartitionQuery = true };
            IQueryable<Airline> airlines = this.client.CreateDocumentQuery<Airline>(
                    UriFactory.CreateDocumentCollectionUri("CharterResor", "Airline"), queryOptions)
                ;


            return View(airlines); //--> CosmoDb Content

            return View(db.Airlines.ToList()); //--> SQL  Content comment on the Line ABOVE TO RETURN THIS
        }

        // GET: Airlines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // GET: Airlines/Create
        public ActionResult Create()
        {   
            return View();
        }

        // POST: Airlines/Create
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "code,name")] Airline airline)
        {
            if (ModelState.IsValid)
            {
                db.Airlines.Add(airline);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          //uncomment to create documents a admin in cosmoDB await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("CharterResor", "Airline"), airline); //To create document in CosmoDB
            return View(airline);
        }

        // GET: Airlines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
            return View(airline);
        }

        // POST: Airlines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,name")] Airline airline)
        {
            if (ModelState.IsValid)
            {
                db.Entry(airline).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(airline);
        }

        // GET: Airlines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Airline airline = db.Airlines.Find(id);
            if (airline == null)
            {
                return HttpNotFound();
            }
           //Uncooment To deleteDoc from cosmoDB await this.client.DeleteDocumentAsync(UriFactory.CreateDocumentUri("CharterResor", "Airline", id.ToString()));

            return View(airline);
        }

        // POST: Airlines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Airline airline = db.Airlines.Find(id);
            db.Airlines.Remove(airline);
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
