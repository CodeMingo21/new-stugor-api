//////using Microsoft.AspNetCore.Authorization;
//////using Microsoft.AspNetCore.Mvc;
//////using Microsoft.Extensions.Configuration;
//////using static System.Net.WebRequestMethods;

//////[Route("auth")]
//////public class AuthController : Controller
//////{
//////    private readonly IConfiguration _config;

//////    public AuthController(IConfiguration config)
//////    {
//////        _config = config;
//////    }

//////    [AllowAnonymous]
//////    [HttpGet("login")]
//////    public IActionResult Login()
//////    {
//////        var domain = "https://dev-3pst2842uniw8kh8.eu.auth0.com";
//////        var clientId = _config["Auth0:ClientId"];
//////        var redirectUri = "https://localhost:44368/auth/callback"; // make sure this matches your callback URL in Auth0 settings!

//////        var loginUrl = $"{domain}/authorize?" +
//////                       $"response_type=code&" +
//////                       $"client_id={clientId}&" +
//////                       $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
//////                       $"scope=openid profile email";

//////        return Redirect(loginUrl);
//////    }

//////    [AllowAnonymous]
//////    [HttpGet("callback")]
//////    public async Task<IActionResult> Callback(string code)
//////    {
//////        var domain = "dev-3pst2842uniw8kh8.eu.auth0.com"; // ❗ no https:// here
//////        var clientId = _config["Auth0:ClientId"];
//////        var clientSecret = _config["Auth0:ClientSecret"];
//////        var redirectUri = "https://localhost:44368/auth/callback";

//////        var client = new HttpClient();
//////        var tokenResponse = await client.PostAsync($"https://{domain}/oauth/token",
//////            new FormUrlEncodedContent(new Dictionary<string, string>
//////            {
//////                { "grant_type", "authorization_code" },
//////                { "client_id", clientId },
//////                { "client_secret", clientSecret },
//////                { "code", code },
//////                { "redirect_uri", redirectUri }
//////            }));

//////        var json = await tokenResponse.Content.ReadAsStringAsync();

//////        // ✅ Error handling
//////        if (!tokenResponse.IsSuccessStatusCode)
//////        {
//////            return Content($"Error: {json}", "text/plain");
//////        }
//////            //return BadRequest($"Token exchange failed: {json}");

//////        var tokenObj = System.Text.Json.JsonDocument.Parse(json);
//////        var accessToken = tokenObj.RootElement.GetProperty("access_token").GetString();

//////        // 🔐 Set a secure auth cookie
//////        Response.Cookies.Append("access_token", accessToken, new CookieOptions
//////        {
//////            HttpOnly = true,
//////            Secure = true,
//////            SameSite = SameSiteMode.Strict
//////        });

//////        return Redirect("https://localhost:44368"); // ✅ Your live UI site
//////    }

//////    [Authorize]
//////    [HttpGet("me")]
//////    public async Task<IActionResult> Me()
//////    {
//////        if (!Request.Cookies.TryGetValue("access_token", out var accessToken))
//////            return Unauthorized("No access token found.");

//////        var domain = _config["Auth0:Domain"];
//////        var client = new HttpClient();
//////        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

//////        var response = await client.GetAsync($"https://{domain}/userinfo");
//////        if (!response.IsSuccessStatusCode)
//////            return Unauthorized("Invalid token or session expired.");

//////        var userJson = await response.Content.ReadAsStringAsync();
//////        return Content(userJson, "application/json");
//////    }

//////    [AllowAnonymous]
//////    [HttpGet("logout")]
//////    public IActionResult Logout()
//////    {
//////        var domain = "dev-3pst2842uniw8kh8.eu.auth0.com"; // ❗ No https://
//////        var clientId = _config["Auth0:ClientId"];
//////        var returnTo = "https://localhost:44368"; 

//////        var logoutUrl = $"https://{domain}/v2/logout?" +
//////                        $"client_id={clientId}&" +
//////                        $"returnTo={Uri.EscapeDataString(returnTo)}";

//////        return Redirect(logoutUrl);
//////    }
//////}


////// AuthController.cs
////using Microsoft.AspNetCore.Authentication;
////using Microsoft.AspNetCore.Authentication.Cookies;
////using Microsoft.AspNetCore.Authorization;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.Extensions.Configuration;

////[Route("auth")]
////public class AuthController : Controller
////{
////    private readonly IConfiguration _config;

