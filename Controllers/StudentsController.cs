using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Mercury.Controllers
{
    public class StudentsController : Controller
    {
        private MercuryEntities db = new MercuryEntities();

        // GET: Students
        public ActionResult Index()
        {
            //var students = db.Students.Include(s => s.School);
            return View(db.sp_GetStudents(null).ToList());
        }

        private void fillInSession()
        {
            if (Session["Students"] == null)
            {
                //add in session["Products"];
                Session["Students"] = new List<sp_GetStudents_Result>();
                List<sp_GetStudents_Result> Students = db.sp_GetStudents(null).ToList();
                foreach (sp_GetStudents_Result student in Students)
                {
                    ((List<sp_GetStudents_Result>)Session["Students"]).Add(student);
                }
            }
        }

        //get products with paging
        public ActionResult getP(int pq_curPage, int pq_rPP)
        {
            fillInSession();

            int total_Records = (from order in (List<sp_GetStudents_Result>)Session["Students"]
                                 select order).Count();

            int skip = (pq_rPP * (pq_curPage - 1));
            if (skip >= total_Records)
            {
                pq_curPage = (int)Math.Ceiling(((double)total_Records) / pq_rPP);
                skip = (pq_rPP * (pq_curPage - 1));
            }

            var Students2 = (from order in (List<sp_GetStudents_Result>)Session["Students"]
                             orderby order.LastName
                             select order).Skip(skip).Take(pq_rPP);

            StringBuilder sb = new StringBuilder(@"{""totalRecords"":" + total_Records + @",""curPage"":" + pq_curPage + @",""data"":");


            JavaScriptSerializer js = new JavaScriptSerializer();

            string json = js.Serialize(Students2);
            sb.Append(json);
            sb.Append("}");

            return this.Content(sb.ToString(), "text/text");
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/GetDashboardView/5
        public ActionResult GetDashboardView(int id)
        {
            sp_GetStudent_DashboardView_Result studentView = db.sp_GetStudent_DashboardView(id).First<sp_GetStudent_DashboardView_Result>();

            var htmlbody = "<table><tr><th colspan='2'>Student Dashboard Information</th></tr><tr><td>School: [schoolname]" +
                            "</ td >< td > Suspensions: [suspensions]" +
                            "</ td ></ tr >< tr >< td > Student Name: [studentname]" +
                            "</ td >< td > RTTP Enrollment Date: [rttpdnrollmentdate]" +
                            "</ td ></ tr >< tr >< td > Grade: [grade]" +
                            "</ td >< td > First Attendance Date: [firstattendancedate]" +
                            "</ td ></ tr >< tr >< td > SIS Number: [sisnumber]" +
                            "</ td >< td > Birthdate: [birthdate]" +
                            "</ td ></ tr >< tr >< td ></ td >< td ></ td ></ tr >< tr >< td > Total Excused Absenses: [totalexcusedabsenses]" +
                            "</ td >< td ></ td ></ tr >< tr >< td > Total UnExcused Absenses: [totalunExcusedabsenses]" +
                            "</ td >< td ></ td ></ tr >< tr >< td > Total Absenses(YTD): [totalabsenses]" +
                            "</ td >< td ></ td ></ tr ></ table > ";

            htmlbody.Replace("[schoolname]", studentView.SchoolName);
            htmlbody.Replace("[suspensions]", studentView.Suspensions.ToString());
            htmlbody.Replace("[studentname]", studentView.StudentName);
            htmlbody.Replace("[rttpdnrollmentdate]", String.Format("MM/dd/yyyy", studentView.RTTPEnrollmentDt));
            htmlbody.Replace("[grade]", studentView.Grade.ToString());
            htmlbody.Replace("[firstattendancedate]", String.Format("MM/dd/yyyy", studentView.FirstAttendanceDt));
            htmlbody.Replace("[sisnumber]", studentView.SISNumber);
            htmlbody.Replace("[birthdate]", String.Format("MM/dd/yyyy", studentView.BirthDt));
            htmlbody.Replace("[totalexcusedabsenses]", studentView.ExcusedAbsense.ToString());
            htmlbody.Replace("[totalunExcusedabsenses]", studentView.UnexcusedAbsense.ToString());
            htmlbody.Replace("[totalabsenses]", studentView.TotalAbsense.ToString());

            return Content(htmlbody, "text/html");
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "[schoolname]");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,SISNumber,RTTPEnrollmentDt,TransferDt,FirstAttendanceDt,DropDt,BirthDt,Grade,SchoolId,ActiveDate,InactiveDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", student.SchoolId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", student.SchoolId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,SISNumber,RTTPEnrollmentDt,TransferDt,FirstAttendanceDt,DropDt,BirthDt,Grade,SchoolId,ActiveDate,InactiveDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolId = new SelectList(db.Schools, "SchoolId", "SchoolName", student.SchoolId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
