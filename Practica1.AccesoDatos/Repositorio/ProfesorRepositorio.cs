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
    public class ProfesorRepositorio : Repositorio<Profesor>, IProfesorRepositorio
    {
        private readonly ApplicationDbContext _db;
        public ProfesorRepositorio(ApplicationDbContext db): base(db) //heredan todo eso a sus papas
        {
            _db = db; 
        }
        public void Actualizar(Profesor profesor)
        {
            var profesorBD = _db.Profesores.FirstOrDefault(b => b.Id == profesor.Id);
            if (profesorBD != null)
            {
                profesorBD.Nombre = profesor.Nombre;
                profesorBD.Descripcion= profesor.Descripcion;
                profesorBD.Telefono = profesor.Telefono;
                profesorBD.DepartamentoId = profesor.DepartamentoId;
                profesorBD.PadreId = profesor.PadreId;
                _db.SaveChanges();
            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropdownList(string obj)
        {
            if (obj == "Departamento")
            {
                return _db.Departamentos.Select(c => new SelectListItem {
                    Text =c.Nombre,
                    Value=c.Id.ToString()
                });      
            }
            if (obj == "Profesor")
            {
                return _db.Profesores.Select(c => new SelectListItem
                {
                    Text = c.Descripcion,
                    Value = c.Id.ToString()
                });
            }
            return null;
        }
    }
}
