using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Entites
{
    public  class Subject
    {
        public Subject()
        {
          Students = new HashSet<Student>();
            Departments= new HashSet<Department>();
            Teahers=new HashSet<Teacher>();
        }
        [Key]
        public int SubjectID { get; set; }
        [StringLength(500)]
        public string SubjectName { get; set; }
        public int TimeOfPeriod { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<Teacher> Teahers { get; set; }
             



    }
}
