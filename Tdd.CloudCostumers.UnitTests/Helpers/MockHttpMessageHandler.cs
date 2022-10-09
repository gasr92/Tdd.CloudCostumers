using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tdd.CloudCostumers.API.Models;

namespace Tdd.CloudCostumers.UnitTests.Helpers
{
    public static class MockHttpMessageHandler<T>
    {
        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<User> expectedResponse, string endpoint)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handleMock = new Mock<HttpMessageHandler>();
            //var httpRequestMessage = new HttpRequestMessage
            //{
            //    RequestUri = new Uri(endpoint),
            //    Method = HttpMethod.Get
            //};

            handleMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return handleMock;
        }


        internal static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponse)
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponse))
            };

            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var handleMock = new Mock<HttpMessageHandler>();
            handleMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return handleMock;
        }



        internal static Mock<HttpMessageHandler> SetupReturn404()
        {
            var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };

            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handleMock = new Mock<HttpMessageHandler>();
            handleMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
            ).ReturnsAsync(mockResponse);

            return handleMock;
        }
    }
}
