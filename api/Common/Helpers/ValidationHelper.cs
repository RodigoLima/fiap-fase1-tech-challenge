using fiap_fase1_tech_challenge.Common.Exceptions;
using FluentValidation;

namespace fiap_fase1_tech_challenge.Common.Helpers
{
    public class ValidationHelper
    {
        public static async Task ValidateAsync<T>(IValidator<T> validator, T request)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
        }
    }
}
