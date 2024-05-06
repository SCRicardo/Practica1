using Microsoft.AspNetCore.Mvc;
using Practica1.AccesoDatos.Repositorio.IRepositorio;
using Practica1.Modelos;
using Practica1.Modelos.ViewModels;
using Practica1.Utilidades;

namespace Practica1.Areas.Admin.Controllers
{
    [Area("Admin")] //Le decimos a que Area pertecene
    public class ProfesorController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProfesorController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Método get del upsert
        public async Task<IActionResult>Upsert(int? id)
        {
            ProfesorVM profesorVM = new ProfesorVM()
            {
                Profesor = new Profesor(),
                DepartamentoLista = _unidadTrabajo.Profesor.ObtenerTodosDropdownList("Departamento"),
                PadreLista=_unidadTrabajo.Profesor.ObtenerTodosDropdownList("Profesor")
            };
            if (id == null)
            {
                //Crear nuevo Producto
                return View(profesorVM);
            }
            else
            {
                //Vamos a actualizar a producto
                profesorVM.Profesor = await _unidadTrabajo.Profesor.obtener(id.GetValueOrDefault());
                if (profesorVM.Profesor == null)
                {
                    return NotFound();
                }
                return View(profesorVM);
            }
        }



        #region API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProfesorVM profesorVM)
        {
            if(ModelState.IsValid)
            {
                //Definir la variable para obtener los archivos desde el formulario
                //En este caso es una sola imagen
                var files = HttpContext.Request.Form.Files;
                //Definir una variable para contruir la ruta del directorio wwwroot
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (profesorVM.Profesor.Id == 0)
                {
                    //Creacion de un producto nuevo
                    //Preparamos al modelo para ser guardado
                    await _unidadTrabajo.Profesor.Agregar(profesorVM.Profesor);
                }
                else
                {
                    //Actualizar un Producto existente
                    //Hacemos la consulta del registro a modificar
                    var objProfesor = await _unidadTrabajo.Profesor.obtenerPrimero(p=>p.Id== profesorVM.Profesor.Id,
                        isTracking:false);
                    _unidadTrabajo.Profesor.Actualizar(profesorVM.Profesor);
                }
                TempData[DS.Exitosa] = "Profesor registrado con éxito";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            //Si el modelState es inválido
            TempData[DS.Error] = "Algo salió MAL :(";
            profesorVM.DepartamentoLista = _unidadTrabajo.Profesor.ObtenerTodosDropdownList("Departamento");
            profesorVM.PadreLista = _unidadTrabajo.Profesor.ObtenerTodosDropdownList("Profesor");
            return View(profesorVM);
        }

        [HttpGet]
        public async Task<IActionResult> obtenerTodos()
        {
            var todos=await _unidadTrabajo.Profesor.obtenerTodos(incluirPropiedades:"Departamento");
            return Json(new {data=todos});  //data es el nombre que tiene que tener la tabla por defecto para crear el JSON
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var profesorDB = await _unidadTrabajo.Profesor.obtener(id);
            if (profesorDB == null)
            {
                return Json(new { success = false, message = "Error al borrar Profesor." });
            }
            _unidadTrabajo.Profesor.Remover(profesorDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Profesor borrado exitósamente." });
        }


        [ActionName("ValidarNombre")]
        public async Task<IActionResult>ValidarNombre(string serie,int id = 0)
        {
            bool valor=false;
            var lista = await _unidadTrabajo.Profesor.obtenerTodos();
            if (id == 0)
            {
                valor=lista.Any(b=>b.Nombre.ToLower().Trim()== serie.ToLower().Trim());
            }else
            {
                valor = lista.Any(b=>b.Nombre.ToLower().Trim()== serie.ToLower().Trim() && b.Id!=id);
            }
            if (valor)
            {
                return Json(new {success=true});
            }
			return Json(new { success = false });
		}

        #endregion
    }
}
