using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class ApiBase {

        protected readonly string BaseUrl;
		protected readonly string AccessToken;
		
		public ApiBase(string baseUrl) : this(baseUrl, null) {}

		public ApiBase(string baseUrl, string accessToken) {
			BaseUrl = baseUrl.TrimEnd('/') + '/';
			AccessToken = accessToken;
		}

        protected async Task<GetResult<T>> GetResource<T>(string resource, bool relative = true, string accessToken = null) {
			var httpClient = CreateHttpClient(accessToken);
            var uri = GetResourceURI(resource, relative);
            var response = await httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode) {
                // TODO: Get meaningful error
                return new GetResult<T>("Could not get resource");
            } else {
                T result = await response.Content.ReadAsAsync<T>();
                return new GetResult<T>(result);
            }
		}


        protected async Task<PostResult<T>> PostResource<T>(string resource, bool relative = true, string accessToken = null) {
            var httpClient = CreateHttpClient(accessToken);
            var uri = GetResourceURI(resource, relative);
            var empty = new StringContent("");
            var response = await httpClient.PostAsync(uri, empty);

            if (!response.IsSuccessStatusCode) {
                string error = await response.Content.ReadAsStringAsync();
                return new PostResult<T>(error);
            } else {
                T result = await response.Content.ReadAsAsync<T>();
                return new PostResult<T>(result);
            }
        }

        protected async Task<PostResult<R>> PostResource<T, R>(string resource, T content, bool relative = true, string accessToken = null) {
            var httpClient = CreateHttpClient(accessToken);
            var uri = GetResourceURI(resource, relative);
            var response = await httpClient.PostAsync<T>(uri, content, new JsonMediaTypeFormatter());

            if (!response.IsSuccessStatusCode) {
                string error = await response.Content.ReadAsStringAsync();
                return new PostResult<R>(error);
            } else {
                R result = await response.Content.ReadAsAsync<R>();
                return new PostResult<R>(result);
            }
        }

        protected async Task<DeleteResult> DeleteResource(string resource, bool relative = true, string accessToken = null) {
            var httpClient = CreateHttpClient(accessToken);
            var uri = GetResourceURI(resource, relative);
            var response = await httpClient.DeleteAsync(uri);

            if (!response.IsSuccessStatusCode) {
                string error = await response.Content.ReadAsStringAsync();
                return new DeleteResult(error);
            } else {
                return new DeleteResult();
            }
        }

		protected HttpClient CreateHttpClient(string accessToken = null) {
			if (accessToken == null) {
				accessToken = AccessToken;
			}

			if (accessToken == null) {
				throw new ArgumentNullException("Must supply an access token at construction or on this method");
			}

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			return httpClient;
		}

        private Uri GetResourceURI(string resource, bool relative) {
            if (relative) {
                resource = BaseUrl + resource;
            }

            return new Uri(resource);
        }
	}
}