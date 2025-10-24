using _3_�������������_�_DTO;
using _3_�������������_�_DTO.Valitador;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;
using System;
using System.Diagnostics;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Book Api",
        Version = "v1",
        Description = "Api ��� ���������� �������"
    });


var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

if (File.Exists(xmlPath))
{
    c.IncludeXmlComments(xmlPath);
}
});

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IValidator<CreateBookDTO>, CreateBookDTOValidator>();


var app = builder.Build();


app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandler = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
        var exeption = exceptionHandler?.Error;
        var details = new
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            deatail = "An unexpected error",
            Instance = context.Request.Path,
            TraceId = context.TraceIdentifier
        };
        Console.WriteLine($"Error {exeption?.Message}");
        Console.WriteLine($"StackTrace {exeption?.StackTrace}");

        await context.Response.WriteAsJsonAsync(details);
    });
});

app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    var stopwatch = Stopwatch.StartNew();
    Console.WriteLine($"Start {context.Request.Method}{context.Request.Path} time - {startTime}");

    await next();
    stopwatch.Stop();
    Console.WriteLine($"done {context.Request.Method}{context.Request.Path}, status {context.Response.StatusCode} time - {stopwatch.ToString()}");
}
);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
