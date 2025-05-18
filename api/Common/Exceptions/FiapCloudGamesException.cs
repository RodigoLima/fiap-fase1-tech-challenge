using System.Net;

namespace fiap_fase1_tech_challenge.Common.Exceptions
{
    public abstract class FiapCloudGamesException : SystemException
    {
        protected FiapCloudGamesException(string message) : base(message) { }

        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}