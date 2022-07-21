using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpBancarias.Data
{
    public partial class OpBancariasContext
    {
        public DbSet<Persona> Personas { get; set; }
    }
}
