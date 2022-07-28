using Microsoft.EntityFrameworkCore;
using OpBancarias.Data.Models;

namespace OpBancarias.Data
{
    public partial class OpBancariasContext
    {
        public virtual DbSet<Usuario> Usuarios { get; set; }

    }
}
