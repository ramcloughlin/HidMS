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
using hidMy.Models_ViewModels;

namespace hidMy.Controllers
{
    public class GLAspectsandElementsController : Controller
    {
        private GLaccountsModel db = new GLaccountsModel();

        // GET: GLAspectsandElements
        public ActionResult Index(int? page, string ipp)
        {

            if (TempData["ExclError"] != null)
            {
                ModelState.AddModelError("", TempData["ExclError"].ToString());
            }
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
            string wc = hs.WhereClause(recolhidas);

            var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList();
            //var result = db.GLaccounts.Where(wc).OrderBy(c => c.hid).ToList();   // dynamic where for sql server using isDescendant at server
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = (page ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View(result.ToPagedList(pageNumber, int.Parse(ipp)));

        }

        // GET: GLApectsandElements/Details/5
        public ActionResult Details(long? id, int? page, string ipp)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            var resultList = db.Database.SqlQuery<GLAspectsandElementLeglislation>("DECLARE @Manager hierarchyid SELECT @Manager = GLAspectsandElements.hid FROM GLAspectsandElements   WHERE AspectsElementsID =" + id + "SELECT        GLAspectsandElements.AspectsElementsID, GLAspectsandElements.Name, GLAspectsandElements.hid, GLAspectsandElements.[Level], GLAspectsandElements.parent, GLAspectsandElements.LeglislationLINK, GLAspectsandElements.Comment, GLAspectsandElements.CommentLINK, GLAccounts.Name AS[Clause], GLAccounts.ClauseText FROM  GLAspectsandElements INNER JOIN   GLAccounts ON GLAspectsandElements.LeglislationLINK = GLAccounts.Account WHERE cast(GLAspectsandElements.hid as hierarchyid).IsDescendantOf(@Manager) = 1 ORDER BY GLAspectsandElements.hid").ToList();

            //if (contas == null)
            //{
            //    return HttpNotFound();

            //}

            if (resultList == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.parent = resultList.First().Name;
            }
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLaccounts> hs = new hidServices<GLaccounts>(db);
            string wc = hs.WhereClause(recolhidas);   // Sql server only
            var result = db.Database.SqlQuery<GLaccounts>("select * from GLaccounts where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = page ?? 1;
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            // return View(resultList.ToList());
            return View(resultList);
        }

        [HttpPost]
        public ActionResult HCommand(GLAspectsandElements conta, int? page, string ipp, string refresh, string promote, string up, string down, string demote)
        {

            hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
            if (string.IsNullOrEmpty(refresh))
            {
                var pk = Session["itemSelecionado"] as String;
                try
                {

                    if (!string.IsNullOrEmpty(promote)) { hs.Command(pk, hidServices<GLAspectsandElements>.Commands.Promote); }
                    if (!string.IsNullOrEmpty(up)) { hs.Command(pk, hidServices<GLAspectsandElements>.Commands.Up); }
                    if (!string.IsNullOrEmpty(down)) { hs.Command(pk, hidServices<GLAspectsandElements>.Commands.Down); }
                    if (!string.IsNullOrEmpty(demote)) { hs.Command(pk, hidServices<GLAspectsandElements>.Commands.Demote); }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            db = new GLaccountsModel();  // to refresh entity after sql command
            string wc = hs.WhereClause(recolhidas);
            var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = page ?? 1;
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
        }

        public ActionResult Expand(long id, int? pagenumber, string ipp)
        {
            AtualizaListaRecolhidos(id.ToString(), false);
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
            string wc = hs.WhereClause(recolhidas);
            var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = (pagenumber ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
        }
        public ActionResult Collapse(long id, int? pagenumber, string ipp)
        {
            AtualizaListaRecolhidos(id.ToString(), true);
            ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
            hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
            string wc = hs.WhereClause(recolhidas);
            var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList(); // Sql server only
            //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
            int pageNumber = (pagenumber ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
        }

        [HttpPost]
        public ActionResult ItemsPerPage(int? page, string ipp)
        {
            string url = Url.Content("~/GLAspectsandElements/Index/");
            return Json(url + "?page=" + page.ToString() + "&ipp=" + ipp);
        }

        [HttpPost]
        public ActionResult LinhaSelecionada(String id, int? page, string ipp)
        {
            GLAspectsandElements contas = db.GLAspectsandElements.Find(long.Parse(id));
            if (contas == null)
            {
                return HttpNotFound();
            }

            if ((String)Session["itemSelecionado"] == contas.AspectsElementsID.ToString())
            { Session["itemSelecionado"] = ""; }
            else
            { Session["itemSelecionado"] = contas.AspectsElementsID.ToString(); }

            string url = Url.Content("~/GLAspectsandElements/Index/");
            return Json(url + "?page=" + page.ToString() + "&ipp=" + ipp);
        }

        // GET: GLaccounts/Create
        public ActionResult Create(int? page, string ipp)
        {
            var mae = Session["itemSelecionado"] as String;
            hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
            if (!hs.RootExists())
            {
                ViewBag.mensagem = "As root account";
                ViewBag.LeglislationLink = new SelectList(db.GLaccounts.OrderBy(x => x.hid), "Account", "Name");
            }
            else
            {
                try
                {
                    GLAspectsandElements r = db.GLAspectsandElements.Find(long.Parse(mae));
                    ViewBag.mensagem = "As last child of Checklist Item " + mae + " (" + r.Name + ")";
                    ViewBag.LeglislationLink = new SelectList(db.GLaccounts.OrderBy(x => x.hid), "Account", "Name");
                  
                    
                    //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));


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
                    var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList(); // Sql server only
                    //ViewBag.LeglislationLink = new SelectList(db.GLLeglislation.OrderBy(x => x.hid), "LeglislationId", "Name", "Level");                                                                                                                                      //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));

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
        public ActionResult Create([Bind(Include = "AspectsandElementsID, Name,  hid, LeglislationLINK")] GLAspectsandElements gLaccounts, int? page, string ipp)
        {

            int pageNumber = (page ?? 1);
            ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
            if (ModelState.IsValid)
            {
                var mae = Session["itemSelecionado"] as String;
                hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
                gLaccounts.hid = hs.GetNextSonHid(mae);
                db.GLAspectsandElements.Add(gLaccounts);
                db.SaveChanges();
                //gLaccounts.InsertNewConta(gLaccounts.Name, mae);
                ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
                string wc = hs.WhereClause(recolhidas);
                var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList(); // Sql server only
                ViewBag.LeglislationLink = new SelectList(db.GLaccounts.OrderBy(x => x.hid), "Account", "Name","ClauseText");

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
            GLAspectsandElements contas = db.GLAspectsandElements.Find(id);
            ViewBag.LeglislationLINK = new SelectList(db.GLaccounts.OrderBy(x => x.hid), "Account", "Name","ClauseText",contas.LeglislationLINK);

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
        public ActionResult Edit([Bind(Include = "AspectsElementsID,Name,hid,Level,parent,LeglislationLINK,Comment,CommentLINK")] GLAspectsandElements gLaccounts, int? page, string ipp)
        {
            if (ModelState.IsValid)
            {
                gLaccounts.hid = (byte[])TempData["hid"];
                db.Entry(gLaccounts).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                int pageNumber = (page ?? 1);
                ipp = ipp ?? (string)Session["DefaultItemsPerPage"];
                ArrayList recolhidas = (ArrayList)Session["ContasCollapsedList"];
                hidServices<GLAspectsandElements> hs = new hidServices<GLAspectsandElements>(db);
                string wc = hs.WhereClause(recolhidas);
                var result = db.Database.SqlQuery<GLAspectsandElements>("select * from GLAspectsandElements where " + wc + " order by hid").ToList(); // Sql server only
                //var result = db.GLaccounts.OrderBy(c => c.hid).ToList().Where(c => c.IsNotCollapsed(hs, recolhidas));
                return View("Index", result.ToPagedList(pageNumber, int.Parse(ipp)));
            }
            return View(gLaccounts);
        }

        // GET: GLAspectsandElements/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GLAspectsandElements gLAspectsandElement = db.GLAspectsandElements.Find(id);
            if (gLAspectsandElement == null)
            {
                return HttpNotFound();
            }
            return View(gLAspectsandElement);
        }

        // POST: GLAspectsandElements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            GLAspectsandElements gLAspectsandElement = db.GLAspectsandElements.Find(id);
            db.GLAspectsandElements.Remove(gLAspectsandElement);
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