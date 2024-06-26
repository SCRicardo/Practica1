﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [MaxLength(60,ErrorMessage ="El campo nombre no es mas de 60 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        [MaxLength(100, ErrorMessage = "El campo descripcion no es mas de 60 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage ="El campo Estado es requerido")]
        public bool Estado { get; set; }

    }
}
