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
	public class UbicacionController : ApiController
	{
		private AHI db = new AHI();

		// GET api/Ubicacion
		public IEnumerable<Ubicacion> GetUbicacions()
		{
			var ubicacion = db.Ubicacion.Include(u => u.viaje);
			return ubicacion.AsEnumerable();
		}

		// GET api/Ubicacion/5
		public Ubicacion GetUbicacion(int id)
		{
			Ubicacion ubicacion = db.Ubicacion.Find(id);
			if (ubicacion == null)
			{
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
			}

			return ubicacion;
		}

		// PUT api/Ubicacion/5
		public HttpResponseMessage PutUbicacion(int id, Ubicacion ubicacion)
		{
			if (ModelState.IsValid && id == ubicacion.idubicacion)
			{
				db.Entry(ubicacion).State = EntityState.Modified;

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

		// POST api/Ubicacion
		public HttpResponseMessage PostUbicacion(Ubicacion ubicacion)
		{
			ubicacion.hora = DateTime.Now;
			if (ModelState.IsValid)
			{
				db.Ubicacion.Add(ubicacion);
				db.SaveChanges();
				HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, ubicacion);
				response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = ubicacion.idubicacion }));
				return response;
			}
			else
				return Request.CreateResponse(HttpStatusCode.BadRequest);
		}

		// DELETE api/Ubicacion/5
		public HttpResponseMessage DeleteUbicacion(int id)
		{
			Ubicacion ubicacion = db.Ubicacion.Find(id);
			if (ubicacion == null)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			db.Ubicacion.Remove(ubicacion);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				return Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return Request.CreateResponse(HttpStatusCode.OK, ubicacion);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
	}
}