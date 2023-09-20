namespace Dottor.Blazor.SWA.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ClientPrincipal
    {
        public string? IdentityProvider { get; set; }
        public string? UserId { get; set; }
        public string? UserDetails { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<string>? UserRoles { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<CustomClaim>? Claims { get; set; }
    }
}
