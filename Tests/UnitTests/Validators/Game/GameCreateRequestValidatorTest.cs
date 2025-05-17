using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Validators.Game;
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
            var request = new GameCreateRequest
            {
                Name = "Game test",
                //Description with 201 characteres
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut " +
                "labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut a.",
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
            var request = new GameCreateRequest
            {
                //Name with 50 caracteres
                Name = "Lorem ipsum dolor sit amet, consectetur adipisci.",
                //Description with 200 caracteres
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt " +
                "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi eta.",
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
