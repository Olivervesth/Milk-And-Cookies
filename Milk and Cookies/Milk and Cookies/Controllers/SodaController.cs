using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace sessions_og_cookies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SodaController : ControllerBase
    {

        [HttpGet]
        [Route("{item}")]
        public string Get(string item )
        {
            DateTimeOffset dt = DateTimeOffset.Now.AddMinutes(5); // create a datetime object so we can add 5 min to the cookie lifespand
            CookieOptions co = new CookieOptions();
            co.Expires = dt;
            Response.Cookies.Append("favoriteSoda", item,co);
            //Response.Cookies.Append("favoriteSoda", item, co);
            return "fav: " + item;
        }

        [HttpGet]
        [Route("favoriteSoda")]
        public string GetCookie()
        {
            return Request.Cookies["favoriteSoda"];
        }

        [HttpGet]
        [Route("setcookie/{type}")]
        public void SetMyCookie(string type)
        {
            Response.Cookies.Append("favoriteCookie", type);//make tha cookie
        }

        [HttpGet]
        [Route("getcookie")]
        public string GetMyCookie()
        {
            return Request.Cookies["favoriteCookie"];//Eat the cookie og get it at least
        }

    }
}
