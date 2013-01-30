using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Web.Code {
	public class ApiBase {

		protected readonly Uri BaseUrl;
		protected readonly string AccessToken;

		public ApiBase(string baseUrl) : this(baseUrl, null) {}

		public ApiBase(string baseUrl, string accessToken) {
			BaseUrl = new Uri(baseUrl);
			AccessToken = accessToken;
		}

		protected T GetResource<T>(string resource, bool relative = true, string accessToken = null) {
			var httpClient = CreateHttpClient(accessToken);

			if (relative) {
				resource = (new Uri(BaseUrl, resource)).ToString();
			}

			HttpResponseMessage response = httpClient.GetAsync(resource).Result;

			response.EnsureSuccessStatusCode();

			T info = response.Content.ReadAsAsync<T>().Result;
			return info;
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
			httpClient.DefaultRequestHeaders.Add("X-Version", "1");
						
			return httpClient;
		}
	}
}