using System.Net;
using AutoMapper;
using BusinessLayer.Exceptions;
using FluentValidation;
using System.Text.Json;


namespace EventHub.MiddlewareHandlers;

public class GlobalErrorHandler
{

    private readonly RequestDelegate _next;

    public GlobalErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var exceptionDetails = new ExceptionDetails()
            {
                Message = error.Message, // сообщение исключения
                Type = error.GetType().Name, // название исключения
            };
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                EmptyBodyException => (int)HttpStatusCode.BadRequest,
                EntityAlreadyExistException => (int)HttpStatusCode.Conflict,
                InvalidDataTypeException => (int)HttpStatusCode.BadRequest,
                ValidationException => (int)HttpStatusCode.BadRequest,
                AutoMapperMappingException => (int)HttpStatusCode.BadRequest,
                EntityNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };
            var result = JsonSerializer.Serialize(exceptionDetails);
            await response.WriteAsync(result);
        }
    }
    
}