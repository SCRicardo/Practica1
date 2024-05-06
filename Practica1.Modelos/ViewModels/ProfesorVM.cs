﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.Modelos.ViewModels
{
    public class ProfesorVM
    {
        public Profesor  Profesor { get; set; }
        public IEnumerable<SelectListItem> DepartamentoLista { get; set; } //IEnumerable es el tipo de dato para Listas
        public IEnumerable<SelectListItem> PadreLista { get; set; }
    }
}
