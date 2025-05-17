using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Exceptions;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services;
using fiap_fase1_tech_challenge.Test.Utils;
using FluentAssertions;
using Moq;
using static Bogus.DataSets.Name;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Services
{
    public class GameServiceTests
    {
        private readonly Mock<IGameRepository> _gameRepository;
        private readonly GameService _gameService;
        public GameServiceTests()
        {
            _gameRepository = new Mock<IGameRepository>();
            _gameService = new GameService(_gameRepository.Object);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnAllGames")]
        public async Task GetAllAsync_ShouldReturnAllGames()
        {
            //Arrange
            var games = GameFaker.FakeListOfGame(10);
            _gameRepository
                .Setup(g => g.GetAllAsync())
                .ReturnsAsync(games);

            //Act
            var result = await _gameService.GetAllAsync();

            //Assert
            result.Should().NotBeEmpty();
            result.Count().Should().Be(games.Count());
        }
        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnEmptyList")]
        public async Task GetAllAsync_ShouldReturnEmptyList()
        {
            //Arrange
            _gameRepository
                .Setup(g => g.GetAllAsync())
                .ReturnsAsync([]);

            //Act
            var result = await _gameService.GetAllAsync();

            //Assert
            result.Should().BeEmpty();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldReturnAGameByItsId")]
        public async Task GetByIdAsync_ShouldReturnAGameByItsId()
        {
            //Arrange
            var games = GameFaker.FakeListOfGame(10);
            var gameToReturn = games[5];

            _gameRepository
                .Setup(g => g.GetByIdAsync(gameToReturn.Id))
                .ReturnsAsync(gameToReturn);

            //Act
            var result = await _gameService.GetByIdAsync(gameToReturn.Id);

            //Assert
            result.Should().Be(gameToReturn);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldThrowGameNotFound")]
        public async Task GetByIdAsync_ShouldThrowGameNotFound()
        {
            //Arrange
            _gameRepository
                .Setup(g => g.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Game?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _gameService.GetByIdAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(GameMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "CreateAsync_ShouldCreateAGame")]
        public async Task CreateAsync_ShouldCreateAGame()
        {
            //Arrange
            var game = GameFaker.FakeListOfGame(1)[0];
            var request = new GameCreateRequest
            {
                Name = game.Name,
                Description = game.Description,
                Genre = game.Genre,
                Price = game.Price,
                ReleasedDate = game.ReleasedDate
            };

            _gameRepository
                .Setup(g => g.CreateAsync(It.IsAny<Game>()))
                .ReturnsAsync(game);

            //Act
            var result = await _gameService.CreateAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(game.Id);
            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);
            result.Price.Should().Be(request.Price);
            result.ReleasedDate.Should().Be(request.ReleasedDate);
            result.Genre.Should().Be(request.Genre);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "UpdateAsync_ShouldUpdateTheGame")]
        public async Task UpdateAsync_ShouldUpdateTheGame()
        {
            //Arrange
            var game = GameFaker.FakeListOfGame(1)[0];
            var request = new GameUpdateRequest
            {
                Name = "New Game Name"
            };

            _gameRepository
                .Setup(g => g.GetByIdAsync(game.Id))
                .ReturnsAsync(game);

            _gameRepository
                .Setup(g => g.UpdateAsync(It.IsAny<Game>()))
                .ReturnsAsync(true);

            //Act
            var result = await _gameService.UpdateAsync(game.Id, request);

            //Assert
            result.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "UpdateAsync_ShouldThrowGameNotFound")]
        public async Task UpdateAsync_ShouldThrowGameNotFound()
        {
            //Arrange
            var request = new GameUpdateRequest
            {
                Name = "New Game Name"
            };

            _gameRepository
                .Setup(g => g.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Game?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _gameService.UpdateAsync(It.IsAny<int>(), request))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(GameMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "DeleteAsync_ShouldDeleteTheGame")]
        public async Task DeleteAsync_ShouldDeleteTheGame()
        {
            //Arrange
            var game = GameFaker.FakeListOfGame(1)[0];

            _gameRepository
                .Setup(g => g.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(game);

            _gameRepository
                .Setup(g => g.DeleteAsync(game.Id))
                .ReturnsAsync(true);

            //Act
            var result = await _gameService.DeleteAsync(game.Id);

            //Assert
            result.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameService")]
        [Fact(DisplayName = "DeleteAsync_ShouldThrowGameNotFound")]
        public async Task DeleteAsync_ShouldThrowGameNotFound()
        {
            //Arrange
            _gameRepository
                .Setup(g => g.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Game?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _gameService.DeleteAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(GameMessages.General.NotFound);
        }
    }
}
