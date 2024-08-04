using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProjectCleanArchiticture.Data.Entites
{
    public class Teacher
    {
        public Teacher()
        {
            Subjects = new HashSet<Subject>();
            SubordinateTeachers = new HashSet<Teacher>();
            Salary=Decimal.MinValue;
        }

        public int Id { get; set; }
        public string Name { get; set; }
       
        public int Position { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }

        #region Navigation Properties

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public int? ManagedDepartmentId { get; set; }
        public virtual Department ManagedDepartment { get; set; }

        public int? AdvisorId { get; set; }
        public virtual Teacher Advisor { get; set; }
        public virtual ICollection<Teacher> SubordinateTeachers { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        #endregion
    }
}
