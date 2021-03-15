using System;

namespace API.Error
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);

        }       

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"A bad request. You have made.",
                401=>"Authorized. You are not.",
                404=>"Resource Found, It was not.",
                500=>"Errors are the path to dark side. Errors Lead to anger. Anger Leads to Hate. Hate Leads to carrier change.",
                _=>null
            };
        }
    }
}