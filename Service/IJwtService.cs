using Domain.DTO;

namespace Service
{
    public interface IJwtService
    {
        public string GenerateToken(UserLoginDto user);
    }
}