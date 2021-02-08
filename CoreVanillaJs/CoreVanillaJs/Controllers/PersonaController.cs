using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreVanillaJs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("permitir")]
    public class PersonaController : ControllerBase
    {
        [HttpGet]
        
        public JsonResult Get()
        {
            using (Models.CRUDContext db = new Models.CRUDContext())
            {
                var lst = (from d in db.Personas select d).ToList();
                return new JsonResult(lst);
            }
        }

        [HttpPost]
        public JsonResult Post([FromBody] Models.Request.PersonaRequest model)
        {
            using (Models.CRUDContext db = new Models.CRUDContext())
            {
                Models.Persona oPersonas = new Models.Persona();
                oPersonas.Nombre = model.Nombre;
                oPersonas.Edad = model.Edad;
                db.Personas.Add(oPersonas);
                db.SaveChanges();
            }
            return new JsonResult(model);
        }

        [HttpPut]
        public JsonResult Put([FromBody] Models.Request.PersonaEditRequest model)
        {
            using (Models.CRUDContext db = new Models.CRUDContext())
            {
                Models.Persona oPersonas = db.Personas.Find(model.Id);
                oPersonas.Nombre = model.Nombre;
                oPersonas.Edad = model.Edad;
                db.Entry(oPersonas).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            return new JsonResult(model);
        }

        [HttpDelete]
        public JsonResult Delete([FromBody] Models.Request.PersonaEditRequest model)
        {
            using (Models.CRUDContext db = new Models.CRUDContext())
            {
                Models.Persona oPersonas = db.Personas.Find(model.Id);
                db.Personas.Remove(oPersonas);
                db.SaveChanges();
            }
            return new JsonResult(model);
        }



    }
}
