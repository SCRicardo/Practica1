using Microsoft.AspNetCore.Mvc.Rendering;
using Practica1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProfesorRepositorio : IRepositorio<Profesor>
    {
        void Actualizar(Profesor profesor);
        IEnumerable<SelectListItem>ObtenerTodosDropdownList(string obj);
    }
}
