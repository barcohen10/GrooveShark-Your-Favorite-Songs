using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FavoriteSongs.Models;

namespace FavoriteSongs.Controllers
{
    public class UserController : Controller
    {
        private const string FavoriteSongsUrl = "user/UserFavoriteSongs?sessionId={0}";

        public ActionResult Index()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Index(User user1)
        {
            if(user1.UserName==null||user1.Password==null)
            {
                ViewBag.Login = "Empty";
                return View("Index"); // login failed,try again
            }

            var sessionId = Grooveshark.StartSession();
            Dictionary<string, object> authRes = Grooveshark.Authenticate(sessionId, user1);

            if (!(String.IsNullOrEmpty(authRes["Email"].ToString()))) // succesful login
            {
                ViewBag.sessionId = sessionId;
                ViewBag.Login = "Success";
                ViewBag.UserName = authRes["Email"].ToString();
                ViewBag.FavoriteSongsUrl = String.Format(FavoriteSongsUrl, sessionId);
                return View("Data");

            }
            else
            {
                ViewBag.Login = "Failed";
                return View("Index"); // login failed,try again
            }
        }

        [HttpGet]
        public ActionResult UserFavoriteSongs(string sessionId)
        {
            var favoriteSongs = Grooveshark.GetUserFavoriteSongs(sessionId);

            return Json(favoriteSongs["songs"], JsonRequestBehavior.AllowGet);
        }
	}
}