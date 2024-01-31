﻿using DocuEye.CLI.ApiClient.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient
{
    public class DocuEyeApiClient : IDocuEyeApiClient
    {
        private readonly HttpClient httpClient;

        public DocuEyeApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        

        public Task<ImportWorkspaceResult> ImportWorkspace(ImportWorkspaceRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
