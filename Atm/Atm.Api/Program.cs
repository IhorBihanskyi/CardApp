using Atm.Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app
    .UseRouting()
    .UseSwagger()
    .UseSwaggerUI()
    .UseEndpoints(x => x.MapControllers());

app.UseMiddleware<ErrorHandlerMiddleware>();

app.Run();