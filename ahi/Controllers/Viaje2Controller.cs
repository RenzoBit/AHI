using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ahi.Models;
using System.Web.Security;

namespace ahi.Controllers
{
    public class Viaje2Controller : Controller
    {
        private AHI db = new AHI();

        //
        // GET: /Viaje2/

        public ActionResult Index(int idusuario)
        {
			return View(db.Usuario.Find(idusuario));
        }

		public ActionResult Index2(int idusuario)
		{
			FormsAuthentication.SetAuthCookie(db.Usuario.Find(idusuario).username, true);
			return RedirectToAction("Index", "Viaje2", new { idusuario = idusuario }); ;
		}

        //
        // GET: /Viaje2/Details/5

        public ActionResult Details(int id = 0)
        {
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        //
        // GET: /Viaje2/Create

        public ActionResult Create()
        {
            ViewBag.iddispositivo = new SelectList(db.Dispositivo, "iddispositivo", "mac");
            ViewBag.idvehiculo = new SelectList(db.Vehiculo, "idvehiculo", "placa");
            return View();
        }

        //
        // POST: /Viaje2/Create

        [HttpPost]
        public ActionResult Create(Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                db.Viaje.Add(viaje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.iddispositivo = new SelectList(db.Dispositivo, "iddispositivo", "mac", viaje.iddispositivo);
            ViewBag.idvehiculo = new SelectList(db.Vehiculo, "idvehiculo", "placa", viaje.idvehiculo);
            return View(viaje);
        }

        //
        // GET: /Viaje2/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            ViewBag.iddispositivo = new SelectList(db.Dispositivo, "iddispositivo", "mac", viaje.iddispositivo);
            ViewBag.idvehiculo = new SelectList(db.Vehiculo, "idvehiculo", "placa", viaje.idvehiculo);
            return View(viaje);
        }

        //
        // POST: /Viaje2/Edit/5

        [HttpPost]
        public ActionResult Edit(Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.iddispositivo = new SelectList(db.Dispositivo, "iddispositivo", "mac", viaje.iddispositivo);
            ViewBag.idvehiculo = new SelectList(db.Vehiculo, "idvehiculo", "placa", viaje.idvehiculo);
            return View(viaje);
        }

        //
        // GET: /Viaje2/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return HttpNotFound();
            }
            return View(viaje);
        }

        //
        // POST: /Viaje2/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Viaje viaje = db.Viaje.Find(id);
            db.Viaje.Remove(viaje);
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