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
        if (principal.UserId is not null) identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, principal.UserId));
        if (principal.UserDetails is not null) identity.AddClaim(new Claim(ClaimTypes.Name, principal.UserDetails));
        if (principal.UserRoles is not null) identity.AddClaims(principal.UserRoles.Select(r => new Claim(ClaimTypes.Role, r)));
        if (principal.Claims is not null)
        {
            foreach (var claim in principal.Claims.Where(c => c.Typ != "" && c.Val != ""))
                identity.AddClaim(new Claim(claim.Typ, claim.Val));
        }

        return new ClaimsPrincipal(identity);
    }
}
