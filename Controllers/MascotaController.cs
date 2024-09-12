using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TAREA___CRUD_CON_EF.Data;
using TAREA___CRUD_CON_EF.Models;
using TAREA___CRUD_CON_EF.ViewModel;

namespace TAREA___CRUD_CON_EF.Controllers
{
    public class MascotaController : Controller
    {
        private readonly ILogger<MascotaController> _logger;
        private readonly ApplicationDbContext _context;

        public MascotaController(ILogger<MascotaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var mascotas = from o in _context.DataMascota select o;
            
            var viewModel = new MascotaViewModel
            {
                FormMascota = new Mascota(),  
                ListMascota = mascotas.ToList() 
            };

            return View(viewModel);
        }

        public IActionResult Editar(int id)
        {
            var mascota = _context.DataMascota.FirstOrDefault(masc => masc.Id == id);
            
            if (mascota == null)
            {
                return NotFound();
            }

            var viewModel = new MascotaViewModel
            {
                FormMascota = mascota,
                ListMascota = _context.DataMascota.ToList()
            };

            return View("Index", viewModel);
        }

        [HttpPost]
        public IActionResult Guardar(MascotaViewModel viewModel)
        {
            if (viewModel.FormMascota.Id == 0)
            {
                // Crear una nueva mascota
                viewModel.FormMascota.FechaNacimiento = viewModel.FormMascota.FechaNacimiento.Date;
                _context.Add(viewModel.FormMascota);
                TempData["Message"] = "Se ha registrado una nueva mascota.";
            }
            else
            {
                // Editar la mascota existente
                var mascotaExistente = _context.DataMascota.FirstOrDefault(m => m.Id == viewModel.FormMascota.Id);
                if (mascotaExistente != null)
                {
                    mascotaExistente.Nombre = viewModel.FormMascota.Nombre;
                    mascotaExistente.Raza = viewModel.FormMascota.Raza;
                    mascotaExistente.Color = viewModel.FormMascota.Color;
                    mascotaExistente.FechaNacimiento = viewModel.FormMascota.FechaNacimiento.Date;
                    _context.Update(mascotaExistente);
                    TempData["Message"] = "Se ha actualizado la mascota.";
                }
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            var mascota = _context.DataMascota.FirstOrDefault(masc => masc.Id == id);
            if (mascota != null)
            {
                _context.Remove(mascota);
                _context.SaveChanges();
                TempData["Message"] = "Se ha eliminado la mascota.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}