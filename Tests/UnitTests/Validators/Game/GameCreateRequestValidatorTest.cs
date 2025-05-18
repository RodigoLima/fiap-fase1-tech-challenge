using Bogus;
using fiap_fase1_tech_challenge.Modules.Games.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Games.Messages;
using fiap_fase1_tech_challenge.Modules.Games.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Validators.Game
{
    public class GameCreateRequestValidatorTest
    {
        private GameCreateRequestValidator _validator;

        public GameCreateRequestValidatorTest()
        {
            _validator = new GameCreateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForName")]
        public void Validate_ShouldReturnRequiredFieldForName()
        {
            //Arrange
            var request = new GameCreateRequest
            {
                Genre = "test",
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Name).WithErrorMessage(GameMessages.Name.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMaxSizeForName")]
        public void Validate_ShouldReturnMaxSizeForName()
        {
            //Arrange
            var request = new GameCreateRequest
            {
                //name with 51 characteres
                Name= "Lorem ipsum dolor sit amet, consectetur adipiscing.",
                Genre = "test",
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Name).WithErrorMessage(GameMessages.Name.MaxLength);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMaxSizeForDescription")]
        public void Validate_ShouldReturnMaxSizeForDescription()
        {
            //Arrange
            var faker = new Faker();

            string description = "";
            while (description.Length <= 500)
            {
                description += " " + faker.Lorem.Sentence();
            }

            var request = new GameCreateRequest
            {
                Name = "Game test",
                Description = description,
                Genre = "test",
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Description).WithErrorMessage(GameMessages.Description.MaxLength);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Theory(DisplayName = "Validate_ShouldReturnPriceGreaterThanZero")]
        [InlineData(null)]
        [InlineData(0.0)]
        public void Validate_ShouldReturnPriceGreaterThanZero(double? price)
        {
            //Arrange
            var request = new GameCreateRequest
            {
                Name = "Game test",
                Genre = "test"
            };
            if (price != null) request.Price = (double)price;

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Price).WithErrorMessage(GameMessages.Price.GreaterThanZero);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForGenre")]
        public void Validate_ShouldReturnRequiredFieldForGenre()
        {
            //Arrange
            var request = new GameCreateRequest
            {
                Name = "Game test",
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Genre).WithErrorMessage(GameMessages.Genre.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMoreThanOneError")]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new GameCreateRequest
            {
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrors();
            result.Errors.Count().Should().BeGreaterThan(1);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnValidData")]
        public void Validate_ShouldReturnValidData()
        {
            //Arrange
            var faker = new Faker();

            string nome = faker.Commerce.ProductName();
            string description = faker.Commerce.ProductDescription();
            if (description.Length > 500)
                description = description.Substring(0, 500);

            var request = new GameCreateRequest
            {
                Name = nome,
                Description = description,
                Price = 10,
                Genre = "test"
            };

            var teste = request.Description.Count();

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
