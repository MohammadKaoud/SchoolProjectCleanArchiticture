using FluentValidation;
using FluentValidation.Internal;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>>validator,IStringLocalizer<SharedResources>stringLocalizer)
        {
            _validators = validator;
            _stringlocalizer = stringLocalizer;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            if (_validators.Any())
            {
                var _context=new ValidationContext<TRequest>(request);
                var validationResult = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(_context, cancellationToken)));
                var failures=validationResult.SelectMany(x=>x.Errors).Where(x=>x!=null).ToList();
                if (failures.Count!=0)
                {
                    var message= failures.Select(x => _stringlocalizer[x.PropertyName] +" : " + _stringlocalizer[x.ErrorMessage
                        ]).FirstOrDefault();
                    throw new FluentValidation.ValidationException(message);
                }

            }
            return await next();
        }
    }
}
