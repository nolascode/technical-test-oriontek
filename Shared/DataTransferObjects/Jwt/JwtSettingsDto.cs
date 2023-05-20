using System.Text.Json.Serialization;
namespace Shared.DataTransferObjects.Jwt
{
    public record JwtSettingsDto
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        [JsonPropertyName("accessTokenExpiration")]
        public string AccessTokenExpiration { get; set; }

        [JsonPropertyName("refreshTokenExpiration")]
        public string RefreshTokenExpiration { get; set; }
    }
}