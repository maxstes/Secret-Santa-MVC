using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Secret_Santa_MVC.Helpers;
using static System.Net.WebRequestMethods;

namespace Secret_Santa_MVC.Service.GoogleOuth
{
    public static class GoogleAuthService
    {
        private const string clientId = "1089473892024-apcai5mg93vr588pc3mfpnhdi424u6ct.apps.googleusercontent.com";
        private const string clientSecret = "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";

        public static string GenerateAoutRequstUrl(string scope, string redirectUrl,
            string codeChellange)
        {
            var oAuthServerEndPoint = "https://accounts.google.com/o/oauth2/v2/auth";

            var queryParams = new Dictionary<string, string>
            {
                {"client_id",clientId },
                {"redirect_uri" , redirectUrl },
                {"response_type","code" },
                {"scope",scope },
                {"code_challenge",codeChellange },
                { "code_challenge_method", "S256" },
                { "access_type", "offline" }
            };

            var url = QueryHelpers.AddQueryString(oAuthServerEndPoint, queryParams);
            return url;
        }

        public static async Task<TokenResult> ExchangeCodeOnToken(string code,string codeVerifier,
            string redirectUrl)
        {
            var tokenEndpoint = "https://oauth2.googleapis.com/token";

            var AuthParams = new Dictionary<string, string>
            {
                { "client_id", clientId},
                {"client_secret",clientSecret } ,
               {"code",code },
                {"code_verifier",codeVerifier },
                {"grant_type","authorization_code" },
                {"redirect_uri",redirectUrl }
            };

           var tokenResult = await HttpClientHelper.SendPostRequest
                <TokenResult>(tokenEndpoint, AuthParams);
            return tokenResult;
        }
        public static async Task<TokenResult> RefreshTokenAsync(string refreshToken)
        {
            string RefreshEndPoint = "https://oauth2.googleapis.com/token";
                var RefreshParams = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                 { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
    };
                        var tokenResult = await HttpClientHelper.SendPostRequest
                           <TokenResult>(RefreshEndPoint, RefreshParams);
                return tokenResult;
            
        }
    }
   
}
