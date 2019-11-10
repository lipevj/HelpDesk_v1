using HelpDeskTCC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskTCC.Models
{
    public class Chamados
    {
        [Key]
        [Display(Name = "Ticket")]
        public int ChamadosId { get; set; }

        [Required(ErrorMessage = "Titulo Obrigatorio")]
        [MaxLength(25, ErrorMessage = "Digite no máximo 25 caracteres !")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Descrição Obrigatoria")]
        [MaxLength(100, ErrorMessage = "Digite no máximo 100 caracteres !")]
        public string Descrição { get; set; }
        public String Dt_Abertura { get; set; }

        //[Required(ErrorMessage = "Solicitante Obrigatorio")]
        public String Solicitante { get; set; }

        internal static object FromSqlRaw(string v)
        {
            throw new NotImplementedException();
        }

        [Required]
        public int PrioridadeId { get; set; }
        public virtual Prioridades Prioridade { get; set; }


        [Display(Name = "Prazo Encerramento")]
        public String Prazo { get; set; }

        [Required]
        public int CategoriaId { get; set; }
        public virtual Categorias Categoria { get; set; }


        public String Responsavel { get; set; }

        public String Dt_Atendimento { get; set; }

        public String Dt_Encerramento { get; set; }

        public int StatusId { get; set; }
        public virtual Status Statu { get; set; }

        public String Comentario { get; set; }

    }
}