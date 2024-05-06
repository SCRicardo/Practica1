using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practica1.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.AccesoDatos.Configuracion
{
    public class ProfesorConfiguracion : IEntityTypeConfiguration<Profesor>
    {
        public void Configure(EntityTypeBuilder<Profesor> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Nombre).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descripcion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Telefono).IsRequired();
            builder.Property(x=>x.DepartamentoId).IsRequired();
            builder.Property(x => x.PadreId).IsRequired(false);

            //Relaciones
            builder.HasOne(x=>x.Departamento).WithMany().HasForeignKey(x=>x.DepartamentoId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Padre).WithMany().HasForeignKey(x => x.PadreId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
