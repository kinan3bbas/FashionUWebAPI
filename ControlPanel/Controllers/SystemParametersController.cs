﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlPanel.Models;

namespace ControlPanel.Controllers
{
    [Authorize]
    public class SystemParametersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SystemParameters
        public ActionResult Index()
        {
            return View(db.SystemParameters.OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: SystemParameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemParameter systemParameter = db.SystemParameters.Find(id);
            if (systemParameter == null)
            {
                return HttpNotFound();
            }
            return View(systemParameter);
        }

        // GET: SystemParameters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Code,Value,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] SystemParameter systemParameter)
        {
            if (ModelState.IsValid)
            {
                db.SystemParameters.Add(systemParameter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemParameter);
        }

        // GET: SystemParameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemParameter systemParameter = db.SystemParameters.Find(id);
            if (systemParameter == null)
            {
                return HttpNotFound();
            }
            return View(systemParameter);
        }

        // POST: SystemParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Code,Value,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] SystemParameter systemParameter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemParameter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemParameter);
        }

        // GET: SystemParameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemParameter systemParameter = db.SystemParameters.Find(id);
            if (systemParameter == null)
            {
                return HttpNotFound();
            }
            return View(systemParameter);
        }

        // POST: SystemParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemParameter systemParameter = db.SystemParameters.Find(id);
            db.SystemParameters.Remove(systemParameter);
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
