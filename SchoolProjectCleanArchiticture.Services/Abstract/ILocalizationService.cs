using SchoolProjectCleanArchiticture.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Services.Abstract
{
    public  interface ILocalizationService
    {
        string GetLocalizedName(Student student);
        bool AreNamesEqual(Student student1, Student student2);
    }
}
