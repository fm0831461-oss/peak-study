using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PeekStudy.API.DTOs.UserDTOs;
using PeekStudy.API.IRepo;
using PeekStudy.API.Models;
using PeekStudy.API.Services.IServices;
namespace PeekStudy.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo userRepo;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public UserService(IUserRepo userRepo, IMapper mapper, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.userRepo = userRepo;
            this.mapper = mapper;
        }
        private string GenerateJwtToken(User user)
        {
            // البيانات التي سيتم تخزينها داخل الـ Token
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Email, user.Email)
    };

            // قراءة الـ Secret Key من appsettings.json
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
            );

            // إنشاء بيانات التوقيع
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            // إنشاء الـ Token
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(configuration["Jwt:DurationInMinutes"])
                ),
                signingCredentials: credentials
            );

            // تحويله إلى String وإرجاعه
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



       


        public async Task<bool> DeleteUserAsync(int id)
        {
            var get = await userRepo.GetByIdAsync(id);
            if (get == null)
            {
                return false;
            }
            await userRepo.DeleteAsync(id);
            await userRepo.SaveAsync();
            return true;
        }





        public async Task<UserDTO?> GetProfileAsync(int id)
        {
          var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
           return mapper.Map<UserDTO>(user);
         
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDTO dto)
        {
            var user = await userRepo.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return null;
            }
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                return null;
            }
            var token = GenerateJwtToken(user);
            return new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<LoginResponseDto?> RegisterAsync(RegisterUserDto dto)
        {
            bool emailExist = await userRepo.EmailExistsAsync(dto.Email);
            if (emailExist)
            {
                return null;
            }
            var user = mapper.Map<User>(dto);
            //auto mapper بيعمل نفس ده 
            //var user = new User();

            //user.FullName = dto.FullName;
            //user.Email = dto.Email;
            //user.PasswordHash = dto.Password;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await userRepo.AddAsync(user);
            await userRepo.SaveAsync();
            var token = GenerateJwtToken(user);
            //var response = new LoginResponseDto
            //{
            //    UserId = user.UserId,
            //    FullName = user.FullName,
            //    Email = user.Email,
            //    Token = token
            //};
            //return response;

            return new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<UserDTO?> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await userRepo.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            user.FullName = dto.FullName;
            user.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }
            await userRepo.UpdateAsync(user);
            await userRepo.SaveAsync();
            return mapper.Map<UserDTO>(user);

        }

    }
}

