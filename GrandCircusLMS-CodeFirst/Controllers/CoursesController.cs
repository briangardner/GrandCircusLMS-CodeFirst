using System.Linq;
using System.Net;
using System.Web.Mvc;
using GrandCircusLMS.Domain.Interfaces;
using GrandCircusLMS.Domain.Interfaces.Services;
using GrandCircusLMS.Domain.Models;
using GrandCircusLMS_CodeFirst.Models.Courses;

namespace GrandCircusLMS_CodeFirst.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(ICourseService courseService, 
            IUnitOfWork unitOfWork)
        {
            _courseService = courseService;
            _unitOfWork = unitOfWork;
        }

        // GET: Courses
        public ActionResult Index()
        {
            var courses = _unitOfWork.Repository<Course>().GetAllIncluding(x => x.Location, x => x.ProgramManager);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = _unitOfWork.Repository<Course>().GetSingle(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/CourseDetails/5
        public ActionResult CourseDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = _unitOfWork.Repository<Course>().GetSingle(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            var vm = new CourseDetailsViewModel()
            {
                Credits = course.Credits,
                Instructors = course.Instructors.ToList(),
                LocationName = course.Location.Name,
                Name = course.Name,
                ProgramManagerName = string.Join(" ", course.ProgramManager.FirstName, course.ProgramManager.LastName),
                StudentsMissingGrade = _courseService.GetStudentsWithoutGrade(course),
                StudentsFailing = _courseService.GetStudentsFailing(course),
                StudentsPassing = _courseService.GetStudentsPassing(course),
            };
            
            
            return View(vm);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name");
            ViewBag.ProgramManagerId = new SelectList(_unitOfWork.Repository<ProgramManager>().GetAll(), "Id", "FirstName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Credits,LocationId,ProgramManagerId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Course>().Insert(course);

                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name", course.LocationId);
            ViewBag.ProgramManagerId = new SelectList(_unitOfWork.Repository<ProgramManager>().GetAll(), "Id", "FirstName", course.ProgramManagerId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = _unitOfWork.Repository<Course>().GetSingle(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name", course.LocationId);
            ViewBag.ProgramManagerId = new SelectList(_unitOfWork.Repository<ProgramManager>().GetAll(), "Id", "FirstName", course.ProgramManagerId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Credits,LocationId,ProgramManagerId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Repository<Course>().Update(course);

                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(_unitOfWork.Repository<Location>().GetAll(), "Id", "Name", course.LocationId);
            ViewBag.ProgramManagerId = new SelectList(_unitOfWork.Repository<ProgramManager>().GetAll(), "Id", "FirstName", course.ProgramManagerId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Course course = _unitOfWork.Repository<Course>().GetSingle(id.Value);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = _unitOfWork.Repository<Course>().GetSingle(id);
            _unitOfWork.Repository<Course>().Delete(course);
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
