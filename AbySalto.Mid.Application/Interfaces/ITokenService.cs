
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(ApplicationUser applicationUser);
    }
}
