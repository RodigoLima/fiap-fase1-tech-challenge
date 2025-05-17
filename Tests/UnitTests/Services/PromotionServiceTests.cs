using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.Exceptions;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services;
using fiap_fase1_tech_challenge.Test.Utils;
using FluentAssertions;
using Moq;
using Tests.Utils;
using static fiap_fase1_tech_challenge.Messages.PromotionMessages;

namespace Tests.UnitTests.Services
{
    public class PromotionServiceTests
    {
        private readonly Mock<IPromotionRepository> _promotionRepository;
        private readonly Mock<IGameService> _gameService;
        private readonly PromotionService _promotionService;

        public PromotionServiceTests()
        {
            _promotionRepository = new Mock<IPromotionRepository>();
            _gameService = new Mock<IGameService>();
            _promotionService = new PromotionService(_promotionRepository.Object, _gameService.Object);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnAllPromotions")]
        public async Task GetAllAsync_ShouldReturnAllPromotions()
        {
            //Arrange
            var promotions = PromotionFaker.FakeListOfPromotions(10);

            _promotionRepository
                .Setup(p => p.GetAllAsync())
                .ReturnsAsync(promotions);

            //Act
            var response = await _promotionService.GetAllAsync();

            //Assert
            response.Should().NotBeEmpty();
            response.Count().Should().Be(promotions.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "GetAllAsync_ShouldReturnAnEmptyList")]
        public async Task GetAllAsync_ShouldReturnAnEmptyList()
        {
            //Arrange
            _promotionRepository
                .Setup(p => p.GetAllAsync())
                .ReturnsAsync([]);

            //Act
            var response = await _promotionService.GetAllAsync();

            //Assert
            response.Should().BeEmpty();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldReturnAPromotion")]
        public async Task GetByIdAsync_ShouldReturnAPromotion()
        {
            //Arrange
            var promotion = PromotionFaker.FakeListOfPromotions(1)[0];

            _promotionRepository
                .Setup(p => p.GetByIdAsync(promotion.Id))
                .ReturnsAsync(promotion);

            //Act
            var response = await _promotionService.GetByIdAsync(promotion.Id);

            //Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(promotion.Id);
            response.DiscountPercentage.Should().Be(promotion.DiscountPercentage);
            response.InitialDate.Should().Be(promotion.InitialDate);
            response.FinalDate.Should().Be(promotion.FinalDate);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "GetByIdAsync_ShouldThrowNotFound")]
        public async Task GetByIdAsync_ShouldThrowNotFound()
        {
            //Arrange
            _promotionRepository
                .Setup(p => p.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Promotion?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _promotionService.GetByIdAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(PromotionMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "CreateAsync_ShouldCreateAPromotion")]
        public async Task CreateAsync_ShouldCreateAPromotion()
        {
            //Arrange
            var promotion = PromotionFaker.FakeListOfPromotions(1)[0];
            var game = GameFaker.FakeListOfGame(1)[0];

            var request = new PromotionCreateRequest
            {
                DiscountPercentage = promotion.DiscountPercentage,
                InitialDate = promotion.InitialDate,
                FinalDate = promotion.FinalDate,
                GameId = promotion.GameId
            };

            _gameService
                .Setup(g => g.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(game);

            _promotionRepository
                .Setup(p => p.CreateAsync(It.IsAny<Promotion>()))
                .ReturnsAsync(promotion);

            //Act
            var result = await _promotionService.CreateAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(promotion.Id);
            result.DiscountPercentage.Should().Be(promotion.DiscountPercentage);
            result.InitialDate.Should().Be(promotion.InitialDate);
            result.FinalDate.Should().Be(promotion.FinalDate);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "UpdateAsync_ShouldUpdateAPromotion")]
        public async Task UpdateAsync_ShouldUpdateAPromotion()
        {
            //Arrange
            var promotion = PromotionFaker.FakeListOfPromotions(1)[0];
            var game = GameFaker.FakeListOfGame(1)[0];
            var request = new PromotionUpdateRequest
            {
                DiscountPercentage = 20
            };

            _promotionRepository
                .Setup(p => p.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(promotion);

            _gameService
                .Setup(g => g.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(game);

            _promotionRepository
                .Setup(p => p.UpdateAsync(It.IsAny<Promotion>()))
                .ReturnsAsync(true);

            //Act
            var result = await _promotionService.UpdateAsync(It.IsAny<int>(), request);

            //Assert
            result.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "UpdateAsync_ShouldThrowNotFound")]
        public async Task UpdateAsync_ShouldThrowNotFound()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                DiscountPercentage = 20
            };

            _promotionRepository
                .Setup(p => p.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Promotion?) null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _promotionService.UpdateAsync(It.IsAny<int>(), request))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(PromotionMessages.General.NotFound);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "DeleteAsync_ShouldDeleteAPromotion")]
        public async Task DeleteAsync_ShouldDeleteAPromotion()
        {
            //Arrange
            var promotion = PromotionFaker.FakeListOfPromotions(1)[0];

            _promotionRepository
                .Setup(p => p.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(promotion);

            _promotionRepository
                .Setup(p => p.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            //Act
            var result = await _promotionService.DeleteAsync(promotion.Id);

            //Assert
            result.Should().BeTrue();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionService")]
        [Fact(DisplayName = "DeleteAsync_ShouldThrowNotFound")]
        public async Task DeleteAsync_ShouldThrowNotFound()
        {
            //Arrange
            _promotionRepository
                .Setup(p => p.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Promotion?)null);

            //Act & Assert
            await FluentActions
                .Awaiting(() => _promotionService.DeleteAsync(It.IsAny<int>()))
                .Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage(PromotionMessages.General.NotFound);
        }
    }
}
