using System.Net;

namespace fiap_fase1_tech_challenge.Common.Exceptions
{
    public class NotFoundException : FiapCloudGamesException
    {
        public NotFoundException(string message) : base(message) { }
        public override List<string> GetErrorMessages() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}