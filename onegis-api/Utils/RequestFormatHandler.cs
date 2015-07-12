using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace onegis_api.Utils
{
    //http://www.asp.net/web-api/overview/advanced/http-message-handlers
    public class RequestFormatHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //preprocess the request
            Dictionary<String, String> getq = null;
            if (request.Method.Method.Equals("POST") || (request.Method.Method.Equals("GET")))
            {
                if (request.Method.Method.Equals("POST") && request.Content.IsFormData())
                {
                    getq = await request.GetFormData();
                }
                else if (request.Method.Method.Equals("GET"))
                {
                    getq = request.GetQueryStrings();
                }

                if (getq.ContainsKey("f"))
                {
                    if (getq["f"].ToLower().Equals("json") || getq["f"].ToLower().Equals("pjson"))
                    {
                        var response = await base.SendAsync(request, cancellationToken);
                        if (getq.ContainsKey("callback"))
                        {
                            var str = String.Format("{0}({1})", getq["callback"], await response.Content.ReadAsStringAsync());
                            response.Content = new StringContent(str, Encoding.UTF8, "text/plain");
                            return response;
                        }
                        response.Content = new StringContent(await response.Content.ReadAsStringAsync(), Encoding.UTF8, "text/plain");
                        return response;
                    }
                }
            }

            //post process the request
            var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent("Error thrown by handler")
            };

            // Note: TaskCompletionSource creates a task that does not contain a delegate.
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(resp);   // Also sets the task state to "RanToCompletion"
            return await tsc.Task;

        }
    }
}