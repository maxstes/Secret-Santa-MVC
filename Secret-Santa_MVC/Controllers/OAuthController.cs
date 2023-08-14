//using Microsoft.AspNetCore.Mvc;
//using OAuth2;
//using OAuth2.Client.Impl;
//using OAuth2.Infrastructure;
//using Secret_Santa_MVC.Email;
//using Secret_Santa_MVC.Service;

//namespace Secret_Santa_MVC.Controllers
//{
//    public class OAuthController : Controller
//    {
//        private readonly OAuthClass auth = OAuthTool.GetOAuth().Result;
//        public string redirectUrl = "https://localhost:7129/GoogleAuth/Code";
//        public IActionResult Index()
//            {
//                return View();
//            }

//            public ActionResult GoogleLogin()
//            {

//               // var redirectUri = new Uri(Url.Action("GoogleLoginCallBack", "Account", null, protocol: Request.Url.Scheme));
//                var googleClient = new GoogleClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
//                {
//                    ClientId = auth.ClientId?.Trim(),
//                    ClientSecret = auth.ClientSecret?.Trim(),
//                    RedirectUri = redirectUrl,
//                    Scope = "profile email"
//                });
//                return Redirect(googleClient.GetLoginLinkUri("SomeStateValueYouWantToUse"));
//            }
//    }
//}
