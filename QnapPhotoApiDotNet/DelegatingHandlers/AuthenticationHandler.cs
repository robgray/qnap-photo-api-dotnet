namespace RobGray.QnapPhotoApiDotNet.DelegatingHandlers;

using Extensions;
using Microsoft.Extensions.Caching.Memory;
using QnapApi;
using QnapApi.Login;

public class AuthenticationHandler(QnapApiOptions qnapApiOptions, ILogger logger, IMemoryCache memoryCache, TimeProvider timeProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
            // We're taking a guess that authTimeout is good for an hour. 
            // If not, can make smaller.
		
            var loginResponse = await memoryCache.GetOrCreateAsync<LoginResponse?>(
                PhotoStationClient.AuthCookieName,
                async cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(timeProvider.GetLocalNow().AddHours(qnapApiOptions.AuthTimeoutInHours));
				
                    var loginRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri($"{qnapApiOptions.BaseUrl}/cgi-bin/authLogin.cgi"),
                        Content = new FormUrlEncodedContent([
                            new("user", qnapApiOptions.Username),
                            //new("serviceKey", "1"),
                            new("pwd", qnapApiOptions.Password.Base64Encode())
                        ]),
                    };
                    var loginResponse = await base.SendAsync(loginRequest, cancellationToken);
                    var login = (await loginResponse.Content.ReadAsStringAsync(cancellationToken)).DeserializeXmLToObject<LoginResponse>();
			
                    if (login is null)
                    {
                        logger.Warning("Could not convert Qnap Login Response to a known format. Authentication cookie will not be added to requests");
                        return null;
                    }
				
                    if (!login.AuthPassed)
                    {
                        logger.Warning("Login to Qnap Nas was not successful. Check Username and Password setting in configuration");
                        return null;
                    }
				
                    return login;
                });
		
        if (loginResponse is not null)
        {
            request.Headers.Add("Cookie", $"{PhotoStationClient.AuthCookieName}={loginResponse.Sid}");
        }

        return await base.SendAsync(request, cancellationToken);
    }
}