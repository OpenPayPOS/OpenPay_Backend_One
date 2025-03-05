using Microsoft.OpenApi.Models;
using OpenPay.Services.Configuration;
using OpenPay.Data.Configuration;
using OpenPay.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
})
    .AddApplicationPart(typeof(IWebAssemblyLoader).Assembly);

builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("frontend", policy =>
    {
        policy.AllowAnyHeader().AllowCredentials().AllowAnyMethod().WithOrigins(builder.Configuration["FrontendUrl"]);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Api",
        Version = "v1",
        Description = "An API to support my OpenPay application",
        Contact = new OpenApiContact
        {
            Name = "Martijn Koppejan",
            Email = "martijn.koppejan1@gmail.com"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1");
    });
}


app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("frontend");

app.Run();

// INFO: Task list legend:
// INFO: INFO
// INFO: TODO
// INFO: HACK
// INFO: UNDONE