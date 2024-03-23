namespace LeaveManagement.Identity.Services;

using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using LeaveManagement.Identity.Models;
using LeaveManagement.Application.Models.Identity;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Exceptions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly JwtSettings jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await this.userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new NotFoundException($"User with {request.Email} not found.", request.Email);
        }

        var result = await this.signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            throw new BadRequestException($"Credentials for '{request.Email} aren't valid.'");
        }

        var jwtSecurityToken = await this.GenerateToken(user);

        return new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var result = await this.userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await this.userManager.AddToRoleAsync(user, "Employee");

            return new RegistrationResponse()
            {
                UserId = user.Id
            };
        }
        else
        {
            var str = new StringBuilder();

            foreach (var err in result.Errors)
            {
                str.AppendFormat("{0}\n", err.Description);
            }

            throw new BadRequestException($"{str}");
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await this.userManager.GetClaimsAsync(user);
        var roles = await this.userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Key));

        var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: this.jwtSettings.Issuer,
            audience: this.jwtSettings.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(this.jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
    }
}
