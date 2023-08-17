using Shortener.Configuration;
using Shortener.Endpoints;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var connectionString = builder.Configuration.GetConnectionString("SvcDbContext");
    ShortenerBootstrapper.Configure(builder.Services,builder.Configuration,connectionString);
}
var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapPost("/shorten", ShortenerEndpoints.ShortenEndpoint)
        .WithOpenApi()
        .WithName("Shortener");

    app.MapGet("{shortCode}", RedirectEndpoints.RedirectEndpoint)
        .WithOpenApi()
        .WithName("Redirect")
        .AllowAnonymous();
}
app.Run();
