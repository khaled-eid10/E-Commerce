namespace Shared.Authentication;
public record RegisterRequest([EmailAddress] string Email, string DisplayName, string Password,
    string? UserName = "MMM", string? PhoneNumber = "");