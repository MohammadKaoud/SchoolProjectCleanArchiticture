using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolProjectCleanArchiticture.Core.Mapping

{
    public partial class RoleProfile
    {
        public void MappingGetListOfRole()
        {
            CreateMap<IdentityRole, GetRoleResult>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

         
        }
    }
}
