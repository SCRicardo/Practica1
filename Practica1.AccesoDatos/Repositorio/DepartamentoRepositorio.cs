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
    public class DepartamentoRepositorio:Repositorio<Departamento>, IDepartamentoRepositorio
    {
        private readonly ApplicationDbContext _db;
        public DepartamentoRepositorio(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public void Actualizar(Departamento departamento)
        {
            var departamentoBD = _db.Departamentos.FirstOrDefault(b=>b.Id == departamento.Id);
            if (departamentoBD != null)
            {
                departamentoBD.Nombre = departamento.Nombre;
                departamentoBD.Descripcion= departamento.Descripcion;
                departamentoBD.Jefe = departamento.Jefe;
                _db.SaveChanges();
            }
        }
    }
}
