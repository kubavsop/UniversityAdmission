using System.Reflection;
using Admission.API.Common.ServiceInstaller;
using Admission.User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServices(builder.Configuration, Assembly.GetExecutingAssembly(), typeof(IServiceInstaller).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.Services.AddAutoMigrationAsync();
await app.Services.EnsureRoleCreatedAsync();

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