using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaParserService.Common.ResponseType
{
    public class ResponseDetails
    {
        public ResponseStatus ResponseStatus { get; set; }
        public object ResponseData { get; set; }
        public string Error { get; set; }
        public string ResponseMessage { get; set; }
    }

    public enum ResponseStatus
    {
        Success =1,
        Notfound =2,
        Error =3,
        BadRequest =4
    }
}
