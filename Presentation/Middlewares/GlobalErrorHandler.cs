using System.Net;
using System.Text.Json;
using Application.Exceptions;
using AutoMapper;
using FluentValidation;

namespace Presentation.Middlewares;


public class GlobalErrorHandler
{
    private readonly ILogger<GlobalErrorHandler> _logger;
    private readonly RequestDelegate _next;

    private class ExceptionDetails
    {
        public ExceptionDetails()
        {
            Type = Message = string.Empty;
        }
        public string Type { get; set; }
        public string Message { get; set; }
    }
    
    public GlobalErrorHandler(RequestDelegate next, ILogger<GlobalErrorHandler> logger)
    {
        _next = next;
        _logger = logger;
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
            _logger.LogError(error, "message");
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