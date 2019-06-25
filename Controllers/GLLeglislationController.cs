using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web.Mvc;
using PagedList;
using System.Collections;
using Hierarchy.SqlServer;
using Hierarchy.Common;
using hidMy.Models;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace hidMy.Controllers
{
    public class GLLeglislationController : Controller
    {
        private GLaccountsModel db = new GLaccountsModel();


        // GET: GLLeglislation
        public ActionResult Index(int? page, string ipp)
        {

            if (TempData["ExclError"] != null)
            {
                ModelState.AddModelError("", TempData["ExclError"].ToString());
            }
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);
            string wc = hs.WhereClause(recolhidas);

            var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList();
            //var result = db.GLaccounts.Where(wc).OrderBy(c => c.hid).ToList();   // dynamic where for sql server using isDescendant at server
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = (page ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View(result.ToPagedList(pageNumber, int.Parse(ipp)));

        }

       
        [HttpPost]
        public ActionResult HCommand(GLLeglislation conta, int? page, string ipp, string refresh, string promote, string up, string down, string demote)
        {

            hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);
            if (string.IsNullOrEmpty(refresh))
            {
                var pk = Session["itemSelecionado"] as String;
                try
                {

                    if (!string.IsNullOrEmpty(promote)) { hs.Command(pk, hidServices<GLLeglislation>.Commands.Promote); }
                    if (!string.IsNullOrEmpty(up)) { hs.Command(pk, hidServices<GLLeglislation>.Commands.Up); }
                    if (!string.IsNullOrEmpty(down)) { hs.Command(pk, hidServices<GLLeglislation>.Commands.Down); }
                    if (!string.IsNullOrEmpty(demote)) { hs.Command(pk, hidServices<GLLeglislation>.Commands.Demote); }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            db = new GLaccountsModel();  // to refresh entity after sql command
            string wc = hs.WhereClause(recolhidas);
            var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = page ?? 1;
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
        }

        public ActionResult Expand(long id, int? pagenumber, string ipp)
        {
            AtualizaListaRecolhidos(id.ToString(), false);
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);
            string wc = hs.WhereClause(recolhidas);
            var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = (pagenumber ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
        }
        public ActionResult Collapse(long id, int? pagenumber, string ipp)
        {
            AtualizaListaRecolhidos(id.ToString(), true);
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);
            string wc = hs.WhereClause(recolhidas);
            var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = (pagenumber ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
        }

        [HttpPost]
        public ActionResult ItemsPerPage(int? page, string ipp)
        {
            string url = Url.Content("~/GLLeglislation/Index/");
            return Json(url + "?page=" + page.ToString() + "&ipp=" + ipp);
        }

        [HttpPost]
        public ActionResult LinhaSelecionada(String id, int? page, string ipp)
        {
            GLLeglislation contas = db.GLLeglislation.Find(long.Parse(id));
            if (contas == null)
            {
                return HttpNotFound();
            }

            if ((String)Session["itemSelecionado"] == contas.LeglislationID.ToString())
            { Session["itemSelecionado"] = ""; }
            else
            { Session["itemSelecionado"] = contas.LeglislationID.ToString(); }

            string url = Url.Content("~/GLLeglislation/Index/");
            return Json(url + "?page=" + page.ToString() + "&ipp=" + ipp);
        }

        // GET: GLaccounts/Create
        public ActionResult Create(int? page, string ipp)
        {
            var mae = Session["itemSelecionado"] as String;
            hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);
            if (!hs.RootExists())
            {
                ViewBag.mensagem = "As root account";
            }
            else
            {
                try
                {
                    GLLeglislation r = db.GLLeglislation.Find(long.Parse(mae));
                    ViewBag.mensagem = "As last child of " + mae + " (" + r.Name + ")";
                }
                catch (Exception)
                {

                    mae = "";
                }
                if (string.IsNullOrEmpty(mae))
                {
                    ModelState.AddModelError("", "Root exists. Please select parent account.");
                    ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
                    string wc = hs.WhereClause(recolhidas);
                    var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList(); // Sql server only
                    //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
                    int pageNumber = (page ?? 1);
                    ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
                    return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
                }
            }
            return View();
        }

        // POST: GLaccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeglislationID, Name, ClauseText, NonComplianceText, RemedialActionText, hid")] GLLeglislation gLaccounts, int? page, string ipp)
        {

            int pageNumber = (page ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            if (ModelState.IsValid)
            {
                var mae = Session["itemSelecionado"] as String;
                hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);

                gLaccounts.hid = hs.GetNextSonHid(mae);
                db.GLLeglislation.Add(gLaccounts);
                db.SaveChanges();
                //gLaccounts.InsertNewConta(gLaccounts.Name, mae);
                ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
                string wc = hs.WhereClause(recolhidas);
                var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList(); // Sql server only
                //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
                return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
            }
            return View(gLaccounts);
        }

        // GET: GLaccounts/Edit/5
        public ActionResult Edit(long? id, int? page, string ipp)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLLeglislation contas = db.GLLeglislation.Find(id);
            TempData["hid"] = contas.hid;
            if (contas == null)
            {
                return HttpNotFound();
            }
            return View(contas);
        }

        // POST: GLaccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeglislationID,Name, ClauseText, NonComplianceText, RemedialActionText,hid")] GLLeglislation gLaccounts, int? page, string ipp)
        {
            if (ModelState.IsValid)
            {
                gLaccounts.hid = (byte[])TempData["hid"];
                db.Entry(gLaccounts).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                int pageNumber = (page ?? 1);
                ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
                ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
                hidServices<GLLeglislation> hs = new hidServices<GLLeglislation>(db);
                string wc = hs.WhereClause(recolhidas);
                var result = db.Database.SqlQuery<GLLeglislation>("select * from GLLeglislation where " + wc + " order by hid").ToList(); // Sql server only
                //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
                return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
            }
            return View(gLaccounts);
        }

       

      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void AtualizaListaRecolhidos(string pk, Boolean recolher)
        {
            ArrayList a = (ArrayList)Session["ContasCollapsedList"];
            if (recolher)
            {
                if (!a.Contains(pk))
                {
                    a.Add(pk);
                }
            }
            else
            {
                a.Remove(pk);
            }
            Session["ContasCollapsedList"] = a;
        }
    }
}