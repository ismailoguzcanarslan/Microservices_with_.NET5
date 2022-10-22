using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FreeCourse.Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; private set; }

        //Response içerisinde bu parametre olmayacak. Sadece kod içerisinde kullanılıyor.
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccess { get; private set; }
        public List<string> Errors { get; set; }

        //Statik nesnelerle geriye yeni bir nesne dünülüyorsa buna -> Static Factory Method denir.
        public static ResponseDto<T> Success(T data, int statuscode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statuscode, IsSuccess = true };
        }

        public static ResponseDto<T> Success(int statuscode)
        {
            return new ResponseDto<T>{ Data = default(T), StatusCode = statuscode, IsSuccess = true };
        }

        public static ResponseDto<T> Error(List<string> errors, int statuscode)
        {
            return new ResponseDto<T> { Errors = errors, StatusCode = statuscode, IsSuccess = false };
        }

        public static ResponseDto<T> Error(string error, int statuscode)
        {
            return new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = statuscode, IsSuccess = false };
        }
    }
}
