using Admission.User.API;
using Admission.User.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddPresentationLayer();

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