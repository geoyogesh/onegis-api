using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace onegis_api.Utils
{
    //http://www.asp.net/web-api/overview/advanced/http-message-handlers
    public class RequestFormatHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //preprocess the request
            var qs = request.GetQueryStrings();
            if (qs.ContainsKey("f"))
            {
                if (qs["f"].ToLower().Equals("json") || qs["f"].ToLower().Equals("pjson"))
                {
                    var response = await base.SendAsync(request, cancellationToken);
                    if (qs.ContainsKey("callback"))
                    {
                        var str = String.Format("{0}({1})",qs["callback"],await response.Content.ReadAsStringAsync());
                        response.Content = new StringContent(str);
                    }
                    return response;
                }
            }

            //post process the request
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Error")
                };

                // Note: TaskCompletionSource creates a task that does not contain a delegate.
                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(resp);   // Also sets the task state to "RanToCompletion"
                return await tsc.Task;            
        }
    }
}