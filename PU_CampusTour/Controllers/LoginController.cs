using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PU_CampusTour.Models;

namespace PU_CampusTour.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                return RedirectToAction("location", "Login");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }
        public ActionResult Edit()
        {
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Edit(FormCollection c)
        {
            string name = "";
            string detail = "";
            string longi="";
            string lat = "";
            string image = "default";

            name = c["Name"];
            detail=c["Detail"];
            longi = c["Longitude"];
            lat = c["Latitude"];

            if (lat == String.Empty || longi == String.Empty || detail == String.Empty || name == String.Empty)
            {
                return View("Edit");
            }
        
            using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
            {
                var q = from s in ctx.Places where s.Name == name select s;

                foreach (var t in q)
                {
                    t.Name = name;
                    ctx.SaveChanges();
                    t.Description = detail;
                    ctx.SaveChanges();
                    t.Longitude = longi;
                    ctx.SaveChanges();
                    t.Latitude = lat;
                    ctx.SaveChanges();
                    t.Image = image;
                    ctx.SaveChanges();
                }
                
                 
            }
            return RedirectToAction("location");
            
            
        }
        public ActionResult Category_Display()
        {
            

                return View();
           
        }

        // to display selected category items
        [HttpPost]
        public ActionResult Category_Display(FormCollection c)
        {
            

            if (Request["Category"] != "All")
            {
                int id = Convert.ToInt32(Request["Category"]);
                if (Request.Cookies["Login"].Value.ToLower() == "true")
                {
                    dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();

                    ViewBag.id = id;

                    return View();
                }
                else
                {
                    return RedirectToAction("login", "Login");
                }
            }
            else
            {
                return RedirectToAction("location", "Login");
            }

        }
        public ActionResult Success()
        {
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        public ActionResult Error()
        {
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                return View();
            }
            else
            {
                return RedirectToAction("login","Login");
            }
        }

        public ActionResult login()
        {
            //ViewBag.Message = "";
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
            }
            else
            {

                Response.Cookies["Login"].Value = false.ToString();
            }


            MvcApplication.login = false;
            return View();
        }

        public ActionResult logout()
        {
            //ViewBag.Message = "";
            Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
            MvcApplication.login = false;
            return View();
        }

        public ActionResult location()
        {
            if (Request.Cookies["Login"].Value.ToLower()=="true")
            {

                ViewBag.Message = "Done";
                using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
                {
                   

                    // returning data in place class to be viewed
                    return View();
                }
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        
        public ActionResult DeleteAll(int[] deleteValues)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                int[] MyCheckboxes = deleteValues;


                foreach (var item in MyCheckboxes)
                {
                    var q = from s in db.Places
                            where s.Id == Convert.ToInt32(item)
                            select s;
                    foreach (var t in q)
                    {
                        db.Places.Remove(t);
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("location", "Login");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }
        [HttpPost]
        public ActionResult login(FormCollection c)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1();
           

            string username = "";
            string password = "";

            
            username = c["Username"];
            password = c["Password"];
            
            var q = from s in ctx.Admins
                    select new
                    {
                        s.Username,
                        s.Password
                    };
            if (c["Username"] != null && c["Password"] != null)
            {
                foreach (var t in q)
                {
                    if (t.Username == c["Username"] && t.Password == c["Password"])
                    {
                        //ViewBag.Message = "";
                        MvcApplication.login = true;
                        Response.Cookies["Login"].Value = true.ToString();
                      //return Request.Cookies["Login"].Value.ToString();
                       //return View();
                        return RedirectToAction("location");

                        
                    }
                    else
                    {
                        ViewBag.Message = "Login Fails. Username and Password doesnot match";
                        //return ("ok");
                        return View("login");
                    }
                }
                return View();
               // return ("ok");
            }
            else
            {
               //return ("ok");
               return RedirectToAction("Error");
            }
           
            
        }

    }
}
