namespace Dottor.Blazor.SWA.Functions.Utilities;

using Dottor.Blazor.SWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

internal static class ClientPrincipleToClaimsPrinciple
{
    public static ClaimsPrincipal GetClaimsFromClientClaimsPrincipal(ClientPrincipal principal)
    {
        principal.UserRoles =
            principal.UserRoles?.Except(new[] { "anonymous" }, StringComparer.CurrentCultureIgnoreCase) ?? new List<string>();

        if (!principal.UserRoles.Any())
        {
            return new ClaimsPrincipal();
        }

        var identity = new ClaimsIdentity(principal.IdentityProvider);
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
        identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
        identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));

        return new ClaimsPrincipal(identity);
    }
}
