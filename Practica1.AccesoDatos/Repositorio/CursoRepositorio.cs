using Microsoft.AspNetCore.Mvc.Rendering;
using Practica1.AccesoDatos.Data;
using Practica1.AccesoDatos.Repositorio.IRepositorio;
using Practica1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.AccesoDatos.Repositorio
{
    public class CursoRepositorio : Repositorio<Curso>, ICursoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public CursoRepositorio(ApplicationDbContext db): base(db) //heredan todo eso a sus papas
        {
            _db = db; 
        }
        public void Actualizar(Curso curso)
        {
            var cursoBD = _db.Cursos.FirstOrDefault(b => b.Id == curso.Id);
            if (cursoBD != null)
            {
                cursoBD.Nombre = curso.Nombre;
                cursoBD.Descripcion= curso.Descripcion;
                cursoBD.Nivel = curso.Nivel;
                cursoBD.ProfesorId = curso.ProfesorId;
                cursoBD.PadreId = curso.PadreId;
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownList(string obj)
        {
            if (obj == "Profesor")
            {
                return _db.Profesores.Select(c => new SelectListItem {
                    Text =c.Nombre,
                    Value=c.Id.ToString()
                });      
            }
            if (obj == "Curso")
            {
                return _db.Cursos.Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }
            return null;
        }
    }
}
