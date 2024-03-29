﻿using HelpDeskTCC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Windows.Input;

namespace HelpDeskTCC.ViewModels
{
    public class ContaRegistrarViewModel
    {   
        
        
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }


    }
}