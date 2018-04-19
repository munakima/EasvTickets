using Elements;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BLL.WebApi.Gateway.Core
{
    internal static class HTTPRequestHelper
    {
        private static T CreateAndExecuteRequest<T>(string resourcePath, Method method, Action<RestRequest> action)
            where T : new()
        {
            var client = new RestClient(Constants.BLLWebApiBaseUrl);
            var request = new RestRequest(resourcePath, method);
            action(request);
            IRestResponse<T> response = client.Execute<T>(request);
            return response.Data;

        }

        public static T CreateGetRequest<T>(string resourcePath, int? id = null)
            where T : new()
        {
            return CreateAndExecuteRequest<T>(resourcePath, Method.GET,
                (restRequest) =>
                {
                    if(id!= null)
                    {
                        restRequest.AddUrlSegment("id", id.ToString());
                    }
                });
        }

        public static T CreateGetRequest<T>(string resourcePath, Dictionary<string, int> NameIdPairs)
            where T : new()
        {
            return CreateAndExecuteRequest<T>(resourcePath, Method.GET,
                (restRequest) =>
                {
                    foreach (var pair in NameIdPairs)
                    {
                        restRequest.AddUrlSegment(pair.Key, pair.Value.ToString());
                    }
                });
        }

        public static int CreatePostRequest<T>(string resourcePath, T model)
            where T : new()
        {
            return CreateAndExecuteRequest<int>(resourcePath, Method.POST,
                (restRequest) =>
                {
                    restRequest.AddJsonBody(model);
                });
        }
    }
}
