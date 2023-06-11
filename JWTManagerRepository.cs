using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt.Model
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public JWTManagerRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
         
            _config = config;
        }
        public async Task<LoginResponseModel> Login(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return new LoginResponseModel()
                {
                    IsSuccess = false
                };
            }
            var PasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!PasswordValid)
            {
                return new LoginResponseModel()
                {
                    IsSuccess = false
                };
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var tokenHendeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._config.GetValue<string>("JWT:Key"));

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(authClaims),
                Expires = System.DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHendeler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHendeler.WriteToken(token);
            return new LoginResponseModel()
            {
                IsSuccess = true,
                Token = encryptedToken,
                Username = user.UserName,
                Role = userRoles[0]
            };
        }

        public async Task<LoginResponseModel> SignUp(Register model)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            var response = new LoginResponseModel();

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                response = new LoginResponseModel();
                response.IsSuccess = true;
                return response;
            }
            response.IsSuccess = false;
            return response;
        }

    }





}
