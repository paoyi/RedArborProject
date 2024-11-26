namespace Redarbor.Infraestructure.Security.TokenGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid id, string name);
    }
}