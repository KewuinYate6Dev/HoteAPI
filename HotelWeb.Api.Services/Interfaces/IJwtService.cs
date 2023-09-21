namespace HotelWeb.Api.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string secretKey, string document, string email, string role, string name);
    }
}
