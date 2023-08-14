using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Helpers;
using Secret_Santa_MVC.Service.GoogleOuth;
using System;

namespace Secret_Santa_MVC.Controllers
{
    public class GoogleAuthController : Controller
    {
        public string redirectUrl = "https://localhost:7129/GoogleAuth/Code";
        private const string YouTubeScope = "https://www.googleapis.com/auth/youtube";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RedirectOnOuthServer()
        {   

            var codeVerifier = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("codeVerifier", codeVerifier);

            var codeChellange = Sha256Helper.ComputeHash(codeVerifier);


            var url = GoogleAuthService.GenerateAoutRequstUrl(YouTubeScope,redirectUrl,
                codeChellange);

            return Redirect(url);
        }
        [HttpGet]
        public async Task<IActionResult> CodeAsync(string code)
        { 
            string codeVerifier = HttpContext.Session.GetString("codeVerifier");
            
        
            var tokenResult = await GoogleAuthService.ExchangeCodeOnToken(code,codeVerifier,
                redirectUrl);
            //string NewDescription = "Heh"; 
            //var myChannelId = YoutubeService.GetMyChannelId(tokenResult.AccesToken).Result;
            //YoutubeService.UpdateChanelDescription(tokenResult.AccesToken, myChannelId,NewDescription);
        // чекаємо 3600 секунд
          var refreshedTokenResult = await GoogleAuthService.RefreshTokenAsync
               (tokenResult.RefreshToken);

            return Ok();
        }
        
    }
}
