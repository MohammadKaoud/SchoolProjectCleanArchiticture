using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Models;
using SchoolProjectCleanArchiticture.Core.Resources;
using SchoolProjectCleanArchiticture.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Features.Students.Commands.Validators
{
    
    public  class AddStudentCommandValidator:AbstractValidator<AddStudentCommand>
    {
        private IStudentService _studentService;
        private IStringLocalizer<SharedResources> _stringLocalizer;
        private IDepartmentService _departmentService;
        public AddStudentCommandValidator(IStudentService studentService,IStringLocalizer<SharedResources>stringLocalizer,IDepartmentService departmentService)
        {
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            _departmentService = departmentService;
            AddValidationRult();
            AddCustomValidation();


        }

        
        public void AddValidationRult()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull].ToString())
            .NotNull().WithMessage(_stringLocalizer[SharedResourcesKeys.NotNull].ToString());
            RuleFor(x => x.Address).NotEmpty().WithMessage("The Same Problem ")
            .NotNull();


        }
        public void AddCustomValidation()
        {
            RuleFor(x => x.Name).MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
                .WithMessage("Used Before fool");
            When(p => p.DepartmentId != null, () =>
            {
                RuleFor(x=>x.DepartmentId).MustAsync(async(key,CancellationToken)=>await _departmentService.IsDepartmentExist(key)==true)
                .WithMessage(_stringLocalizer[SharedResourcesKeys.DepartmentIsnotExist]).ToString();
            });
        }


    }
}
