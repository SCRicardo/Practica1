using Practica1.AccesoDatos.Data;
using Practica1.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IBodegaRepositorio Bodega { get; private set; }
        public ICategoriaRepositorio Categoria { get; private set; }
        public IMarcaRepositorio Marca { get; private set; }
        public IProductoRepositorio Producto { get; private set; }
        public IDepartamentoRepositorio Departamento { get; private set; }
        public IProfesorRepositorio Profesor { get; private set; }
        public ICursoRepositorio Curso { get; private set; }
        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(db);
            Categoria = new CategoriaRepositorio(db);
            Marca=new MarcaRepositorio(db);
            Producto= new ProductoRepositorio(db);
            Departamento = new DepartamentoRepositorio(db);
            Profesor=new ProfesorRepositorio(db);
            Curso=new CursoRepositorio(db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
