using CS.API.ViewModels;
using CS.Core.Entities;
using CS.Core.Exceptions;
using CS.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CS.API.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<AppSettings> appSettings, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                UserType = Core.Enums.UserType.Client,
                Name = model.Name
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded && result.Errors.Any())
            {
                if (result.Errors?.FirstOrDefault().Code == "DuplicateUserName")
                {
                    throw new DuplicateException(result.Errors?.FirstOrDefault()?.Description);
                }
            }

            return Ok(new { message = "User created successfully" });
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> SignInAsync(SignInDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            var userToVerify = await _userManager.FindByNameAsync(model.Email);
            if (userToVerify == null) return Unauthorized(model);

            var success = _passwordHasher.VerifyHashedPassword(userToVerify, userToVerify.PasswordHash, model.Password);

            if (success.Equals(PasswordVerificationResult.Success))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, userToVerify.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var userDto = model;
                var token = tokenHandler.CreateToken(tokenDescriptor);
                userDto.Id = userToVerify.Id;
                userDto.Token = tokenHandler.WriteToken(token);
                userDto.Password = userDto.Password.NullPassword();
                userDto.UserType = userToVerify.UserType;
                userDto.Email = userToVerify.Email;
                userDto.UserName = userToVerify.UserName;

                return new OkObjectResult(userDto);
            }

            return Unauthorized(model);
        }

    }
}