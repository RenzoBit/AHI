using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ahi.Models;

namespace ahi.Controllers
{
    public class Ubicacion2Controller : Controller
    {
        private AHI db = new AHI();

        //
        // GET: /Ubicacion2/

        public ActionResult Index(int idviaje, int idusuario)
        {
			ViewBag.idusuario = idusuario;
			ViewBag.idviaje = idviaje;
            var ubicacion = db.Ubicacion.Where(x => x.idviaje == idviaje).Include(u => u.viaje);
            return View(ubicacion.ToList());
        }

        //
        // GET: /Ubicacion2/Details/5

        public ActionResult Details(int id = 0)
        {
            Ubicacion ubicacion = db.Ubicacion.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        //
        // GET: /Ubicacion2/Create

        public ActionResult Create()
        {
            ViewBag.idviaje = new SelectList(db.Viaje, "idviaje", "descripcion");
            return View();
        }

        //
        // POST: /Ubicacion2/Create

        [HttpPost]
        public ActionResult Create(Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Ubicacion.Add(ubicacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idviaje = new SelectList(db.Viaje, "idviaje", "descripcion", ubicacion.idviaje);
            return View(ubicacion);
        }

        //
        // GET: /Ubicacion2/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ubicacion ubicacion = db.Ubicacion.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.idviaje = new SelectList(db.Viaje, "idviaje", "descripcion", ubicacion.idviaje);
            return View(ubicacion);
        }

        //
        // POST: /Ubicacion2/Edit/5

        [HttpPost]
        public ActionResult Edit(Ubicacion ubicacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idviaje = new SelectList(db.Viaje, "idviaje", "descripcion", ubicacion.idviaje);
            return View(ubicacion);
        }

        //
        // GET: /Ubicacion2/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ubicacion ubicacion = db.Ubicacion.Find(id);
            if (ubicacion == null)
            {
                return HttpNotFound();
            }
            return View(ubicacion);
        }

        //
        // POST: /Ubicacion2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ubicacion ubicacion = db.Ubicacion.Find(id);
            db.Ubicacion.Remove(ubicacion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}