using Microsoft.AspNetCore.Mvc;
using Practica1.AccesoDatos.Repositorio.IRepositorio;
using Practica1.Modelos;
using Practica1.Utilidades;

namespace Practica1.Areas.Admin.Controllers
{
    [Area("admin")]
    public class DepartamentoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public DepartamentoController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo= unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Departamento departamento = new Departamento();
            if (id == null)
            {
                //Crear nueva categoria
                return View(departamento);
            }
            departamento = await _unidadTrabajo.Departamento.obtener(id.GetValueOrDefault()); //Nos aseguramos que la información llegue correctamente
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Evita que se pueda clonar 
        public async Task<IActionResult> Upsert(Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                if (departamento.Id == 0)
                {
                    await _unidadTrabajo.Departamento.Agregar(departamento);
                    TempData[DS.Exitosa] = "departamento creada exitósamente";
                }
                else
                {
                    _unidadTrabajo.Departamento.Actualizar(departamento);
                    TempData[DS.Exitosa] = "departamento actualizada exitósamente";
                }
                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al guardar el departamento";
            return View(departamento);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var departamentoDB = await _unidadTrabajo.Departamento.obtener(id);
            if (departamentoDB == null)
            {
                return Json(new { success = false, message = "Error al borrar Departamento." });
            }
            _unidadTrabajo.Departamento.Remover(departamentoDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Departamento borrada exitósamente." });
        }

        #region API 
        [HttpGet]
        public async Task<IActionResult> obtenerTodos()
        {
            var todos = await _unidadTrabajo.Departamento.obtenerTodos();
            return Json(new { data = todos });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(string nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Departamento.obtenerTodos();
            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);
            }
            if (valor)
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        #endregion
    }
}
