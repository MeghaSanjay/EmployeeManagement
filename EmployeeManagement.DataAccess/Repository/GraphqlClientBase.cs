using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EmployeeManagement.DataAccess.Repository
{
    public abstract class GraphqlClientBase
    {
        public readonly GraphQLHttpClient _graphQLHttpClient;
        public GraphqlClientBase()
        {
            if (_graphQLHttpClient == null)
            {
                _graphQLHttpClient = GetGraphQlApiClient();
            }
        }

        public GraphQLHttpClient GetGraphQlApiClient()
        {
            var endpoint = "https://userdemographql.hasura.app/v1/graphql";

            var httpClientOption = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", "admin");

            return new GraphQLHttpClient(httpClientOption, new NewtonsoftJsonSerializer(), httpClient);
        }
    }
}
