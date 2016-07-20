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
    public class VehiculoController : ApiController
    {
        private AHI db = new AHI();

        // GET api/Vehiculo
        public IEnumerable<Vehiculo> GetVehiculoes()
        {
            return db.Vehiculo.OrderBy(x => x.placa).AsEnumerable();
        }

        // GET api/Vehiculo/5
        public Vehiculo GetVehiculo(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return vehiculo;
        }

        // PUT api/Vehiculo/5
        public HttpResponseMessage PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (ModelState.IsValid && id == vehiculo.idvehiculo)
            {
                db.Entry(vehiculo).State = EntityState.Modified;

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

		// POST api/Vehiculo
		public HttpResponseMessage PostVehiculo(Vehiculo vehiculo)
		{
			if (ModelState.IsValid && db.Vehiculo.Where(x => x.placa == vehiculo.placa).FirstOrDefault() == null)
			{
				db.Vehiculo.Add(vehiculo);
				db.SaveChanges();
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, vehiculo);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = vehiculo.idvehiculo }));
				return response;
			}
			else
				return Request.CreateResponse(HttpStatusCode.BadRequest);
		}

        // DELETE api/Vehiculo/5
        public HttpResponseMessage DeleteVehiculo(int id)
        {
            Vehiculo vehiculo = db.Vehiculo.Find(id);
            if (vehiculo == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Vehiculo.Remove(vehiculo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, vehiculo);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}