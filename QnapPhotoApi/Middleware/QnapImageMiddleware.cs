namespace Ragware.QnapPhotoApi.Middleware;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using QnapApi;

/// <summary>
/// Acts as a ReverseProxy to the nas to get the particular image
/// as well as tidies up the URL and handles authentication (cookie).
/// </summary>
/// <remarks>
///	Better to use this method than try to go through the full REST endpoint.
/// We could just hit the NAS image endpoint directly, but we need
/// to auth with the NAS and this ensures we are authenticated.
/// </remarks>
public class QnapImageMiddleware(RequestDelegate nextMiddleware, IHttpClientFactory factory)
{
	public async Task Invoke(HttpContext context, IOptionsMonitor<QnapApiOptions> qnapApiOptions)
	{
		var targetUri = BuildImageUri(context.Request, qnapApiOptions.CurrentValue.BaseUrl);

		if (targetUri is not null)
		{
			var targetRequestMessage = CreateTargetMessage(context, targetUri);

			var httpClient = factory.CreateClient(PhotoStationClient.HttpClientKey);
			using var responseMessage = await httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
        
			context.Response.StatusCode = (int)responseMessage.StatusCode;
			CopyFromTargetResponseHeaders(context, responseMessage);
			await responseMessage.Content.CopyToAsync(context.Response.Body);
        
			return;
		}
      
		await nextMiddleware(context);
	}

	private HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
	{
		var requestMessage = new HttpRequestMessage();
		CopyFromOriginalRequestContentAndHeaders(context, requestMessage);
		
		requestMessage.RequestUri = targetUri;
		requestMessage.Headers.Host = targetUri.Host;
		requestMessage.Method = GetMethod(context.Request.Method);

		return requestMessage;
	}

	private void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
	{
		var requestMethod = context.Request.Method;

		if (!HttpMethods.IsGet(requestMethod) &&
		    !HttpMethods.IsHead(requestMethod) &&
		    !HttpMethods.IsDelete(requestMethod) &&
		    !HttpMethods.IsTrace(requestMethod))
		{
			var streamContent = new StreamContent(context.Request.Body);
			requestMessage.Content = streamContent;
		}

		foreach (var header in context.Request.Headers)
		{
			requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
		}
	}
	
	private void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
	{
		foreach (var header in responseMessage.Headers)
		{
			context.Response.Headers[header.Key] = header.Value.ToArray();
		}

		foreach (var header in responseMessage.Content.Headers)
		{
			context.Response.Headers[header.Key] = header.Value.ToArray();
		}
		context.Response.Headers.Remove("transfer-encoding");
	}

	private static HttpMethod GetMethod(string method)
	{
		if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
		if (HttpMethods.IsGet(method)) return HttpMethod.Get;
		if (HttpMethods.IsHead(method)) return HttpMethod.Head;
		if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
		if (HttpMethods.IsPost(method)) return HttpMethod.Post;
		if (HttpMethods.IsPut(method)) return HttpMethod.Put;
		if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
		return new HttpMethod(method);
	}

	private Uri? BuildImageUri(HttpRequest request, string baseUrl)
	{ 
		Uri? imageUri = null;

		if (request.Path.StartsWithSegments("/image", out var remainingPath))
		{
			var imageId = remainingPath.Value!.Remove(0, 1); // The first character is a "/"
			imageUri = new Uri($"{baseUrl}/photo/api/thumb.php?s=0&m=display&f={imageId}");
		}
		
		if (request.Path.StartsWithSegments("/thumb", out remainingPath))
		{
			var imageId = remainingPath.Value!.Remove(0, 1); // The first character is a "/"
			imageUri = new Uri($"{baseUrl}/photo/api/thumb.php?s=1&m=display&f={imageId}");
		}

		return imageUri;
	}
}