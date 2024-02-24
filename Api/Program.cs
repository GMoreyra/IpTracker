using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Data.Interfaces;
using Data.Repositories;
using Mapping.Profiles;
using Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = KeyUtils.APP;
});

builder.Services.AddScoped<IIpTrackerService, IpTrackerService>();
builder.Services.AddScoped<IIpTrackerRepository, IpTrackerRepository>();

var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new IpLocationToIpInfoProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

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
