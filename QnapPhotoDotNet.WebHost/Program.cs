using Microsoft.AspNetCore.Mvc;
using RobGray.QnapPhotoDotNet;
using RobGray.QnapPhotoDotNet.QnapApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSerilog(configuration =>
{
    configuration.WriteTo.Console();
});

builder.Services.AddQnapPhotoApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseQnapImageMiddleware();

app.UseHttpsRedirection();

app.MapGet("/list/{page:int}", async (HttpContext context, int page, [FromServices] IQnapApiClient qnapClient) =>
    {
        var request = new ListRequest
        {
            MediaType = MediaType.Photos,
            SortDirection = SortDirection.Descending,
            //StarRating = StarRating.Five,
            PageNumber = page,
            PageSize = 100,
        };

        return await qnapClient.List(request);
    })
    .WithName("ListPhotos")
    .WithTags("QnapPhotos")
    .WithOpenApi();

app.Run();