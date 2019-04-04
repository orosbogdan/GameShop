using GameShop.Utility;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace GameShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
         
            return View();
        }
        [HttpPost]
        public void Comment(string comment, int game_id)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            if(Session["user"]!=null)
            {
                var comm = new Models.Comment();
                comm.game_id = game_id;
                string username = Session["user"].ToString();
                comm.user_id =  gameShopContext.Users.Single( x=> x.username== username).id;
                comm.comment1 = comment;
                gameShopContext.Comments.Add(comm);
                gameShopContext.SaveChanges();

            }

        }

        [HttpPost]
        public void Rate(short rating, int game_id)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            if (Session["user"] != null)
            {
                var rate = new Models.Rating();
                string username = Session["user"].ToString();
                rate.game_id = game_id;
                rate.user_id = gameShopContext.Users.Single(x => x.username == username ).id;
                rate.rating1 = rating;
                gameShopContext.Ratings.Add(rate);
                gameShopContext.SaveChanges();

            }

        }

        public ActionResult GameDetails(int id)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();

            return View(gameShopContext.Games.Single(x => x.id==id));
        }

        public ActionResult Library()
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            string username= Session["user"].ToString();
            long userid = gameShopContext.Users.Single(x => x.username == username).id;

            return View( gameShopContext.Transactions.Where(x=> x.user_id==userid  ).ToList());
        }

        

        public ActionResult Buy(int name)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            Models.Transaction trans = new Models.Transaction();
            Models.User userr = new Models.User();

            if (Session["user"]!=null)
            {
             
                string user = Session["user"].ToString();
                
                if(gameShopContext.Discounts.Any(x => x.game_id == name))
                    if (gameShopContext.Users.Single(x => x.username == user).balance > (gameShopContext.Games.Single(x => x.id == name).price - gameShopContext.Discounts.Single(x => x.game_id == name).percentage_discount / 100 * gameShopContext.Games.Single(x => x.id == name).price))
                    {
                        userr = gameShopContext.Users.Single(x => x.username == user);
                        userr.balance = userr.balance - ((gameShopContext.Games.Single(x => x.id == name).price - gameShopContext.Discounts.Single(x => x.game_id == name).percentage_discount / 100 * gameShopContext.Games.Single(x => x.id == name).price));
                        gameShopContext.Users.Attach(userr);
                        gameShopContext.SaveChanges();

                        trans.date = DateTime.Now;
                        trans.game_id = name;
                        trans.price = ((gameShopContext.Games.Single(x => x.id == name).price - gameShopContext.Discounts.Single(x => x.game_id == name).percentage_discount / 100 * gameShopContext.Games.Single(x => x.id == name).price));

                        trans.user_id = gameShopContext.Users.Single(x => x.username == user).id;


                        gameShopContext.Transactions.Add(trans);
                        gameShopContext.SaveChanges();
                        return RedirectToAction("Shop", "Home");
                    }
                    else
                    {
                        return View("Error");
                    }
                else
                     if (gameShopContext.Users.Single(x => x.username == user).balance > gameShopContext.Games.Single(x => x.id == name).price )
                {
                    userr = gameShopContext.Users.Single(x => x.username == user);
                    userr.balance = userr.balance - gameShopContext.Games.Single(x => x.id == name).price ;
                    gameShopContext.Users.Attach(userr);
                    gameShopContext.SaveChanges();

                    trans.date = DateTime.Now;
                    trans.game_id = name;
                    trans.price = gameShopContext.Games.Single(x => x.id == name).price;
                    trans.user_id = gameShopContext.Users.Single(x => x.username == user).id;


                    gameShopContext.Transactions.Add(trans);
                    gameShopContext.SaveChanges();
                    return RedirectToAction("Shop", "Home");
                }
                else
                {
                    return View("Error");
                }

            }
            else
            {
                return Redirect(@"/Home/Login/?url=/Order/?name="+name);
            }

        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        public JsonResult IsUserExists(string username)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();

            return Json(!gameShopContext.Users.Any(x => x.username == username),JsonRequestBehavior.AllowGet);


        }

        public ActionResult Genre(string name)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();


            return View(gameShopContext.Games.Where(x => x.Genres.Any(u => u.name == name)).ToList());

        }
        public ActionResult Shop()
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            return View(gameShopContext.Games.ToList());
        }

        public ActionResult Login(string url)
        {
            return View((object)url);
        }

       

        public ActionResult LoginResult(string data)
        {
            return View(data);
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string url)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();

            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()]).{8,}$");
            var hashedPassword = Hasher.CreateMD5(password);


            if (regex.IsMatch(password) && (gameShopContext.Users.Any(x => x.username == username && x.password == hashedPassword)))
            {
                Session["user"] = username; return Redirect(url);
            }
            else
                return RedirectToAction("LoginResult","Home","Login has failed!");
         
        } 

        [HttpPost]
        public ActionResult Register(string username, string password, string email)
        {
            Models.GameShopContext gameShopContext = new Models.GameShopContext();
            Models.User user = new Models.User();
            user.balance = 0;
            user.email = email;
            user.username = username;
            user.password = Hasher.CreateMD5(password);
         
     
            try
            {

                gameShopContext.Users.Add(user);
                gameShopContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
         
        }

    }
}