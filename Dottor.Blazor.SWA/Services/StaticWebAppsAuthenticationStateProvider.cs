namespace Dottor.Blazor.SWA.Services
{
    using Dottor.Blazor.SWA.Models;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using System.Net.Http.Json;
    using System.Security.Claims;

    public class StaticWebAppsAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;


        public StaticWebAppsAuthenticationStateProvider(IConfiguration config, IWebAssemblyHostEnvironment environment, HttpClient httpClient)
        {
            _configuration = config;
            _httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authDataUrl = _configuration.GetValue<string>("StaticWebAppsAuthentication:AuthenticationDataUrl", "/.auth/me");
                var data = await _httpClient.GetFromJsonAsync<AuthenticationData>(authDataUrl);

                var principal = data!.ClientPrincipal;
                if (principal is not null)
                {
                    if (principal.UserRoles is not null)
                        principal.UserRoles = principal.UserRoles.Except(new string[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase);
                    
                    var identity = new ClaimsIdentity(principal.IdentityProvider);
                    if (principal.UserId is not null) identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
                    if (principal.UserDetails is not null) identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
                    if (principal.UserRoles is not null) identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
                    if (principal.Claims is not null)
                    {
                        foreach (var claim in principal.Claims.Where(c => c.Typ != "" && c.Val != ""))
                            identity.AddClaim(new Claim(claim.Typ, claim.Val));
                    }

                    return new AuthenticationState(new ClaimsPrincipal(identity));

                }

                return new AuthenticationState(new ClaimsPrincipal());
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
    }
}
