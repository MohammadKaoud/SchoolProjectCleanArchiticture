using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data.Entites
{
   public  class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Subjects=new HashSet<Subject>();
            Teachers=new HashSet<Teacher>();    
          
        }
        [Key]
        public int DepartmentId { get; set; }
        [StringLength(500)]
        public string DepartmentName { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        

        [ForeignKey(nameof(Teacher.ManagedDepartmentId))]
        public int DeparmentManagerId { get; set; }

        [InverseProperty(nameof(Teacher.ManagedDepartment))]

        public virtual  Teacher Manager { get; set; }

    
    }
}
