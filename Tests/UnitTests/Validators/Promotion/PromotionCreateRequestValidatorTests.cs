using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Validators.Promotion;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace Tests.UnitTests.Validators.Promotion
{
    public class PromotionCreateRequestValidatorTests
    {
        PromotionCreateRequestValidator _validator;

        public PromotionCreateRequestValidatorTests()
        {
            _validator = new PromotionCreateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnGreaterThanZeroForDiscount")]
        public void Validate_ShouldReturnRequiredFieldForDiscount()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(5),
                GameId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.DiscountPercentage)
                .WithErrorMessage(PromotionMessages.Discount.GreatherThan);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnLessThan101ForDiscount")]
        public void Validate_ShouldReturnLessThan101ForDiscount()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                DiscountPercentage = 101,
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(5),
                GameId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.DiscountPercentage)
                .WithErrorMessage(PromotionMessages.Discount.LessThan);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnGreaterOrEqualThanTodayForInitialDate")]
        public void Validate_ShouldReturnGreaterOrEqualThanTodayForInitialDate()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                DiscountPercentage = 10,
                InitialDate = DateTime.Now.AddDays(-5),
                FinalDate = DateTime.Now.AddDays(5),
                GameId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.InitialDate)
                .WithErrorMessage(PromotionMessages.InitialDate.GreaterOrEqual);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnGreaterThanInitialDateForFinalDate")]
        public void Validate_ShouldReturnGreaterThanInitialDateForFinalDate()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                DiscountPercentage = 10,
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(-5),
                GameId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.FinalDate)
                .WithErrorMessage(PromotionMessages.FinalDate.GreaterThan);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredForGameId")]
        public void Validate_ShouldReturnRequiredForGameId()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                DiscountPercentage = 10,
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(5)
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.GameId)
                .WithErrorMessage(PromotionMessages.Game.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMoreThanOneError")]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(5)
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrors();
            result.Errors.Count().Should().BeGreaterThan(1);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldPassTheValidation")]
        public void Validate_ShouldPassTheValidation()
        {
            //Arrange
            var request = new PromotionCreateRequest
            {
                DiscountPercentage = 10,
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(5),
                GameId = 1
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
