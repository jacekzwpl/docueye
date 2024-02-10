using Microsoft.AspNetCore.Mvc;

namespace DocuEye.Infrastructure.HttpProblemDetails
{
    /// <summary>
    /// Problem details for 400 response
    /// </summary>
    public class BadRequestProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="title">Problem details title</param>
        /// <param name="detail">Problem details description</param>
        public BadRequestProblemDetails(string title, string? detail = null)
        {
            this.Title = title;
            this.Detail = detail;
            this.Status = 400;
            this.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
        }
    }
}
