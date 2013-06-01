using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PU_CampusTour.Models;
namespace PU_CampusTour.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return RedirectToAction("location","Login");
        }
        public ActionResult Edit()
        {
            return View();
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
        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult login()
        {
            //ViewBag.Message = "";
            return View();
        }

        public ActionResult location()
        {


            ViewBag.Message = "Done";
            using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
            {
                // saving dummy values in database
                /* Place p = new Place()
                 {
                 //p.Place_Id = 1;
                 Name = "Department of Biotechnology",
                 Description = "The Department of Biotechnology started in 1962",
                 Longitude = "16.90",
                 Latitude = "1.078",
                 Image = "E:/images/DE.jpg",
                 //Category_Id = 1
                    
                 };

                 ctx.AddToPlaces(p);
                 ctx.SaveChanges();
                 */


                // returning data in place class to be viewed
                return View();
            }
        }

        
        public ActionResult DeleteAll(int[] deleteValues)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();

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
        [HttpPost]
        public ActionResult login(FormCollection c)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1();
            // adding dummy data
            
            /*Admin p = new Admin()
            {
                Username="Admin",
                Password="123"

            };

            ctx.AddToAdmins(p);
            ctx.SaveChanges();*/

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
                        return RedirectToAction("location");
                    }
                    else
                    {
                        ViewBag.Message = "Login Fails. Username and Password doesnot match";
                        return View("login");
                    }
                }
                 return View();
            }
            else
            {
                return RedirectToAction("Error");
            }
           
            
        }

    }
}
