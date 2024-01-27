using Microsoft.AspNetCore.Mvc;
using System;

namespace DocuEye.Infrastructure.HttpProblemDetails
{
    /// <summary>
    /// Problem details for 404 response
    /// </summary>
    public class NotFoundProblemDetails : ProblemDetails
    {
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="title">Problem details title</param>
        /// <param name="detail">Problem details description</param>
        public NotFoundProblemDetails(string title, string? detail = null) {
            this.Title = title;
            this.Detail = detail;
            this.Status = 404;
            this.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
        }
    }
}
