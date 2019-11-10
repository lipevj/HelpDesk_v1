using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using HelpDeskTCC.Models;
using Microsoft.AspNet.Identity;


namespace HelpDeskTCC.Controllers
{
    [Authorize]
    public class ChamadosController : Controller
    {
        private conexaoContext db = new conexaoContext();

        // GET: Chamados
        public ActionResult Index()
        {
            var usuarioLogado = @User.Identity.GetUserName();
            var chamados = db.Chamados.Include(c => c.Categoria)
                                      .Include(c => c.Prioridade)
                                      .Include(c => c.Statu)
                                      .Where(c => c.Statu.StatusId == 0 
                                                && c.Responsavel == null 
                                                && c.Dt_Atendimento == null 
                                                && c.Dt_Encerramento == null
                                          );
            //.Where(c => c.Prioridade.PrioridadeId == 1);

            //var produtos = context.Produtos
            //   .Include(p => p.Fabricante)
            //   .Include(p => p.Categoria)
            //   .Where(p => p.Categoria.Nome == "Calçados" && p.Fabricante.Nome != "Adidas")
            //   .ToList();


            return View(chamados);

            //return View(chamados.ToList());
        }


        //Chamados Atendidos

        public ActionResult status_atendidos()
        {
            var usuarioLogado = @User.Identity.GetUserName();
            var chamados = db.Chamados.Include(c => c.Categoria)
                                      .Include(c => c.Prioridade)
                                      .Include(c => c.Statu)
                                      .Where(c => c.Statu.StatusId == 2
                                       && c.Responsavel == usuarioLogado 
                                       || usuarioLogado == "Administrador" 
                                       && c.Dt_Atendimento != null
                                       && c.Dt_Encerramento == null);
                                 
                                      //.Where(c => c.Statu.StatusId == 0 && c.Responsavel != null);             // 1 = Fechado
                                                                                                                 // 0 = Aberto

            return View(chamados.ToList());
        }

        //Chamados Finalizados
        [Authorize(Roles ="Administrador, Analista")]
        public ActionResult status_fechado()
        {
            var chamados = db.Chamados.Include(c => c.Categoria)
                                      .Include(c => c.Prioridade)
                                      .Include(c => c.Statu)
                                      .Where(c => c.Statu.StatusId == 1);          // 1 = Fechado
                                                                                   // 0 = Aberto

            return View(chamados.ToList());
        }



        // Meus chamados
        [Authorize(Roles ="Cliente")]
        public ActionResult meusChamados()
        {

            var usuarioLogado = @User.Identity.GetUserName();

            var chamados = db.Chamados.Include(c => c.Categoria)
                                      .Include(c => c.Prioridade)
                                      .Include(c => c.Statu)
                                      .Where(c => c.Solicitante == usuarioLogado);
            return View(chamados.ToList());
        }


        // Meus atendimentos(Analista)
        public ActionResult meusAtendimentos()
        {

            var usuarioLogado = @User.Identity.GetUserName();

            var chamados = db.Chamados.Include(c => c.Categoria)
                                      .Include(c => c.Prioridade)
                                      .Include(c => c.Statu)
                                      .Where(c => c.Solicitante == usuarioLogado);
            return View(chamados.ToList());


        }



        // GET: Chamados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamados chamados = db.Chamados.Find(id);
            if (chamados == null)
            {
                return HttpNotFound();
            }
            return View(chamados);
        }

        // GET: Chamados/Create
        [Authorize(Roles ="Cliente")]
        public ActionResult Create()
        {
          
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição");
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição");

           
            return View();
        }

        // POST: Chamados/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public ActionResult Create([Bind(Include = "ChamadosId,Titulo,Descrição,Dt_Abertura,Solicitante,PrioridadeId,Prazo,CategoriaId,Responsavel,Dt_Atendimento,Dt_Encerramento,StatusId,Comentario")] Chamados chamados)
        {
            if (ModelState.IsValid)
            {
                chamados.Solicitante = User.Identity.Name;
                db.Chamados.Add(chamados);
                
                db.SaveChanges();
                return RedirectToAction("meusChamados");
            }


            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição", chamados.CategoriaId);
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome", chamados.PrioridadeId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição", chamados.StatusId);
            //var usuarioLogado = @User.Identity.GetUserName();
            


            return View(chamados);
        }

        // GET: Chamados/Edit/5
        [Authorize(Roles ="Administrador, Analista")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamados chamados = db.Chamados.Find(id);
            if (chamados == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição", chamados.CategoriaId);
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome", chamados.PrioridadeId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição", chamados.StatusId);
            return View(chamados);
        }

        // POST: Chamados/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChamadosId,Titulo,Descrição,Dt_Abertura,Solicitante,PrioridadeId,Prazo,CategoriaId,Responsavel,Dt_Atendimento,Dt_Encerramento,StatusId,Comentario")] Chamados chamados)
        {
            if (ModelState.IsValid)
            {
                chamados.Responsavel = User.Identity.Name;
                db.Entry(chamados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição", chamados.CategoriaId);
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome", chamados.PrioridadeId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição", chamados.StatusId);
            return View(chamados);
        }





        // GET: Chamados/Devolver_chamado
        public ActionResult Devolver_chamado(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamados chamados = db.Chamados.Find(id);
            if (chamados == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição", chamados.CategoriaId);
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome", chamados.PrioridadeId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição", chamados.StatusId);
            return View(chamados);
        }

        // POST: Chamados/Devolver_chamado
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Devolver_chamado([Bind(Include = "ChamadosId,Titulo,Descrição,Dt_Abertura,Solicitante,PrioridadeId,Prazo,CategoriaId,Responsavel,Dt_Atendimento,Dt_Encerramento,StatusId,Comentario")] Chamados chamados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição", chamados.CategoriaId);
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome", chamados.PrioridadeId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição", chamados.StatusId);
            return View(chamados);
        }
















        // GET: Chamados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chamados chamados = db.Chamados.Find(id);
            if (chamados == null)
            {
                return HttpNotFound();
            }
            return View(chamados);
        }

        // POST: Chamados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chamados chamados = db.Chamados.Find(id);
            db.Chamados.Remove(chamados);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}



