using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.Modelos
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(60, ErrorMessage = "EL maximo es de 60 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion es requerido")]
        [MaxLength(100, ErrorMessage = "EL maximo es de 100 caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El nivel es requerido")]
        [MaxLength(50, ErrorMessage = "EL maximo es de 50 caracteres")]
        public string Nivel { get; set; }

        //Realizar la relacion con la tabla Categoria y con la tabla Marca
        [Required(ErrorMessage = "El profesor es requerido")]
        public int ProfesorId { get; set; }
        [ForeignKey("ProfesorId")]
        public Profesor Profesor { get; set; }

        
        //Recursividad al padre
        public int? PadreId { get; set; }
        public virtual Producto Padre { get; set; }
    }
}
