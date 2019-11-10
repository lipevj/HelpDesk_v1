using System.Collections;
using System.Data.Entity;

namespace HelpDeskTCC.Models
{
    public class conexaoContext : DbContext
    {
        public conexaoContext() : base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<HelpDeskTCC.Models.Prioridades> Prioridades { get; set; }

        public System.Data.Entity.DbSet<HelpDeskTCC.Models.Categorias> Categorias { get; set; }

        public System.Data.Entity.DbSet<HelpDeskTCC.Models.Chamados> Chamados { get; set; }

        public System.Data.Entity.DbSet<HelpDeskTCC.Models.Status> Status { get; set; }
        public IEnumerable ContaRegistrarViewModel { get; internal set; }

    }
}