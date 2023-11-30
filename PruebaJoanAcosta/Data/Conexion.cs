using Microsoft.EntityFrameworkCore;
using PruebaJoanAcosta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Data
{
    public class Conexion : DbContext
    {
        public Conexion(DbContextOptions<Conexion> options) : base(options)
        {
        }

        public DbSet<Deportista> Deportistas { get; set; }
        public DbSet<DeportistaPeso> DeportistaPeso { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}


