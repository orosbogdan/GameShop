using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameShop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int name)
        {
            if (Session["user"] != null)
            {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            return View(gameShopContext.Games.Single(x=> x.id==name));
            }
            else
            {
                return Redirect(@"/Home/Login/?url=/Order/?name=" + name);
            }
        }
    }
}