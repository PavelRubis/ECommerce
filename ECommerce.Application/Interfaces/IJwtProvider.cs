using ECommerce.DAL.DTOs;

namespace ECommerce.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(AccountInWebDTO account);
    }
}
