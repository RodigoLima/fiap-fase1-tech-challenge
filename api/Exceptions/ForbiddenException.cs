using fiap_fase1_tech_challenge.Messages;
using System.Net;

namespace fiap_fase1_tech_challenge.Exceptions
{
    public class ForbiddenException : FiapCloudGamesException
    {
        public ForbiddenException() : base(JwtMessages.Authorization.Forbidden) { }
        public override List<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
    }
}
