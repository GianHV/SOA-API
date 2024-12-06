﻿using System.Data.SqlClient;
using Common.Base;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SOA_API.Middleware
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is SqlException)
            {
                var response = new ApiResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = new List<string> { context.Exception.Message }
                };
                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
                context.Result = new ContentResult
                {
                    Content = jsonResponse,
                    ContentType = "application/json",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                // Mark the exception as handled
                context.ExceptionHandled = true;
            }
        }

    }
}
