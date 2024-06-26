﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Practica1.Modelos;
using System.Reflection;

namespace Practica1.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoria>Categorias { get; set; }
        public DbSet<Marca>Marcas { get; set; }
        public DbSet<Producto>Productos { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Curso>Cursos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
