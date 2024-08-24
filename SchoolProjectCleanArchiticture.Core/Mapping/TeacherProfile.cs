﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Mapping
{
    public partial class TeacherProfile:Profile
    {
        public TeacherProfile()
        {
            MappingAddTeacherCommand();
        }
    }
}
