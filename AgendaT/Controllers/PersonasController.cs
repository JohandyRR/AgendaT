using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using AgendaT.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;


namespace AgendaT.Controllers
{
    public class PersonasController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PersonasController(Contexto contexto, IWebHostEnvironment webHostEnvironment)
        {
            _contexto = contexto;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Persona.ToListAsync());
        }

        [HttpGet]
        public IActionResult CrearPersona()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearPersona(Persona persona, IFormFile Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    string directorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nombrefoto = Guid.NewGuid().ToString() + Foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(directorio, nombrefoto), FileMode.Create))
                    {
                        await Foto.CopyToAsync(fileStream);
                        persona.Foto = "~/img" + nombrefoto;
                    }
                }
                else
                {
                    persona.Foto = "~/img/usuario.png";
                }

                _contexto.Add(persona);
                await _contexto.SaveChangesAsync();
                TempData["ContactoNuevo"] = $"Contacto con nombre {persona.Nombre} {persona.Apellido} Agregado Exitosamente";
                return RedirectToAction(nameof(Index));
            }

            return View(persona);
        }

        [HttpGet]
        public async Task<IActionResult> ActualizarPersona(int PersonaId)
        {
            Persona persona = await _contexto.Persona.FindAsync(PersonaId);

            if(persona == null)
                return NotFound();
            TempData["Foto"] = persona.Foto;
            return View(persona);
        }
        [HttpPost]
        public async Task<IActionResult> ActualizarPersona(Persona persona, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if(foto != null)
                {
                    string directorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nombrefoto = Guid.NewGuid().ToString() + foto.FileName;

                    using(FileStream fileStream =  new FileStream(Path.Combine(directorio, nombrefoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        persona.Foto = "~/img/" + nombrefoto;
                        TempData["Foto"] = null;
                    }
                }
                else
                {
                    persona.Foto = TempData["Foto"].ToString();
                }

                _contexto.Update(persona);
                await _contexto.SaveChangesAsync();
                TempData["ContactoActualizado"] = $"Contacto con nombre {persona.Nombre} {persona.Apellido} Actualizado con exito";
                return RedirectToAction(nameof(Index));
            }

            return View(persona);
        }

        [HttpPost]
        public async Task<JsonResult> EliminarPersona(int PersonaId)
        {
            Persona persona = await _contexto.Persona.FindAsync(PersonaId);
            _contexto.Persona.Remove(persona);
            await _contexto.SaveChangesAsync();
            TempData["ContactoEliminado"] = $"Contacto con nombre {persona.Nombre} {persona.Apellido} Eliminado con exito";
            return Json(true);
        }
    }
}
