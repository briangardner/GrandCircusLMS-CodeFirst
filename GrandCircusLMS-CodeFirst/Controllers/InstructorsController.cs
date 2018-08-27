using System.Net;
using System.Web.Mvc;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Models;

namespace GrandCircusLMS_CodeFirst.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public InstructorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        // GET: Instructors
        public ActionResult Index()
        {
            return View(_unitOfWork.Repository<Instructor>().GetAll());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _unitOfWork.Repository<Instructor>().GetSingle(id.Value);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OfficeNumber,FirstName,LastName,DateOfBirth,SocialSecurityNumber")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Instructor>().Insert(instructor);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _unitOfWork.Repository<Instructor>().GetSingle(id.Value);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OfficeNumber,FirstName,LastName,DateOfBirth,SocialSecurityNumber")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Instructor>().Update(instructor);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = _unitOfWork.Repository<Instructor>().GetSingle(id.Value);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = _unitOfWork.Repository<Instructor>().GetSingle(id);
            _unitOfWork.Repository<Instructor>().Delete(instructor);
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
