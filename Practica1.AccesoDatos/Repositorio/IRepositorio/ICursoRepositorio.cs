using Microsoft.AspNetCore.Mvc.Rendering;
using Practica1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        void Actualizar(Curso curso);
        IEnumerable<SelectListItem>ObtenerTodosDropdownList(string obj);
    }
}
