using AutoMapper;
using Dto;
using Microsoft.EntityFrameworkCore;
using Repository;
using Model;
using System.Text.Json;
namespace Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;

        public UserServices(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<DtoUserNameEmailRoleId?> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            return _mapper.Map<User, DtoUserNameEmailRoleId>(user);
        }

        public async Task<DtoUserNameEmailRoleId> AddNewUser(DtoUserAll user)
        {
            int passwordStrength = _passwordService.GetStrengthByPassword(user.PasswordHash);
            if (passwordStrength >= 2)
            {
                var userEntity = _mapper.Map<DtoUserAll, User>(user);
                userEntity.Role = "Customer";
                var savedUser = await _userRepository.AddNewUser(userEntity);
                var dtoUser = _mapper.Map<User, DtoUserNameEmailRoleId>(savedUser);
                return dtoUser;
            }

            return null;
        }

        public async Task<DtoUserNameEmailRoleId?> Login(DtoUserEmailPassword value)
        {
            var loginUser = _mapper.Map<DtoUserEmailPassword, User>(value);
            var user = await _userRepository.Login(loginUser);

            if (user == null)
            {
                return null;
            }

            var dtoUser = _mapper.Map<User, DtoUserNameEmailRoleId>(user);
            return dtoUser;
        }

        public async Task<DtoUserNameEmailRoleId> Update(int id, DtoUserAll userDto)
        {
            int passwordStrength = _passwordService.GetStrengthByPassword(userDto.PasswordHash);
            if (passwordStrength < 2)
            {
                return null;
            }

            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return null;
            }

            _mapper.Map(userDto, existingUser);
            existingUser.UserId = id;
            var updatedUser = await _userRepository.Update(id, existingUser);

            return _mapper.Map<User, DtoUserNameEmailRoleId>(updatedUser);
        }

        public async Task<bool> IsAdminById(int id, string password)
        {
            var user = await _userRepository.GetUserByIdAndPassword(id, password);
            return user != null && user.Role == "Admin";
        }
    }
}
