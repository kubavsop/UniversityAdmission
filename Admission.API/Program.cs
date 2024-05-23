using System.Reflection;
using Admission.API.Common.Middlewares;
using Admission.API.Common.ServiceInstaller;
using Admission.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServices(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.Services.AddAutoMigrationAsync();

app.UseExceptionHandlingMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();