using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HelpDeskTCC.Models;

namespace HelpDeskTCC.Controllers
{
    public class Chamados1Controller : Controller
    {
        private conexaoContext db = new conexaoContext();

        // GET: Chamados1
        public ActionResult Index()
        {
            var chamados = db.Chamados.Include(c => c.Categoria).Include(c => c.Prioridade).Include(c => c.Statu);
            return View(chamados.ToList());
        }

        // GET: Chamados1/Details/5
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

        // GET: Chamados1/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição");
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome");
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição");
            return View();
        }

        // POST: Chamados1/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChamadosId,Titulo,Descrição,Dt_Abertura,Solicitante,PrioridadeId,Prazo,CategoriaId,Responsavel,Dt_Atendimento,Dt_Encerramento,StatusId,Id_usuario")] Chamados chamados)
        {
            if (ModelState.IsValid)
            {
                db.Chamados.Add(chamados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Descrição", chamados.CategoriaId);
            ViewBag.PrioridadeId = new SelectList(db.Prioridades, "PrioridadeId", "Nome", chamados.PrioridadeId);
            ViewBag.StatusId = new SelectList(db.Status, "StatusId", "Descrição", chamados.StatusId);
            return View(chamados);
        }

        // GET: Chamados1/Edit/5
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

        // POST: Chamados1/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChamadosId,Titulo,Descrição,Dt_Abertura,Solicitante,PrioridadeId,Prazo,CategoriaId,Responsavel,Dt_Atendimento,Dt_Encerramento,StatusId,Id_usuario")] Chamados chamados)
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

        // GET: Chamados1/Delete/5
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

        // POST: Chamados1/Delete/5
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
