using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get;private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }
        
        [JsonIgnore]
        public bool IsSuccessfull { get; private set; }
        public List<string> Errors { get; set; }

        // Static Factory Method
        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode,IsSuccessfull=true };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> {  Data=default(T),StatusCode = statusCode, IsSuccessfull = true };
        }

        public static ResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new ResponseDto<T>
            {
                IsSuccessfull= false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
        public static ResponseDto<T> Fail(int statusCode, string errors)
        {
            return new ResponseDto<T>
            {
                IsSuccessfull = false,
                StatusCode = statusCode,
                Errors = new List<string> { errors }
            };
        }

    }
}
