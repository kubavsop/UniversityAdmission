using System.Reflection;
using Admission.API.Common.Configuration;
using Admission.API.Common.Middlewares;
using Admission.User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServices(builder.Configuration, Assembly.GetExecutingAssembly(), typeof(IServiceInstaller).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.Services.AddAutoMigrationAsync();
await app.Services.EnsureRoleCreatedAsync();

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