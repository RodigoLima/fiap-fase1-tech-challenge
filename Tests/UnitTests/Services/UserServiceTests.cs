using fiap_fase1_tech_challenge.Exceptions;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services;
using fiap_fase1_tech_challenge.Services.Interfaces;
using fiap_fase1_tech_challenge.Test.Utils;
using FluentAssertions;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRoleService> _roleService;
        private readonly Mock<IPasswordHasher> _passwordHasher;
        private readonly UserService _userService;
        public UserServiceTests()
        {
            _userRepository = new Mock<IUserRepository>();
            _roleService = new Mock<IRoleService>();
            _passwordHasher = new Mock<IPasswordHasher>();
            _userService = new UserService(_userRepository.Object, _roleService.Object, _passwordHasher.Object);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnAllUsers")]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            //Arrange
            var users = UserFaker.FakeListOfUser(10);
            _userRepository
                .Setup(u => u.GetAllAsync())
                .ReturnsAsync(users);

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Count().Should().Be(users.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnEmptyList")]
        public async Task GetAllAsync_ShouldReturnEmptyList()
        {
            //Arrange
            _userRepository
                .Setup(u => u.GetAllAsync())
                .ReturnsAsync([]);

            //Act
            var result = await _userService.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldReturnAnUser")]
        public async Task GetByIdAsync_ShouldReturnAnUser()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];

            _userRepository
                .Setup(u => u.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            //Act
            var result = await _userService.GetByIdAsync(user.Id);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(user.Id);
            result.Name.Should().Be(user.Name);
            result.Email.Should().Be(user.Email);
            result.Password.Should().Be(user.Password);
            result.RoleId.Should().Be(user.RoleId);
        }


        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldThrowUserNotFound")]
        public async Task GetByIdAsync_ShouldThrowUserNotFound()
        {
            //Arrange
            _userRepository
                .Setup(u => u.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.GetByIdAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(UserMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "CreateAsync_ShouldCreateAnUser")]
        public async Task CreateAsync_ShouldCreateTheUser()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];
            var role = new Role { Id = user.Id, Name = "RoleName" };

            var userData = new UserCreateRequest
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };

            _roleService
                .Setup(r => r.GetByIdAsync(userData.RoleId))
                .ReturnsAsync(role);

            _userRepository
                .Setup(u => u.CreateAsync(user));

            //Act
            var result = await _userService.CreateAsync(userData);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(userData.Name);
            result.Email.Should().Be(userData.Email);
            result.RoleName.Should().Be(role.Name);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "CreateAsync_ShouldThrowRoleNotFound")]
        public async Task CreateAsync_ShouldThrowRoleNotFound()
        {
            //Arrange
            var request = new UserCreateRequest
            {
                RoleId = 1
            };

            _roleService
                .Setup(r => r.GetByIdAsync(request.RoleId))
                .ReturnsAsync((Role?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.CreateAsync(request))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(RoleMessages.RoleNotFoundMessage);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "UpdateAsync_ShouldUpdateAnUser")]
        public async Task UpdateAsync_ShouldUpdateAnUser()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];
            var role = new Role
            {
                Id = user.RoleId,
                Name = It.IsAny<string>()
            };

            var request = new UserUpdateRequest
            {
                Name = It.IsAny<string>(),
                RoleId = role.Id
            };

            _userRepository
                .Setup(u => u.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            _roleService
                .Setup(r => r.GetByIdAsync(user.RoleId))
                .ReturnsAsync(role);

            //Act
            var result = await _userService.UpdateAsync(user.Id, request);

            //Assert
            result.Should().Be(true);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "UpdateAsync_ShouldUpdateThrowInvalidPassword")]
        public async Task UpdateAsync_ShouldUpdateThrowInvalidPassword()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];

            var role = new Role
            {
                Id = user.RoleId,
                Name = It.IsAny<string>()
            };

            var request = new UserUpdateRequest
            {
                Name = It.IsAny<string>(),
                RoleId = role.Id,
                NewPassword = "@NewPassword123",
                OldPassword = user.Password
            };

            _userRepository
                .Setup(u => u.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            _roleService
                .Setup(r => r.GetByIdAsync(user.RoleId))
                .ReturnsAsync(role);

            _passwordHasher
                .Setup(p => p.Verify(user.Password, It.IsAny<string>()))
                .Returns(false);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.UpdateAsync(user.Id, request))
                .Should()
                .ThrowAsync<ValidationException>()
                .WithMessage(UserMessages.Password.InvalidOld);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "UpdateAsync_ShouldThrowUserNotFound")]
        public async Task UpdateAsync_ShouldThrowUserNotFound()
        {
            //Arrange
            _userRepository
                .Setup(u => u.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.UpdateAsync(It.IsAny<int>(), It.IsAny<UserUpdateRequest>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(UserMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "UpdatePassword_ShouldUpdateAnUserPassword")]
        public void UpdatePassword_ShouldUpdateAnUserPassword()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];
            var newPassword = "@NewPassword123";
            var oldPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _passwordHasher
                .Setup(p => p.Verify(user.Password, It.IsAny<string>()))
                .Returns(true);

            //Act
            var result = _userService.UpdatePassword(user, user.Password, newPassword);

            //Assert
            result.Should().NotBe(oldPassword);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "UpdatePassword_ShouldThrowInvalidOldPassword")]
        public void UpdatePassword_ShouldThrowInvalidOldPassword()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];
            var newPassword = "@NewPassword123";

            _passwordHasher
                .Setup(p => p.Verify(user.Password, It.IsAny<string>()))
                .Returns(false);

            //Act & Assert
            FluentActions
                .Invoking(() => _userService.UpdatePassword(user, "invalidPassword", newPassword))
                .Should()
                .Throw<ValidationException>()
                .WithMessage(UserMessages.Password.InvalidOld);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "DeleteAsync_ShouldDeleteAnUser")]
        public async Task DeleteAsync_ShouldDeleteAnUser()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];

            _userRepository
                .Setup(u => u.GetByIdAsync(user.Id))
                .ReturnsAsync(user);

            _userRepository
                .Setup(u => u.DeleteAsync(user.Id))
                .ReturnsAsync(true);

            //Act
            var result = await _userService.DeleteAsync(user.Id);

            //Assert
            result.Should().Be(true);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "DeleteAsync_ShouldThrowUserNotFound")]
        public async Task DeleteAsync_ShouldThrowUserNotFound()
        {
            //Arrange
            var userId = 6;

            _userRepository
                .Setup(u => u.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.DeleteAsync(userId))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(UserMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "AuthenticateAsync_ShouldAuthenticateTheUser")]
        public async Task AuthenticateAsync_ShouldAuthenticateTheUser()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];

            _userRepository
                .Setup(u => u.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            _passwordHasher
                .Setup(p => p.Verify(user.Password, It.IsAny<string>()))
                .Returns(true);

            //Act
            var result = await _userService.AuthenticateAsync(user.Email, user.Password);

            //Act & Assert
            result.Should().Be(user);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "AuthenticateAsync_ShouldThrowInvalidLoginForEmail")]
        public async Task AuthenticateAsync_ShouldThrowInvalidLoginForEmail()
        {
            //Arrange
            _userRepository
                .Setup(u => u.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            _passwordHasher
                .Setup(p => p.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Should()
                .ThrowAsync<UnauthorizedAccessException>()
                .WithMessage(UserMessages.General.InvalidEmailOrPassword);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UserService")]
        [Fact(DisplayName = "AuthenticateAsync_ShouldThrowInvalidLoginForPassword")]
        public async Task AuthenticateAsync_ShouldThrowInvalidLoginForPassword()
        {
            //Arrange
            var user = UserFaker.FakeListOfUser(1)[0];

            _userRepository
                .Setup(u => u.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            _passwordHasher
                .Setup(p => p.Verify(user.Password, It.IsAny<string>()))
                .Returns(false);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _userService.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Should()
                .ThrowAsync<UnauthorizedAccessException>()
                .WithMessage(UserMessages.General.InvalidEmailOrPassword);
        }
    }
}
