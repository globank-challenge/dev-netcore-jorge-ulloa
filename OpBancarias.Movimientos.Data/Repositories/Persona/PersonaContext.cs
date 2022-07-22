using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Models;

namespace OpBancarias.Data
{
    public partial class OpBancariasContext
    {
        public DbSet<Persona> Personas { get; set; }
    }
}
