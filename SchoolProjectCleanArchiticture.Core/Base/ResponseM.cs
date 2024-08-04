using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Base
{
    public  class ResponseM<T>
    {
        public List<string>Errors {  get; set; }
        public T Data { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; }
        public string message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public ResponseM()
        {

        }
        public ResponseM(T data, string message = null)
        {
            Succeeded = true;
            message = message;
            Data = data;
        }
        public ResponseM(string message)
        {
            Succeeded = false;
            message = message;
        }
        public ResponseM(string message, bool succeeded)
        {
            Succeeded = succeeded;
            message = message;
        }


    }
}

