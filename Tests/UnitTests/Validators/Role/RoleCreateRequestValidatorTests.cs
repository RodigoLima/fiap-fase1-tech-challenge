using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.Messages;
using fiap_fase1_tech_challenge.Modules.Users.Validators;
using FluentValidation.TestHelper;

namespace Tests.UnitTests.Validators.Role
{
    public class RoleCreateRequestValidatorTests
    {
        private readonly RoleCreateRequestValidator _validator;

        public RoleCreateRequestValidatorTests()
        {
            _validator = new RoleCreateRequestValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldReturnRequiredFieldForName")]
        public void Validate_ShouldReturnRequiredFieldForName()
        {
            //Arrange
            var request = new RoleCreateRequest
            {
                
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(r => r.Name).WithErrorMessage(RoleMessages.Name.Required);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "RoleCreateRequestValidator")]
        [Fact(DisplayName = "Validate_ShouldPassTheValidation")]
        public void Validate_ShouldPassTheValidation()
        {
            //Arrange
            var request = new RoleCreateRequest
            {
                Name = "Admin"
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
