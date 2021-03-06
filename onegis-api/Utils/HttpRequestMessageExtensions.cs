﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace onegis_api.Utils
{
        //http://weblog.west-wind.com/posts/2013/Apr/15/WebAPI-Getting-Headers-QueryString-and-Cookie-Values
        /// <summary>
        /// Extends the HttpRequestMessage collection
        /// </summary>
        public static class HttpRequestMessageExtensions
        {
            
            public static async Task<Dictionary<string, string>> GetFormData(this HttpRequestMessage request)
            {
                var s = (await request.Content.ReadAsStringAsync()).Split('\n');
                var p = new Dictionary<string, string>();
                foreach (var item in s)
                {
                    var i= item.Split(':');
                    p.Add(i[0].Trim(),i[1].Trim());
                }
                 return p;
            }


            /// <summary>
            /// Returns a dictionary of QueryStrings that's easier to work with 
            /// than GetQueryNameValuePairs KevValuePairs collection.
            /// 
            /// If you need to pull a few single values use GetQueryString instead.
            /// </summary>
            /// <param name="request"></param>
            /// <returns></returns>
            public static Dictionary<string, string> GetQueryStrings(this HttpRequestMessage request)
            {
                return request.GetQueryNameValuePairs()
                              .ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            }

            /// <summary>
            /// Returns an individual querystring value
            /// </summary>
            /// <param name="request"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string GetQueryString(this HttpRequestMessage request, string key)
            {
                // IEnumerable<KeyValuePair<string,string>> - right!
                var queryStrings = request.GetQueryNameValuePairs();
                if (queryStrings == null)
                    return null;

                var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
                if (string.IsNullOrEmpty(match.Value))
                    return null;

                return match.Value;
            }

            /// <summary>
            /// Returns an individual HTTP Header value
            /// </summary>
            /// <param name="request"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string GetHeader(this HttpRequestMessage request, string key)
            {
                IEnumerable<string> keys = null;
                if (!request.Headers.TryGetValues(key, out keys))
                    return null;

                return keys.First();
            }

            /// <summary>
            /// Retrieves an individual cookie from the cookies collection
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cookieName"></param>
            /// <returns></returns>
            public static string GetCookie(this HttpRequestMessage request, string cookieName)
            {
                CookieHeaderValue cookie = request.Headers.GetCookies(cookieName).FirstOrDefault();
                if (cookie != null)
                    return cookie[cookieName].Value;

                return null;
            }

        }
    
}