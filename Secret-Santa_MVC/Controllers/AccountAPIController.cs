//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Secret_Santa_MVC.Data.Entities;
//using Secret_Santa_MVC.Data;
//using Secret_Santa_MVC.Models.Identity;
//using Microsoft.EntityFrameworkCore;
//using Secret_Santa_MVC.Extensions;
//using System.IdentityModel.Tokens.Jwt;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Rewrite;
//using Microsoft.Extensions.DependencyInjection;
//using System;

//namespace Secret_Santa_MVC.Controllers
//{
//    public class AccountAPIController : Controller
//    {
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SantaContext _context;
//        private readonly IConfiguration _configuration;
//      //  private readonly IServiceProvider _serviceProvider;
//        public AccountAPIController( SantaContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration)// IServiceProvider serviceProvider)
//        {
//            _userManager = userManager;
//            _context = context;
//            _configuration = configuration;
//          //  _serviceProvider = serviceProvider;

//        }
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }
//        [HttpGet]
//        public IActionResult Authenticate()
//        {
//            return View();
//        }
//        [HttpGet]
//        public IActionResult Register()
//        {
//            return View();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Authenticate( AuthRequest request)
//        {
             
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            //var user = request;
//            //var result = await _appUserService.Login(request);
//            if (//result.Succeeded)
//                true)
           

//            {
//                var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
//                //if (user is null)
//                //    return Unauthorized();

//                var roleIds = await _context.UserRoles
//                    .Where(r => r.UserId == user.Id)
//                    .Select(x => x.RoleId).ToListAsync();
//                var roles = _context.Roles.Where(x => roleIds.Contains(x.Id)).ToList();

//                //var accesToken = _tokenService.CreateToken(user, roles);
//                user.RefreshToken = _configuration.GenerateRefreshToken();
//                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

//                await _context.SaveChangesAsync();
//                //return Ok(new AuthResponse
//                //{
//                //    Username = user.UserName!,
//                //    Email = user.Email!,
//                //    Token = accesToken,
//                //    RefreshToken = user.RefreshToken
//                //});
//                return RedirectToAction("Test", "Account");
//            }
//            else { return View((object)"Lox"); }
//        }


//        [HttpPost]
//        public async Task<ActionResult<AuthResponse>> Register( RegisterRequest request)
//        {
//            if (!ModelState.IsValid) return BadRequest(request);

//            var user = new ApplicationUser
//            {
//                FullName = request.FullName,
//                Email = request.Email,
//                UserName = request.Email,
//                DateRegister = DateTime.UtcNow,
//            };
//            var result = await _userManager.CreateAsync(user,request.Password);

//            foreach(var error in result.Errors) 
//            {
//                ModelState.AddModelError(string.Empty, error.Description);
//            }
//            if(!result.Succeeded) return BadRequest(request);


//            var findUser = await _context.Users.FirstOrDefaultAsync(x=> x.Email == request.Email);
//            if (findUser == null) throw new Exception($"User {request.Email} not found");

//            await _userManager.AddToRoleAsync(findUser, RoleConsts.Member);

//            return RedirectToAction("Index","Account");
//        }

//        [HttpPost]
//        public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
//        {
//            if(tokenModel is null)
//            {
//                return BadRequest("Invalid client request");
//            }

//            var accesTiken = tokenModel.AccesToken;
//            var refreshToken = tokenModel.RefreshToken;
//            var principal = _configuration.GetPrincipalFromExpiredToken(accesTiken);

//            if(principal == null)
//            {
//                return BadRequest("Invalid access token or refresh token");
//            }

//            var username = principal.Identity!.Name;
//            var user = await _userManager.FindByEmailAsync(username!);

//            if(user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
//            {
//                return BadRequest("Invalid access token or refresh token");
//            }

//            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
//            var newRefreshToken = _configuration.GenerateRefreshToken();

//            user.RefreshToken = newRefreshToken;
//            await _userManager.UpdateAsync(user);

//            return new ObjectResult(new
//            {
//                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
//                refreshToken = newRefreshToken
//            });
//        }
//        [Authorize]
//        [HttpPost]
//        [Route("revoke/{username}")]
//        public async Task<IActionResult> Revoke(string username)
//        {
//            var user = await _userManager.FindByNameAsync(username);
//            if(user == null) { return BadRequest("Invalid username"); }

//            user.RefreshToken = null;
//            await _userManager.UpdateAsync(user);
//            return RedirectToAction("Index", "Account");
//        }

//        [Authorize]
//        [HttpPost]
//        [Route("revoke-all")]
//        public async Task<IActionResult> RevokeAll()
//        {
//            var users = _userManager.Users.ToList();
//            foreach (var user in users)
//            {
//                user.RefreshToken = null;
//                await _userManager.UpdateAsync(user);
//            }
//            return RedirectToAction("Index","Account");
//        }

//    }
//}
////with login 
////var managedUser = await _userManager.FindByEmailAsync(request.Email);
////if (managedUser == null)
////{
////    return BadRequest("Bad credentials");
////}
////var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

////if(!isPasswordValid)
////{
////    return BadRequest("Bad Password");
////}