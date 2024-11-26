using System.Net;

namespace RedArbor.Domain.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base(HttpStatusCode.NotFound)
        {
        }

        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {
        }
    }
}