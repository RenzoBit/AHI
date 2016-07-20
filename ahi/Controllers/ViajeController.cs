using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ahi.Models;

namespace ahi.Controllers
{
    public class ViajeController : ApiController
    {
        private AHI db = new AHI();

        // GET api/Viaje
        public IEnumerable<Viaje> GetViajes()
        {
            var viaje = db.Viaje.Include(v => v.dispositivo).Include(v => v.vehiculo);
            return viaje.AsEnumerable();
        }

        // GET api/Viaje/5
        public Viaje GetViaje(int id)
        {
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return viaje;
        }

        // PUT api/Viaje/5
        public HttpResponseMessage PutViaje(int id, Viaje viaje)
        {
            if (ModelState.IsValid && id == viaje.idviaje)
            {
                db.Entry(viaje).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Viaje
        public HttpResponseMessage PostViaje(Viaje viaje)
        {
			viaje.mac = viaje.mac.ToUpper();
            Dispositivo dispositivo = db.Dispositivo.Where(x => x.mac == viaje.mac).FirstOrDefault();
            if (dispositivo == null)
            {
                dispositivo = new Dispositivo() { mac = viaje.mac };
                db.Dispositivo.Add(dispositivo);
                db.SaveChanges();
            }
            viaje.iddispositivo = dispositivo.iddispositivo;
            viaje.horainicio = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Viaje.Add(viaje);
				db.SaveChanges();
				Usuario usuario = db.Usuario.Find(Usuario.ADMINISTRADOR);
				usuario.viajes.Add(viaje);
				db.Entry(usuario).State = EntityState.Modified;
				db.SaveChanges();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, viaje);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = viaje.idviaje }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/Viaje/5
        public HttpResponseMessage DeleteViaje(int id)
        {
            Viaje viaje = db.Viaje.Find(id);
            if (viaje == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Viaje.Remove(viaje);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, viaje);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}