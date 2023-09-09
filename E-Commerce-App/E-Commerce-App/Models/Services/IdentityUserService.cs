using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_App.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly JWTTokenService _jwtTokenService;
        public IdentityUserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInMngr, JWTTokenService jwtTokenService )
        {
            _userManager = userManager;
            _signInManager = SignInMngr;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
                PhoneNumber = registerUserDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, registerUserDTO.Roles);

                return new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await _jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(60)),
                    Roles = await _userManager.GetRolesAsync(user)
                };

            }
            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(registerUserDTO.Password) :
                error.Code.Contains("UserName") ? nameof(registerUserDTO.UserName) :
                     error.Code.Contains("Email") ? nameof(registerUserDTO.Email) :
                     "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }


        public async Task<UserDTO> Authenticate(string userName, string password)
        {
           var result = await _signInManager.PasswordSignInAsync(userName, password,true,false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);
                return new UserDTO()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await _jwtTokenService.GetToken(user, System.TimeSpan.FromMinutes(60)),
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }
            return null;
        }

        public async Task<List<ApplicationUser>> getAll()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDTO
            {
                UserName = user.UserName
            };
        }

   


    }
}
