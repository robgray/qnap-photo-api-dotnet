using Microsoft.AspNetCore.Mvc;
using Ragware.QnapPhotoApi;
using Ragware.QnapPhotoApi.QnapApi;
using Ragware.QnapPhotoApi.QnapApi.List;
using Ragware.QnapPhotoApi.QnapApi.ListAlbumPhotos;
using Ragware.QnapPhotoApi.QnapApi.ListAlbums;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.CustomSchemaIds(x => x.FullName);
});
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

app.MapGet("/list/{page:int}", async (int page, [FromServices] IPhotoStationClient qnapClient, CancellationToken cancellationToken) =>
    {
        var request = new ListRequest
        {
            MediaType = MediaType.Photos,
            SortDirection = SortDirection.Descending,
            PageNumber = page,
            PageSize = 100,
        };

        return await qnapClient.ListAsync(request, cancellationToken);
    })
    .WithName("ListPhotos")
    .WithTags("QnapPhotos")
    .WithOpenApi();

app.MapGet("/list-album-photos/{albumId}/{page:int}", async (string albumId, int page, [FromServices] IPhotoStationClient qnapClient, CancellationToken cancellationToken) =>
    {
        var request = new ListAlbumPhotosRequest()
        {
            AlbumId = albumId,
            SortDirection = SortDirection.Descending,
            Sort = Sort.DateAdded,
            PageNumber = page,
            PageSize = 100,
        };

        return await qnapClient.ListAlbumPhotosAsync(request, cancellationToken);
    })
    .WithName("ListAlbumPhotos")
    .WithTags("QnapPhotos")
    .WithOpenApi();

app.MapGet("/list-album/{page:int}", async (int page, [FromServices] IPhotoStationClient qnapClient, CancellationToken cancellationToken) =>
    {
        var request = new ListAlbumsRequest()
        {
            SortDirection = SortDirection.Descending,
            AlbumType = AlbumType.All,
            Sort = AlbumSort.Title,
            PageNumber = page,
            PageSize = 100,
        };

        return await qnapClient.ListAlbumsAsync(request, cancellationToken);
    })
    .WithName("ListAlbums")
    .WithTags("QnapPhotos")
    .WithOpenApi();



app.Run();