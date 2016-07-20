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
    public class DispositivoController : ApiController
    {
        private AHI db = new AHI();

        // GET api/Dispositivo
        public IEnumerable<Dispositivo> GetDispositivoes()
        {
            return db.Dispositivo.AsEnumerable();
        }

        // GET api/Dispositivo/5
        public Dispositivo GetDispositivo(string mac)
        {
            Dispositivo dispositivo = db.Dispositivo.Where(x => x.mac == mac).FirstOrDefault();
            if (dispositivo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return dispositivo;
        }

        // PUT api/Dispositivo/5
        public HttpResponseMessage PutDispositivo(int id, Dispositivo dispositivo)
        {
            if (ModelState.IsValid && id == dispositivo.iddispositivo)
            {
                db.Entry(dispositivo).State = EntityState.Modified;

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

        // POST api/Dispositivo
        public HttpResponseMessage PostDispositivo(Dispositivo dispositivo)
        {
            if (ModelState.IsValid)
            {
				Dispositivo o = db.Dispositivo.Where(x => x.mac == dispositivo.mac).FirstOrDefault();
				if (o == null)
				{
					db.Dispositivo.Add(dispositivo);
					db.SaveChanges();
				}
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, dispositivo);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = dispositivo.iddispositivo }));
                return response;
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // DELETE api/Dispositivo/5
        public HttpResponseMessage DeleteDispositivo(int id)
        {
            Dispositivo dispositivo = db.Dispositivo.Find(id);
            if (dispositivo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Dispositivo.Remove(dispositivo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dispositivo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}