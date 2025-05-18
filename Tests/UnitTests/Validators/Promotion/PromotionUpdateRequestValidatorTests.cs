using fiap_fase1_tech_challenge.Modules.Promotions.Messages;
using fiap_fase1_tech_challenge.Modules.Promotions.Validators;
using FluentValidation.TestHelper;

namespace Tests.UnitTests.Validators.Promotion
{
    public class PromotionUpdateRequestValidatorTests
    {
        private readonly PromotionUpdateRequestValidator _validator;

        public PromotionUpdateRequestValidatorTests()
        {
            _validator = new PromotionUpdateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnGreaterThanZeroForDiscount")]
        public void Validate_ShouldReturnRequiredFieldForDiscount()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                DiscountPercentage = 0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.DiscountPercentage)
                .WithErrorMessage(PromotionMessages.Discount.GreatherThan);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnLessThan101ForDiscount")]
        public void Validate_ShouldReturnLessThan101ForDiscount()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                DiscountPercentage = 101
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.DiscountPercentage)
                .WithErrorMessage(PromotionMessages.Discount.LessThan);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnGreaterOrEqualThanTodayForInitialDate")]
        public void Validate_ShouldReturnGreaterOrEqualThanTodayForInitialDate()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                InitialDate = DateTime.Now.AddDays(-5),
                FinalDate = DateTime.Now.AddDays(5),
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.InitialDate)
                .WithErrorMessage(PromotionMessages.InitialDate.GreaterOrEqual);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredForFinalDate")]
        public void Validate_ShouldReturnRequiredForFinalDate()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                InitialDate = DateTime.Now,
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.FinalDate)
                .WithErrorMessage(PromotionMessages.FinalDate.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnGreaterThanInitialDateForFinalDate")]
        public void Validate_ShouldReturnGreaterThanInitialDateForFinalDate()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                InitialDate = DateTime.Now,
                FinalDate = DateTime.Now.AddDays(-5)
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.FinalDate)
                .WithErrorMessage(PromotionMessages.FinalDate.GreaterThan);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredForInitialDate")]
        public void Validate_ShouldReturnRequiredForInitialDate()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                FinalDate = DateTime.Now.AddDays(5)
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.InitialDate)
                .WithErrorMessage(PromotionMessages.InitialDate.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredForGameId")]
        public void Validate_ShouldReturnRequiredForGameId()
        {
            //Arrange
            var request = new PromotionUpdateRequest
            {
                GameId = 0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result
                .ShouldHaveValidationErrorFor(g => g.GameId)
                .WithErrorMessage(PromotionMessages.Game.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "PromotionUpdateRequestValidator")]
        [Theory(DisplayName = "Validate_ShouldPassTheValidation")]
        [MemberData(nameof(TestData.GenerateValidData), MemberType = typeof(TestData))]
        public void Validate_ShouldPassTheValidation(PromotionUpdateRequest request)
        {
            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }

    public static class TestData
    {
        public static IEnumerable<object[]> GenerateValidData()
        {
            yield return new object[]
            {
                new PromotionUpdateRequest{DiscountPercentage = 10}
            };

            yield return new object[]
            {
                new PromotionUpdateRequest{InitialDate = DateTime.Now, FinalDate = DateTime.Now.AddDays(5)}
            };

            yield return new object[]
            {

                new PromotionUpdateRequest{GameId = 10}
            };
        }
    }
}
