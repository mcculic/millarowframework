using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Millarow.Rest.Http.Defaults
{
    internal sealed class DefaultHttpClientFactory : IHttpClientFactory
    {
        private readonly List<Action<HttpClient>> _setup = new List<Action<HttpClient>>();

        public void Configure(Action<HttpClient> action)
        {
            action.AssertNotNull(nameof(action));

            _setup.Add(action);
        }

        public HttpClient CreateClient()
        {
            var client = new HttpClient();

            foreach (var action in _setup)
                action(client);

            return client;
        }
    }
}
