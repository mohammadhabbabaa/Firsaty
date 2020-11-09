using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using firsaty.Models;

namespace firsaty.Controllers
{
    public class NewMailsController : Controller
    {
        private firsatyEntities db = new firsatyEntities();

        // GET: NewMails
        public ActionResult Index()
        {
            return View(db.NewMails.ToList());
        }

        // GET: NewMails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMail newMail = db.NewMails.Find(id);
            if (newMail == null)
            {
                return HttpNotFound();
            }
            return View(newMail);
        }

        // GET: NewMails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewMails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Mail")] NewMail newMail)
        {
            if (ModelState.IsValid)
            {
                db.NewMails.Add(newMail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newMail);
        }

        // GET: NewMails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMail newMail = db.NewMails.Find(id);
            if (newMail == null)
            {
                return HttpNotFound();
            }
            return View(newMail);
        }

        // POST: NewMails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Mail")] NewMail newMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newMail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newMail);
        }

        // GET: NewMails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewMail newMail = db.NewMails.Find(id);
            if (newMail == null)
            {
                return HttpNotFound();
            }
            return View(newMail);
        }

        // POST: NewMails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewMail newMail = db.NewMails.Find(id);
            db.NewMails.Remove(newMail);
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
