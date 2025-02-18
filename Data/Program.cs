using OpenPay.Data.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataServices(builder.Configuration);

var app = builder.Build();

app.Run();