﻿namespace RobGray.QnapPhotoDotNet.QnapApi;

using System.Net.Http.Json;

public interface IQnapApiClient
{
    public Task<ListResponse?> List(ListRequest request);
}

public class QnapApiClient(IHttpClientFactory httpClientFactory) : IQnapApiClient
{
    public const string HttpClientKey = "QnapClient";
    public const string AuthCookieName = "NAS_SID";
    
    // Authentication is handled in the AuthenticationHandler,
    // which gets a NAS_SID value and adds it to a cookie.
    // Yes, really basic auth lol. Take that up with Qnap :-)
    
    public async Task<ListResponse?> List(ListRequest request)
    {
        var httpClient = httpClientFactory.CreateClient(HttpClientKey);

        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/photo/api/list.php?{request}");
        
        requestMessage.Headers.Add("X-Requested-With", "XMLHttpRequest");
            
        var response = await httpClient.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<ListResponse>();
    }
    
    // To retrieve a particular image we want to call the NAS directly, rather thn
    // download the image and then put it in the response.
    // This abstraction could return an Image, but then it'd need to be copied to a stream
    // and that seems like a waste of resources.
    // Can't call the NAS directly because we need to authenticate with the NAS
    // and any raw url we provide won't have the required cookie.
    
}