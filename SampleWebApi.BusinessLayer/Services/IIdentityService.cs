using SampleWebApi.Shared.Models;
using System.Threading.Tasks;

namespace SampleWebApi.BusinessLayer.Services
{
    public interface IIdentityService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}