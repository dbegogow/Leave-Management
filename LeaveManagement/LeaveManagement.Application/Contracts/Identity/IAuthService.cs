namespace LeaveManagement.Application.Contracts.Identity;

using LeaveManagement.Application.Models.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);

    Task<RegistrationResponse> Register(RegistrationRequest request);
}
