using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SchoolProjectCleanArchiticture.Data.Entites
{
    public class Student
    {
        public Student()
        {
            Subjects = new HashSet<Subject>();
            NameAr=string.Empty;
            NameEn=string.Empty;    
        }

        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? DepartmentId { get; set; }
        public string ?NameAr { get; set; }
        public string ?NameEn { get; set; }

        [NotMapped]
        public string LocalizedName
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en" ? NameEn : NameAr;
            }
            set
            {
                if (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en")
                {
                    NameEn = value;
                }
                else
                {
                    NameAr = value;
                }
            }
        }

        public Department Department { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
