using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Application.Common.Models;
public class ResponseResult<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static ResponseResult<T> SuccessResult(T data, string? message = null)
        => new() { Success = true, Data = data, Message = message };

    public static ResponseResult<T> Failure(string message)
        => new() { Success = false, Data = default, Message = message };
}
