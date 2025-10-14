namespace PcAsCloud.BL.Options;

public record JWTOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
}