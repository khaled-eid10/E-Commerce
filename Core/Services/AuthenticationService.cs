using Microsoft.EntityFrameworkCore;

namespace Services;
internal class AuthenticationService(UserManager<ApplicationUser> _userManager, IMapper mapper,
    IOptions<JWTOptions> options)
    : IAuthenticationService
{
    public async Task<bool> CheckEmailAsync(string email)
        => (await _userManager.FindByEmailAsync(email)) != null;
    public async Task<UserResponse> GetUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email)
          ?? throw new UserNotFoundException(email);
        return new(email, user.DisplayName, await CreateTokenAsync(user));
    }
    public async Task<AddressDTO> GetAddressAsync(string email)
    {
        var user = await _userManager.Users.Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Email == email)
            ?? throw new UserNotFoundException(email);
        //if (user.Address is null) throw new AddressNotFoundException(user.UserName!);


        return mapper.Map<AddressDTO>(user.Address);
    }

    public async Task<AddressDTO> UpdateAddressAsync(AddressDTO address, string email)
    {
        var user = await _userManager.Users.Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Email == email)
            ?? throw new UserNotFoundException(email);

        if (user.Address is not null) // Update 
        {
            user.Address.FirstName = address.FirstName;
            user.Address.LastName = address.LastName;
            user.Address.City = address.City;
            user.Address.Street = address.Street;
            user.Address.Country = address.Country;
        }
        else user.Address = mapper.Map<Address>(address);

        await _userManager.UpdateAsync(user);
        return mapper.Map<AddressDTO>(user.Address);
    }
    public async Task<UserResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundException(request.Email);

        var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (isValid) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));

        throw new UnauthorizedException();
    }
    public async Task<UserResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            DisplayName = request.DisplayName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            UserName = request.Email.Split("@")[0],
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded) return new(request.Email, user.DisplayName, await CreateTokenAsync(user));

        var errors = result.Errors.Select(x => x.Description).ToList();

        throw new BadRequestException(errors);
    }



    private async Task<string> CreateTokenAsync(ApplicationUser user)
    {
        var jwt = options.Value;
        var claims = new List<Claim>() // public Claims 
        {
            new(ClaimTypes.Email ,user.Email!),
            new(ClaimTypes.Name ,user.UserName!)
        };

        var roles = await _userManager.GetRolesAsync(user);
        foreach (string role in roles) claims.Add(new(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            expires: DateTime.UtcNow.AddDays(jwt.DurationInDays),
            signingCredentials: signingCredentials,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}

