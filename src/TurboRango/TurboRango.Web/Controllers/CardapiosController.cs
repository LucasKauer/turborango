﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurboRango.Dominio;
using TurboRango.Web.Models;

namespace TurboRango.Web.Controllers
{
    public class CardapiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cardapios
        public ActionResult Index()
        {
            return View(db.Cardapios.ToList());
        }

        // GET: Cardapios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cardapio cardapio = db.Cardapios.Find(id);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // GET: Cardapios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cardapios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Preco,DateTime,Categoria,InformacaoLegal")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                db.Cardapios.Add(cardapio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cardapio);
        }

        // GET: Cardapios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cardapio cardapio = db.Cardapios.Find(id);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // POST: Cardapios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Preco,DateTime,Categoria,InformacaoLegal")] Cardapio cardapio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cardapio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cardapio);
        }

        // GET: Cardapios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cardapio cardapio = db.Cardapios.Find(id);
            if (cardapio == null)
            {
                return HttpNotFound();
            }
            return View(cardapio);
        }

        // POST: Cardapios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cardapio cardapio = db.Cardapios.Find(id);
            db.Cardapios.Remove(cardapio);
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

        [AllowAnonymous]
        public JsonResult Pratos()
        {
            var todos = db.Cardapios
                .ToList();

            return Json(new
            { cardapios = todos }, JsonRequestBehavior.AllowGet);
        }

    }

}
