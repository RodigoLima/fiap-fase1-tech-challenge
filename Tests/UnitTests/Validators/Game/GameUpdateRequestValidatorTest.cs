﻿using Bogus;
using fiap_fase1_tech_challenge.Modules.Games.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Games.Messages;
using fiap_fase1_tech_challenge.Modules.Games.Validators;
using FluentValidation.TestHelper;

namespace fiap_fase1_tech_challenge.Test.UnitTests.Validators.Game
{
    public class GameUpdateRequestValidatorTest
    {
        private readonly GameUpdateRequestValidator _validator;

        public GameUpdateRequestValidatorTest()
        {
            _validator = new GameUpdateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMaxSizeForName")]
        public void Validate_ShouldReturnMaxSizeForName()
        {
            //Arrange
            var request = new GameUpdateRequest
            {
                //name with 51 characteres
                Name = "Lorem ipsum dolor sit amet, consectetur adipiscing.",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Name).WithErrorMessage(GameMessages.Name.MaxLength);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnMaxSizeForDescription")]
        public void Validate_ShouldReturnMaxSizeForDescription()
        {
            //Arrange
            var faker = new Faker();
            string description = "";
            while(description.Length <= 500)
            {
                description += " " + faker.Lorem.Sentence();
            }
            var request = new GameUpdateRequest
            {
                Description = description,
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Description).WithErrorMessage(GameMessages.Description.MaxLength);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameUpdateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnPriceGreaterThanZero")]
        public void Validate_ShouldReturnPriceGreaterThanZero()
        {
            //Arrange
            var request = new GameUpdateRequest
            {
                Price = 0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(g => g.Price).WithErrorMessage(GameMessages.Price.GreaterThanZero);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "GameUpdateRequestValidator")]
        [Theory(DisplayName = "Validate_ShouldPassTheValidation")]
        [MemberData(nameof (TestData.GenerateValidData),MemberType =typeof(TestData))]
        public void Validate_ShouldPassTheValidation(GameUpdateRequest request)
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
                new GameUpdateRequest{Name = "Game test"}
            };

            yield return new object[]
            {
                new GameUpdateRequest{Description = "Game description"}
            };

            yield return new object[]
            {

                new GameUpdateRequest{Price = 10}
            };

            yield return new object[]
            {

                new GameUpdateRequest{Genre = "Test"}
            };
        }
    }
}
