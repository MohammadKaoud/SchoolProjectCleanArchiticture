using SchoolProjectCleanArchiticture.Data.Entites;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Implementation
{
    public class LocalizationService : ILocalizationService

    {
        public bool AreNamesEqual(Student student1, Student student2)
        {
          return   GetLocalizedName(student1) == GetLocalizedName(student2);
        }

        public string GetLocalizedName(Student student)
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "en" ? student.NameEn : student.NameAr;
        }
    }
}
