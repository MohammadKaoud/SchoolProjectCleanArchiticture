using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Command.Models;
using SchoolProjectCleanArchiticture.Core.Features.User.Query.Models;
using SchoolProjectCleanArchiticture.Data.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SchoolProjectCleanArchiticture.Core.Mapping

{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, SUser>();//ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.FullName))
                                               //   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                                               //  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                                               //  .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                                               // .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                                               // .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                                               // .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
                                               //.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password)).ReverseMap();



       

        }
        public void MappingToGetPaginatedResult()
        {
            CreateMap<SUser, GetPaginatedUsers>();
        }
        public void MappingtoGetSingleUserByName() 
            {
            CreateMap<SUser,UserToView>();  

            } 
        public void MappingEditUser()
        {
            CreateMap<EditUserCommand, SUser>().ReverseMap();
        }
     

     
       
   
    }
}
