using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests.HttpHandlers
{
    public class IncludeFileHttpMessageHandler : HttpMessageHandler
    {
        private string filePath;

        public IncludeFileHttpMessageHandler(string filePath)
        {
            this.filePath = filePath;
        }

        sealed protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {

            string text = File.ReadAllText(filePath);
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(text, Encoding.UTF8, "text/plain"),
            };

            return Task.FromResult(response);
        }
    }
}
