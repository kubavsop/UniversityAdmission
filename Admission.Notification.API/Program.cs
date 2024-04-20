using System.Reflection;
using Admission.API.Common.Middlewares;
using Admission.API.Common.ServiceInstaller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServices(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
