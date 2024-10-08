﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        
        [JsonIgnore]
        public bool IsSuccessfull { get; set; }
        public List<string> Errors { get; set; }

        // Static Factory Method
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T> { Data = data, StatusCode = statusCode,IsSuccessfull=true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> {  Data=default(T),StatusCode = statusCode, IsSuccessfull = true };
        }

        public static Response<T> Fail(List<string> errors,int statusCode)
        {
            return new Response<T>
            {
                IsSuccessfull= false,
                StatusCode = statusCode,
                Errors = errors
            };
        }
        public static Response<T> Fail(string errors, int statusCode)
        {
            return new Response<T>
            {
                IsSuccessfull = false,
                StatusCode = statusCode,
                Errors = new List<string> { errors }
            };
        }

    }
}
