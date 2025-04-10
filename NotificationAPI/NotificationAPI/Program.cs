using Microsoft.AspNetCore.Builder;
using NotificationAPI.Services.Abstractions;
using NotificationAPI.Services.Decorators;
using NotificationAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<INotifier, EmailNotifier>();
builder.Services.Decorate<INotifier, LoggingNotifier>();
builder.Services.Decorate<INotifier, ValidationNotifier>();
builder.Services.Decorate<INotifier, AuditNotifier>(); // El ultimo se ejecuta primero.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API Patrones de Diseño Estructural");
    });
}
else
{
    app.UseHttpsRedirection();

    app.UseAuthorization();
}

app.Lifetime.ApplicationStarted.Register(() =>
{
    var notifier = app.Services.GetRequiredService<INotifier>();
    notifier.Notify("mensaje de prueba");
});


app.MapControllers();

app.Run();