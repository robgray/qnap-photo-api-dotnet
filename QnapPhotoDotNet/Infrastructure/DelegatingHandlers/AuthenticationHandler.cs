using RobGray.QnapPhotoDotNet.Core.QnapApi;
using RobGray.QnapPhotoDotNet.Infrastructure.Extensions;

namespace RobGray.QnapPhotoDotNet.Infrastructure.DelegatingHandlers;

public class AuthenticationHandler(QnapApiOptions qnapApiOptions) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // First get the token.
        // This will auth on every request.
        // Suboptimal but with no Auth Timeout, it'll at least ensure we're authed.
        var loginRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new($"{qnapApiOptions.BaseUrl}/cgi-bin/authLogin.cgi"),
            Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
            {
                new("user", qnapApiOptions.Username),
                new("serviceKey", "1"),
                new("pwd", qnapApiOptions.Password.Base64Encode()),
            }),
        };
        var loginResponse = await base.SendAsync(loginRequest, cancellationToken);
        var login = (await loginResponse.Content.ReadAsStringAsync(cancellationToken)).DeserializeXmLToObject<LoginResponse>();
        
        request.Headers.Add("Cookie", $"{QnapApiClient.AuthCookieName}={login.Sid}");
        
        return await base.SendAsync(request, cancellationToken);
    }
}