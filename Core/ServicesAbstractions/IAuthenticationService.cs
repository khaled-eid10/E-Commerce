using Shared.Authentication;

namespace ServicesAbstractions;
public interface IAuthenticationService
{
    Task<UserResponse> LoginAsync(LoginRequest request);

    Task<UserResponse> RegisterAsync(RegisterRequest request);

    Task<UserResponse> GetUserByEmailAsync(string email);
    Task<bool> CheckEmailAsync(string email);
    Task<AddressDTO> GetAddressAsync(string email);
    Task<AddressDTO> UpdateAddressAsync(AddressDTO address, string email);
}
