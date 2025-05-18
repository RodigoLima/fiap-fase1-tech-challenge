using System.Net;

namespace fiap_fase1_tech_challenge.Common.Exceptions
{
    public class ValidationErrorException : FiapCloudGamesException
    {
        private readonly List<string> _errors;

        public ValidationErrorException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }
        public override List<string> GetErrorMessages() => _errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
