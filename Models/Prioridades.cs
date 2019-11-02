using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDeskTCC.Models
{
    public class Prioridades
    {
        [Key]
        public int PrioridadeId { get; set; }
        [Display(Name = "Prioridade")]
        public String Nome { get; set; }

        public virtual ICollection<Chamados> chamado { get; set; }

    }
}