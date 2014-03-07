using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.Messaging;
using System.Threading.Tasks;


namespace Web.Code {
	public class AuthorizationApi : ApiBase {

		private readonly AuthorizationServerDescription ServerDescription;
		private readonly string ConsumerKey;
		private readonly string ConsumerSecret;

		public AuthorizationApi(string baseUrl, string consumerKey, string consumerSecret) 
			: base(baseUrl) {
			
			ConsumerKey = consumerKey;
			ConsumerSecret = consumerSecret;

			var authEndpoint = new UriBuilder(BaseUrl);
			authEndpoint.Path = "/oauth/auth";
			
			var tokenEndpoint = new UriBuilder(BaseUrl);
			tokenEndpoint.Path = "/oauth/token";

			ServerDescription = new AuthorizationServerDescription {
				AuthorizationEndpoint = authEndpoint.Uri,
				TokenEndpoint = tokenEndpoint.Uri,
				ProtocolVersion = ProtocolVersion.V20
			};
		}

		public OutgoingWebResponse StartOAuth(string callback, string[] scopes) {
			var state = new AuthorizationState(scopes);
			state.Callback = new Uri(callback);

			var client = CreateWebServerClient();
			return client.PrepareRequestUserAuthorization(state);
		}
		
		public async Task<FinishedAuthorizedModel> FinishOAuth(HttpRequestBase request, string[] scopes) {
			//Finish processing oauth
			var client = CreateWebServerClient();
			var auth = client.ProcessUserAuthorization(request);

			//Validate token is correct
			var url = "/oauth/tokeninfo?access_token=" + auth.AccessToken + "&scopes=" + String.Join("&scopes=", scopes);
			var result = await this.GetResource<TokenInfoModel>(url, accessToken: auth.AccessToken);

            var tokenInfo = ValidateToken(result, ConsumerKey);

			return new FinishedAuthorizedModel {
				 Audience = tokenInfo.Audience,
				 User = tokenInfo.User,
				 AccessToken = auth.AccessToken
			};
		}

		private WebServerClient CreateWebServerClient() {
			var client = new WebServerClient(ServerDescription, ConsumerKey, ConsumerSecret);
			return client;
		}

        private TokenInfoModel ValidateToken(GetResult<TokenInfoModel> result, string expectedAudience) {
            if (!result.Success) {
                throw new HttpException("Could not finish authorisation: " + result.Error);
            }

            var tokenInfo = result.Content;

			if (String.IsNullOrEmpty(tokenInfo.Audience) || tokenInfo.Audience != expectedAudience) {
				throw new HttpException("token with unexpected audience: " + tokenInfo.Audience);
			}

            return tokenInfo;
		}
	}

	public class FinishedAuthorizedModel {
		public string Audience { get; set; }
		public string User { get; set; }
		public string AccessToken { get; set; }
	}

	public class TokenInfoModel {
		public string Audience { get; set; }
		public string User { get; set; }
	}
}