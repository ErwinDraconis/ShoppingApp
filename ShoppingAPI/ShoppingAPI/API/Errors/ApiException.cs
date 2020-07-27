using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAPI.API.Errors
{
    public class ApiException : ApiResponse
    {
        public string Details { get; set; }
        public ApiException(int StatusCode, string Message = null, string Details = null) 
            : base(StatusCode, Message)
        {
            this.Details = Details;
        }

    }
}
