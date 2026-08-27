 using PeekStudy.API.DTOs.UserDTOs;

namespace PeekStudy.API.Services.IServices
{
    public interface IUserService
    {
        Task<LoginResponseDto?> RegisterAsync(RegisterUserDto dto);
        Task<LoginResponseDto?> LoginAsync(LoginDTO dto);
        Task<UserDTO?> GetProfileAsync(int id);
        Task<UserDTO?> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);

    }
}
