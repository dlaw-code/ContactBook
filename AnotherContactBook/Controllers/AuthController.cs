using AnotherContactBook.Data;
using AnotherContactBook.Model;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AnotherContactBook.Controllers
{
    [Route("api/controller")]
    [ApiController]
    
    
    public class AuthController : Controller
    {
        //public static AppUser user = new AppUser();
        //public static AdminRole admin = new AdminRole();

        private readonly ContactDbContext _context;
        private readonly Cloudinary cloudinary;


        private readonly IConfiguration _configuration;


        public AuthController(IConfiguration configuration, ContactDbContext context, Cloudinary cloudinary)
        {
            _context= context;
            _configuration = configuration;
            this.cloudinary = cloudinary;
        }
        [HttpPost("register-regular")]
        public async Task<ActionResult<AppUser>> RegisterRegularUser(UserDto request)
        {
            //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            AppUser user = new AppUser();

            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Role = "Regular";
            //user.PasswordSalt = passwordSalt;
            //user.PasswordHash = passwordHash;
            //user.IsAdmin = false;
            
        

            _context.UserTable.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);

        }

        [HttpPost("register-admin")]
        public async Task<ActionResult<AppUser>> RegisterAdmin(UserDto request)
        {
            //CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            AppUser user = new AppUser();

            user.UserName = request.UserName;
            user.Password = request.Password;
            user.Role = "Admin";
            ////user.PasswordSalt = passwordSalt;
            ////user.PasswordHash= passwordHash;
            //user.IsAdmin = false;



            _context.UserTable.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);

        }

        [HttpPost("login")]

        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var loggedInUser = _context.UserTable.FirstOrDefault(c => c.UserName == request.UserName);

            if (loggedInUser.UserName != request.UserName)
            {
                return BadRequest("User not found,");
            }

            if (loggedInUser.Password != request.Password)
            {
                return BadRequest("Wrong Password.");
            }

            
            string token = CreateToken(loggedInUser);
            
            return Ok(token);
        }


        //Generate token for us
        private string CreateToken(AppUser user)
        {

            List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };



            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            //The SigningCredentials contains the key and the Algorithms
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                //audience: "your-audience",
                //issuer: "your-issuer",
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;


            //return string.Empty;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        } 
        
        //public IActionResult Index()
        //{
        //    [HttpPost("register")]
        //    //return View();
        //}
    }
}
