using Atm.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

app
    .UseRouting()
    .UseSwagger()
    .UseSwaggerUI()
    .UseEndpoints(x => x.MapControllers());

app.Run();