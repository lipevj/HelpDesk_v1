﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelpDeskTCC.Controllers
{
    [Authorize(Roles ="Administrador")]
    public class AdministracaoController : Controller
    {
        // GET: Administracao
        
        public ActionResult Index()
        {
            return View();
        }
    }
}