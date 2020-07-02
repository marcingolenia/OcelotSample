using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace ApiGateway
{
    public class ClientsInvoicesAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            var clientResponse = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var invoicesResponse = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var aggregatedResponse = 
                clientResponse.Insert(clientResponse.Length - 1, $", \"invoices\": {invoicesResponse}");
            var headers = responses.SelectMany(httpCtx => httpCtx.Items.DownstreamResponse().Headers).ToList();
            return new DownstreamResponse(new StringContent(aggregatedResponse, Encoding.UTF8, "application/json"), 
                HttpStatusCode.OK, headers, "some reason");
        }
    }
}