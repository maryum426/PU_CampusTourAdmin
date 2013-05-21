using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PU_CampusTour.Models;

namespace PU_CampusTour.Controllers
{
    public class LocationController : Controller
    {
        //
        // GET: /Location/

        /*(public ActionResult location()
        {
            return View();
        }*/

        
       public ActionResult location()
        {
            
            
                ViewBag.Message = "Done";
                using (DatabaseEntities4 ctx = new DatabaseEntities4())
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
                    return View(ctx.Places.ToList());  
                }

                   
        }

        //
        // GET: /Add/

        public ActionResult Add()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Add(FormCollection c)
        {
            
            string name = "";
            string detail = "";
            string longi = "";
            string lat = "";
            string image = "E:/images/DE.jpg";

            // url = Request.Url[0];
            name = c["Name"];
            detail = c["Detail"];
            longi = c["Longitude"];
            lat = c["Latitude"];


            using (DatabaseEntities4 ctx = new DatabaseEntities4())
            {
                Place p = new Place()
                {
                    //p.Place_Id = 1;
                    Name = name,
                    Description = detail,
                    Longitude = longi,
                    Latitude = lat,
                    Image = image
                    //Category_Id = 1

                };

                ctx.AddToPlaces(p);
                ctx.SaveChanges();


            }
            return RedirectToAction("Location", "Login");


        }

        //
        // GET: /Edit/

        string url;
        public ActionResult Edit(int id)
        {
            DatabaseEntities4 db = new DatabaseEntities4();
            var q = from s in db.Places
                    where s.Place_Id == id
                    select s;
            //Pro_Class product = new Pro_Class();
            
            foreach (var t in q)
            {
                ViewBag.id = t.Place_Id;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string i, FormCollection c)
        {
            url = i;
            string name = "";
            string detail = "";
            string longi = "";
            string lat = "";
            string image = "default";

            // url = Request.Url[0];
            name = c["Name"];
            detail = c["Detail"];
            longi = c["Longitude"];
            lat = c["Latitude"];

            int id = Convert.ToInt32(c["id"]);
            using (DatabaseEntities4 ctx = new DatabaseEntities4())
            {

                var q = from s in ctx.Places
                        where s.Place_Id == id
                        select s;
                foreach (var t in q)
                {
                    t.Name = name;
                   
                    t.Description = detail;
                    
                    t.Longitude = longi;
                   
                    t.Latitude = lat;
                   
                    t.Image = image;
                    


                }
                ctx.SaveChanges();


            }
            return RedirectToAction("location","Login");


        }

        public ActionResult Delete(int id)
        {
            DatabaseEntities4 db = new DatabaseEntities4();
            var q = from s in db.Places
                    where s.Place_Id == id
                    select s;
            foreach (var t in q)
            {
                db.Places.DeleteObject(t);
            }
            db.SaveChanges();

            return RedirectToAction("location", "Login");
        }

       

        
        public ActionResult DeleteAll(int[] deleteValues)
        {
            DatabaseEntities4 db = new DatabaseEntities4();
            
            int[] MyCheckboxes = deleteValues;

            
            foreach (var item in MyCheckboxes)
            {
                var q = from s in db.Places
                        where s.Place_Id == Convert.ToInt32(item)
                        select s;
                foreach (var t in q)
                {
                    db.Places.DeleteObject(t);
                }
                db.SaveChanges();
            }

            return RedirectToAction("location", "Login");
        }
    }
}
