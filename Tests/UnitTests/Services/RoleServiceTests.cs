using fiap_fase1_tech_challenge.DTOs.Role;
using fiap_fase1_tech_challenge.Exceptions;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services;
using fiap_fase1_tech_challenge.Services.Interfaces;
using fiap_fase1_tech_challenge.Test.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Utils;

namespace Tests.UnitTests.Services
{
    public class RoleServiceTests
    {
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<ILogger<RoleService>> _logger;
        private readonly RoleService _roleService;

        public RoleServiceTests()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _logger = new Mock<ILogger<RoleService>>();
            _roleService = new RoleService(_roleRepository.Object, _logger.Object);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnAllRoles")]
        public async Task GetAllAsync_ShouldReturnAllRoles()
        {
            //Arrange
            var roles = RoleFaker.FakeListOfRoles(10);
            _roleRepository
                .Setup(u => u.GetAllAsync())
                .ReturnsAsync(roles);

            //Act
            var result = await _roleService.GetAllAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Count().Should().Be(roles.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnEmptyList")]
        public async Task GetAllAsync_ShouldReturnEmptyList()
        {
            //Arrange
            _roleRepository
            .Setup(u => u.GetAllAsync())
                .ReturnsAsync([]);

            //Act
            var result = await _roleService.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldReturnARole")]
        public async Task GetByIdAsync_ShouldReturnARole()
        {
            //Arrange
            var role = RoleFaker.FakeListOfRoles(1)[0];

            _roleRepository
                .Setup(u => u.GetByIdAsync(role.Id))
                .ReturnsAsync(role);

            //Act
            var result = await _roleService.GetByIdAsync(role.Id);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(role.Id);
            result.Name.Should().Be(role.Name);
        }


        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldThrowRoleNotFound")]
        public async Task GetByIdAsync_ShouldThrowRoleNotFound()
        {
            //Arrange
            _roleRepository
                .Setup(u => u.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _roleService.GetByIdAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(RoleMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "CreateAsync_ShouldCreateARole")]
        public async Task CreateAsync_ShouldCreateARole()
        {
            //Arrange
            var role = RoleFaker.FakeListOfRoles(1)[0];
            var request = new RoleCreateRequest
            {
                Name = role.Name
            };

            _roleRepository
                .Setup(u => u.CreateAsync(It.IsAny<Role>()))
                .ReturnsAsync(role);

            //Act
            var response = await _roleService.CreateAsync(request);

            //Assert
            response.Id.Should().NotBe(0);
            response.Name.Should().Be(request.Name);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "UpdateAsync_ShouldUpdateARole")]
        public async Task UpdateAsync_ShouldUpdateARole()
        {
            //Arrange
            var role = RoleFaker.FakeListOfRoles(1)[0];

            var request = new RoleUpdateRequest
            {
                Name = "New role"
            };

            _roleRepository
                .Setup(r => r.GetByIdAsync(role.Id))
                .ReturnsAsync(role);

            _roleRepository
                .Setup(u => u.UpdateAsync(It.IsAny<Role>()))
                .ReturnsAsync(true);

            //Act
            var response = await _roleService.UpdateAsync(role.Id, request);

            //Assert
            response.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "UpdateAsync_ShouldThrowRoleNotFound")]
        public async Task UpdateAsync_ShouldThrowRoleNotFound()
        {
            //Arrange
            var request = new RoleUpdateRequest
            {
                Name = "New name"
            };

            _roleRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _roleService.UpdateAsync(It.IsAny<int>(), request))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(RoleMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "DeleteAsync_ShouldDeleteTheRole")]
        public async Task DeleteAsync_ShouldDeleteTheRole()
        {
            //Arrange
            var role = RoleFaker.FakeListOfRoles(1)[0];
            _roleRepository
                .Setup(r => r.GetByIdAsync(role.Id))
                .ReturnsAsync(role);

            _roleRepository
                .Setup(r => r.DeleteAsync(role.Id))
                .ReturnsAsync(true);

            //Act
            var response = await _roleService.DeleteAsync(role.Id);

            //Act & Assert
            response.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleService")]
        [Fact(DisplayName = "DeleteAsync_ShouldThrowRoleNotFound")]
        public async Task DeleteAsync_ShouldThrowRoleNotFound()
        {
            //Arrange
            _roleRepository
                .Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Role?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _roleService.DeleteAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(RoleMessages.General.NotFound);
        }
    }
}
