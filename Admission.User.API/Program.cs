using Admission.Infrastructure.Common;
using Admission.User.API;
using Admission.User.Application;
using Admission.User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration)
    .AddJwtAuthentication()
    .AddPresentationLayer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Services.AddAutoMigration();

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