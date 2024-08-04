using Microsoft.Extensions.Localization;
using SchoolProjectCleanArchiticture.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Base
{
    public  class ResponseHundler
    {
        private readonly IStringLocalizer<SharedResources>_stringLocalizer;
        public ResponseHundler(IStringLocalizer<SharedResources>stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }


        public ResponseM<T> Deleted<T>()
        {
            return new ResponseM<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                message = _stringLocalizer[SharedResourcesKeys.Deleted]
            };
        }
        public ResponseM<T> Success<T>(T entity, object Meta = null)
        {
            return new ResponseM<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                message = _stringLocalizer[SharedResourcesKeys.Success],
                Meta = Meta
            };
        }
        public ResponseM<T> Unauthorized<T>()
        {
            return new ResponseM<T>()
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = true,
                message = _stringLocalizer[SharedResourcesKeys.UnAuthorized]
            };
        }
        public ResponseM<T> BadRequest<T>(string Message = null)
        {
            return new ResponseM<T>()
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                message = Message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : Message
            };
        }

        public ResponseM<T> NotFound<T>(string message = null)
        {
            return new ResponseM<T>()
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                message = message == null ? _stringLocalizer[SharedResourcesKeys.NotFound] : message
            };
        }

        public ResponseM<T> Created<T>(T entity, object Meta = null)
        {
            return new ResponseM<T>()
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = true,
               message= _stringLocalizer[SharedResourcesKeys.Created],
                Meta = Meta
            };
        }
    }

}
