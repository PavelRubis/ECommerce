using ECommerce.Application.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(string accId);
    }
}
