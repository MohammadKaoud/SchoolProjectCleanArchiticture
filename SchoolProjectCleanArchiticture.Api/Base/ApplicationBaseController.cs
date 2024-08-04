﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProjectCleanArchiticture.Core.Base;
using System.Net;

namespace SchoolProjectCleanArchiticture.Api.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationBaseController : ControllerBase
    {
        private  IMediator mediatorInstance;
        protected IMediator _mediator =>mediatorInstance??=HttpContext.RequestServices.GetService<IMediator>();
        public ObjectResult NewResult<T>(ResponseM<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }

        }
    }
}
