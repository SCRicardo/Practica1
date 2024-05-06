using Microsoft.AspNetCore.Mvc;
using Practica1.AccesoDatos.Repositorio.IRepositorio;
using Practica1.Modelos;
using Practica1.Modelos.ViewModels;
using Practica1.Utilidades;

namespace Practica1.Areas.Admin.Controllers
{
    [Area("Admin")] //Le decimos a que Area pertecene
    public class CursoController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CursoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
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
            CursoVM cursoVM = new CursoVM()
            {
                Curso = new Curso(),
                ProfesorLista = _unidadTrabajo.Curso.ObtenerTodosDropdownList("Profesor"),
                PadreLista=_unidadTrabajo.Curso.ObtenerTodosDropdownList("Curso")
            };
            if (id == null)
            {
                //Crear nuevo Producto
                return View(cursoVM);
            }
            else
            {
                //Vamos a actualizar a producto
                cursoVM.Curso = await _unidadTrabajo.Curso.obtener(id.GetValueOrDefault());
                if (cursoVM.Curso == null)
                {
                    return NotFound();
                }
                return View(cursoVM);
            }
        }



        #region API
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CursoVM cursoVM)
        {
            if(ModelState.IsValid)
            {
                //Definir la variable para obtener los archivos desde el formulario
                //En este caso es una sola imagen
                var files = HttpContext.Request.Form.Files;
                //Definir una variable para contruir la ruta del directorio wwwroot
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (cursoVM.Curso.Id == 0)
                {
                    //Creacion de un producto nuevo
                    //Preparamos al modelo para ser guardado
                    await _unidadTrabajo.Curso.Agregar(cursoVM.Curso);
                }
                else
                {
                    //Actualizar un Producto existente
                    //Hacemos la consulta del registro a modificar
                    var objCurso = await _unidadTrabajo.Curso.obtenerPrimero(p=>p.Id== cursoVM.Curso.Id,
                        isTracking:false);
                    _unidadTrabajo.Curso.Actualizar(cursoVM.Curso);
                }
                TempData[DS.Exitosa] = "Curso registrado con éxito";
                await _unidadTrabajo.Guardar();
                return View("Index");
            }
            //Si el modelState es inválido
            TempData[DS.Error] = "Algo salió MAL :(";
            cursoVM.ProfesorLista = _unidadTrabajo.Curso.ObtenerTodosDropdownList("Profesor");
            cursoVM.PadreLista = _unidadTrabajo.Curso.ObtenerTodosDropdownList("Curso");
            return View(cursoVM);
        }

        [HttpGet]
        public async Task<IActionResult> obtenerTodos()
        {
            var todos=await _unidadTrabajo.Curso.obtenerTodos(incluirPropiedades:"Profesor");
            return Json(new {data=todos});  //data es el nombre que tiene que tener la tabla por defecto para crear el JSON
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cursoDB = await _unidadTrabajo.Curso.obtener(id);
            if (cursoDB == null)
            {
                return Json(new { success = false, message = "Error al borrar Curso." });
            }
            _unidadTrabajo.Curso.Remover(cursoDB);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Curso borrado exitósamente." });
        }


        [ActionName("ValidarNombre")]
        public async Task<IActionResult>ValidarNombre(string serie,int id = 0)
        {
            bool valor=false;
            var lista = await _unidadTrabajo.Curso.obtenerTodos();
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
