using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingAPI.API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public ApiResponse(int StatusCode, string Message = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message  ?? GetCustomMessage(StatusCode);
        }

        private string GetCustomMessage(int StatusCode)
        {
            return StatusCode switch
            {
                400 => "Bad request",
                401 => "Not Authorised",
                404 => "Resource not found",
                500 => "Internal server error",
                _ => null,
            };
        }
    }
}
