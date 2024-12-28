﻿namespace Ragware.QnapPhotoApi.QnapApi;

using System.Net.Http.Json;
using List;
using ListAlbumPhotos;
using ListAlbums;

public class PhotoStationClient(IHttpClientFactory httpClientFactory) : IPhotoStationClient
{
    public const string HttpClientKey = "QnapPhotoStation";
    public const string AuthCookieName = "NAS_SID";
    
    // Authentication is handled in the AuthenticationHandler,
    // which gets a NAS_SID value and adds it to a cookie.
    // Yes, really basic auth lol. Take that up with Qnap :-)
    
    public async Task<ListResponse?> ListAsync(ListRequest request, CancellationToken cancellationToken)
    {
        return await ListAsync<ListRequest,ListResponse>(request, cancellationToken);
    }

    public async Task<ListAlbumsResponse> ListAlbumsAsync(ListAlbumsRequest request, CancellationToken cancellationToken)
    {
        return await ListAsync<ListAlbumsRequest,ListAlbumsResponse>(request, cancellationToken);
    }

    public async Task<ListAlbumPhotosResponse> ListAlbumPhotosAsync(ListAlbumPhotosRequest request, CancellationToken cancellationToken)
    {
        return await ListAsync<ListAlbumPhotosRequest,ListAlbumPhotosResponse>(request, cancellationToken);
    }

    private async Task<TResult> ListAsync<TRequest,TResult>(TRequest request, CancellationToken cancellationToken)
        where TResult : class, new()

    {
        var httpClient = httpClientFactory.CreateClient(HttpClientKey);

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/photo/api/list.php?{request}");

        requestMessage.Headers.Add("X-Requested-With", "XMLHttpRequest");

        var response = await httpClient.SendAsync(requestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResult>(cancellationToken);
    }

    // To retrieve a particular image we want to call the NAS directly, rather thn
    // download the image and then put it in the response.
    // This abstraction could return an Image, but then it'd need to be copied to a stream
    // and that seems like a waste of resources.
    // Can't call the NAS directly because we need to authenticate with the NAS
    // and any raw url we provide won't have the required cookie.

}