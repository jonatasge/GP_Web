using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VirtualPlay.Business.Models;
using VirtualPlay.MyAccount.Managers;

namespace VirtualPlay.MyAccount.Areas.MyAccount.Controllers
{
    public class ProfileController : Controller
    {
        private Entities db = new Entities();

        // GET: /MyAccount/Profile/
        public async Task<ActionResult> Index()
        {
            var sys_user = db.Sys_User.Include(s => s.Ent_Enterprise2).Include(s => s.Per_Person2).Include(s => s.Sys_Role2).Include(s => s.Sys_User2).Include(s => s.Sys_User3);
            return View(await sys_user.ToListAsync());
        }

        // GET: /MyAccount/Profile/Details/5
        public async Task<ActionResult> Details()
        {
            Sys_User sys_user = await db.Sys_User.FindAsync(UserManager.User.id);
            if (sys_user == null)
            {
                return HttpNotFound();
            }
            return View(sys_user);
        }

        // GET: /MyAccount/Profile/Create
        public ActionResult Create()
        {
            ViewBag.idEnterprise = new SelectList(db.Ent_Enterprise, "idEnterprise", "idEnterpriseSource");
            ViewBag.idPerson = new SelectList(db.Per_Person, "idPerson", "idPersonSource");
            ViewBag.idRole = new SelectList(db.Sys_Role, "idRole", "nmRole");
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser");
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser");
            return View();
        }

        // POST: /MyAccount/Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="idUser,idRole,idPerson,idEnterprise,nmUser,dsEmail,dsPassword,dsAcessToken,dsSession,dtExpire,stUser,idUserCreate,dtCreate,idUserLastUpdate,dtLastUpdate,dtLastSession,idMerchant")] Sys_User sys_user)
        {
            if (ModelState.IsValid)
            {
                db.Sys_User.Add(sys_user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idEnterprise = new SelectList(db.Ent_Enterprise, "idEnterprise", "idEnterpriseSource", sys_user.idEnterprise);
            ViewBag.idPerson = new SelectList(db.Per_Person, "idPerson", "idPersonSource", sys_user.idPerson);
            ViewBag.idRole = new SelectList(db.Sys_Role, "idRole", "nmRole", sys_user.idRole);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", sys_user.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", sys_user.idUserLastUpdate);
            return View(sys_user);
        }

        // GET: /MyAccount/Profile/Edit/5
        public async Task<ActionResult> Edit()
        {
            Sys_User sys_user = await db.Sys_User.FindAsync(UserManager.User.id);
            if (sys_user == null)
            {
                return HttpNotFound();
            }
            ViewBag.idEnterprise = new SelectList(db.Ent_Enterprise, "idEnterprise", "idEnterpriseSource", sys_user.idEnterprise);
            ViewBag.idPerson = new SelectList(db.Per_Person, "idPerson", "idPersonSource", sys_user.idPerson);
            ViewBag.idRole = new SelectList(db.Sys_Role, "idRole", "nmRole", sys_user.idRole);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", sys_user.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", sys_user.idUserLastUpdate);
            return View(sys_user);
        }

        // POST: /MyAccount/Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="idUser,idRole,idPerson,idEnterprise,nmUser,dsEmail,dsPassword,dsAcessToken,dsSession,dtExpire,stUser,idUserCreate,dtCreate,idUserLastUpdate,dtLastUpdate,dtLastSession,idMerchant")] Sys_User sys_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sys_user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idEnterprise = new SelectList(db.Ent_Enterprise, "idEnterprise", "idEnterpriseSource", sys_user.idEnterprise);
            ViewBag.idPerson = new SelectList(db.Per_Person, "idPerson", "idPersonSource", sys_user.idPerson);
            ViewBag.idRole = new SelectList(db.Sys_Role, "idRole", "nmRole", sys_user.idRole);
            ViewBag.idUserCreate = new SelectList(db.Sys_User, "idUser", "nmUser", sys_user.idUserCreate);
            ViewBag.idUserLastUpdate = new SelectList(db.Sys_User, "idUser", "nmUser", sys_user.idUserLastUpdate);
            return View(sys_user);
        }

        // GET: /MyAccount/Profile/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sys_User sys_user = await db.Sys_User.FindAsync(id);
            if (sys_user == null)
            {
                return HttpNotFound();
            }
            return View(sys_user);
        }

        // POST: /MyAccount/Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sys_User sys_user = await db.Sys_User.FindAsync(id);
            db.Sys_User.Remove(sys_user);
            await db.SaveChangesAsync();
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
