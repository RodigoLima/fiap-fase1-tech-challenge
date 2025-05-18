using fiap_fase1_tech_challenge.Modules.Authentication.Messages;
using System.Net;

namespace fiap_fase1_tech_challenge.Common.Exceptions
{
    public class InvalidTokenException : FiapCloudGamesException
    {
        public InvalidTokenException() : base(JwtMessages.Authentication.InvalidToken) { }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
