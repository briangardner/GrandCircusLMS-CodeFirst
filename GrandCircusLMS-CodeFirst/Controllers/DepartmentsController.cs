using System.Linq;
using System.Net;
using System.Web.Mvc;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS_CodeFirst.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Departments
        public ActionResult Index()
        {
            var departments = _unitOfWork.Repository<Department>().GetAllIncluding(d => d.Location);
            return View(departments.ToList());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _unitOfWork.Repository<Department>().GetSingle(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LocationId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Department>().Insert(department);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name", department.LocationId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _unitOfWork.Repository<Department>().GetSingle(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name", department.LocationId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LocationId")] Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Department>().Update(department);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name", department.LocationId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _unitOfWork.Repository<Department>().GetSingle(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = _unitOfWork.Repository<Department>().GetSingle(id);
            _unitOfWork.Repository<Department>().Delete(department);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
