using Application.Initialization;
using Application.Options;
using Data.Initialization;
var builder = WebApplication.CreateBuilder(args);

//TODO: Improve this configuration
var positionOptions = builder.Configuration.GetSection(ExternalServiceOptions.ExternalServiceSectionName)
                                           .Get<ExternalServiceOptions>();

builder.Services.RegisterApplication(positionOptions);
builder.Services.RegisterData(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
