using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace Image_box.Models.SecureAttachmentsDownload.Services
{
    public class SecureUrlGenerator : ISecureUrlGenerator
    {
        private readonly IMemoryCache _memoryCache;

        public SecureUrlGenerator(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public string GenerateSecureAttachmentUrl(string id, string url)
        {
            var token = Guid.NewGuid().ToString().ToLower();
            StoreToken(id, token);
            var separator = url.Contains("?") ? "&" : "?";
            return $"{url}{separator}token={token}";
        }

        public bool ValidateToken(string id, string token)
        {
            return  _memoryCache.Get(id) is List<string> tokens && tokens.Contains(token);
        }

        public bool ValidateUrl(string url, string id)
        {
            var uri = new Uri(url);
            var queryStringParams = uri.Query.Split("&");
            foreach (var param in queryStringParams)
            {
                var values = param.Split("=");
                if (values[0].ToLower() == "token") return ValidateToken(id, values[1]);
            }

            return false;
        }

        private bool IsTokenValid(string id, string token)
        {
            return _memoryCache.Get(id) is List<string> tokens && tokens.Contains(token);
        }

        private void StoreToken(string id, string token)
        {
            var tokens = _memoryCache.Get(id) as List<string>;
            if (tokens == null)
                tokens = new List<string>();
            tokens.Add(token);

            _memoryCache.Set(id, tokens);
        }
    }
}