﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Practica1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto>
    {
        void Actualizar(Producto producto);
        IEnumerable<SelectListItem>ObtenerTodosDropdownList(string obj);
    }
}
