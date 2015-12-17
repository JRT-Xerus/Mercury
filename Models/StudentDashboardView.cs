using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Models
{
    public class StudentDashboardView
    {
        public string SchoolName { get; set; }
        public string StudentName { get; set; }
        public Nullable<System.DateTime> RTTPEnrollmentDt { get; set; }
        public Nullable<System.DateTime> FirstAttendanceDt { get; set; }
        public Nullable<int> Grade { get; set; }
        public string SISNumber { get; set; }
        public Nullable<System.DateTime> BirthDt { get; set; }
        public string Suspensions { get; set; }
        public Nullable<decimal> ExcusedAbsense { get; set; }
        public Nullable<decimal> UnexcusedAbsense { get; set; }
        public Nullable<decimal> TotalAbsense { get; set; }
        public int Active { get; set; }
    }
}

