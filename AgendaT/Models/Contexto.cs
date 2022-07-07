using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgendaT.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Persona> Persona { get; set; }

        public Contexto(DbContextOptions<Contexto> opciones):base(opciones)
        {

        }
    }
}