////    public AuthController(IConfiguration config)
////    {
////        _config = config;
////    }

////    //[AllowAnonymous]
////    //[HttpGet("login")]
////    //public IActionResult Login()
////    //{
////    //    var domain = "https://dev-3pst2842uniw8kh8.eu.auth0.com";
////    //    var clientId = _config["Auth0:ClientId"];
////    //    var redirectUri = _config["Auth0:RedirectUri"];

////    //    var loginUrl = $"{domain}/authorize?" +
////    //                   $"response_type=code&" +
////    //                   $"client_id={clientId}&" +
////    //                   $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
////    //                   $"scope=openid profile email";

////    //    return Redirect(loginUrl);
////    //}

////    [Route("login")]
////    public IActionResult Login(string returnUrl = "/")
////    {
////        var redirectUrl = Url.Action("Callback", "Account", new { returnUrl });
////        return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, "Auth0");
////    }

////    [Route("logout")]
////    public async Task<IActionResult> Logout()
////    {
////        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
////        await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
////        {
////            RedirectUri = "https://localhost:5001/"
////        });
////        return Redirect("/");
////    }


////    [AllowAnonymous]
////    [HttpGet("callback")]
////    public async Task<IActionResult> Callback(string code)
////    {
////        var domain = "dev-3pst2842uniw8kh8.eu.auth0.com";
////        var clientId = _config["Auth0:ClientId"];
////        var clientSecret = _config["Auth0:ClientSecret"];
////        var redirectUri = _config["Auth0:RedirectUri"];

////        var client = new HttpClient();
////        var tokenResponse = await client.PostAsync($"https://{domain}/oauth/token",
////            new FormUrlEncodedContent(new Dictionary<string, string>
////            {
////                { "grant_type", "authorization_code" },
////                { "client_id", clientId },
////                { "client_secret", clientSecret },
////                { "code", code },
////                { "redirect_uri", redirectUri }
////            }));

////        var json = await tokenResponse.Content.ReadAsStringAsync();

////        if (!tokenResponse.IsSuccessStatusCode)
////            return Content($"Error: {json}", "text/plain");

////        var tokenObj = System.Text.Json.JsonDocument.Parse(json);
////        var accessToken = tokenObj.RootElement.GetProperty("access_token").GetString();

////        Response.Cookies.Append("access_token", accessToken, new CookieOptions
////        {
////            HttpOnly = true,
////            Secure = true,
////            SameSite = SameSiteMode.Strict
////        });

////        return Redirect(_config["Auth0:PostLogoutRedirectUri"]);
////    }

////    [Authorize]
////    [HttpGet("me")]
////    public async Task<IActionResult> Me()
////    {
////        if (!Request.Cookies.TryGetValue("access_token", out var accessToken))
////            return Unauthorized("No access token found.");

////        var domain = _config["Auth0:Domain"];
////        var client = new HttpClient();
////        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

////        var response = await client.GetAsync($"https://{domain}/userinfo");
////        if (!response.IsSuccessStatusCode)
////            return Unauthorized("Invalid token or session expired.");

////        var userJson = await response.Content.ReadAsStringAsync();
////        return Content(userJson, "application/json");
////    }

////    //[AllowAnonymous]
////    //[HttpGet("logout")]
////    //public IActionResult Logout()
////    //{
////    //    var domain = "dev-3pst2842uniw8kh8.eu.auth0.com";
////    //    var clientId = _config["Auth0:ClientId"];
////    //    var returnTo = _config["Auth0:PostLogoutRedirectUri"];

////    //    var logoutUrl = $"https://{domain}/v2/logout?" +
////    //                    $"client_id={clientId}&" +
////    //                    $"returnTo={Uri.EscapeDataString(returnTo)}";

////    //    return Redirect(logoutUrl);
////    //}
////}
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;

//[Route("auth")]
//public class AuthController : Controller
//{
//    [HttpGet("login")]
//    public IActionResult Login(string returnUrl = "/")
//    {
//        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Auth0");
//    }

//    [HttpGet("logout")]
//    public async Task<IActionResult> Logout()
//    {
//        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
//        await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
//        {
//            RedirectUri = "https://localhost:44368"
//        });
//        return Redirect("/");
//    }
//}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StugorAPI.Controllers
{
    [Authorize]
    [Route("auth")]
    public class AuthController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, "Auth0");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                RedirectUri = "https://localhost:44368" 
            });

            return Redirect("/");
        }
    }
}
