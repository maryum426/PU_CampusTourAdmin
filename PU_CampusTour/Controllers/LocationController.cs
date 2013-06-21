using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.HttpRequestBase;
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

            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                ViewBag.Message = "Done";
                using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
                {


                    // returning data in place class to be viewed
                    return View(ctx.Places.ToList());
                }
            }
            else
            {
                return RedirectToAction("login","Login");
            }

                   
        }

       public ActionResult Requestplace()
       {
           if (Request.Cookies["Login"].Value.ToLower() == "true")
           {

               ViewBag.Message = "Done";
               using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
               {



                   // returning data in place class to be viewed
                   return View(ctx.Places.ToList());
               }
           }
           else
           {
               return RedirectToAction("login","Login");
           }
       }

        //
        // GET: /Add/

        public ActionResult Add(int id)
        {
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                if (id == 0)
                {
                    return View();
                }
                else
                {
                    ViewBag.id = id;
                    return View();
                }
            }
            else
            {
                return RedirectToAction("login","Login");
            }
        }



        [HttpPost]
        public ActionResult Add(FormCollection c)
        {
            
            string name = "";
            string detail = "";
            string longi = "";
            string lat = "";
            string image = "E:/images/DE.jpg";
            int category = 0;

            // url = Request.Url[0];
            

            if (c["Name"] != "" && c["Detail"] != "" && c["Longitude"] != "" && c["Latitude"] != "" && c["Category"] != "")
            {
                name = c["Name"];
                detail = c["Detail"];
                longi = c["Longitude"];
                lat = c["Latitude"];
                category = Convert.ToInt32(c["Category"]);

                using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
                {
                    Place p = new Place()
                    {
                        //p.Place_Id = 1;
                        Name = name,
                        Description = detail,
                        Longitude = longi,
                        Latitude = lat,
                        Image = image,
                        isApproved = "True",
                        Category_Id = category

                        //Category_Id = 1

                    };

                    ctx.Places.Add(p);
                    ctx.SaveChanges();


                }
                return RedirectToAction("Location", "Login");
            }
            else
            {
                ViewBag.MSG = "*Required fields are empty";
                ViewBag.a1 = c["Name"];
                ViewBag.a2 = c["Detail"];
                ViewBag.a3 = c["Longitude"];
                ViewBag.a4 = c["Latitude"];
                ViewBag.a5= c["Category"];
                return View();
            }


        }

        //
        // GET: /Edit/

        string url;
        public ActionResult Edit(int id)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();

            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                var q = from s in db.Places
                        where s.Id == id
                        select s;
                //Pro_Class product = new Pro_Class();

                foreach (var t in q)
                {
                    ViewBag.id = t.Id;
                }
                return View();
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
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
            int category = 0;

            // url = Request.Url[0];


            if (c["Name"] != "" && c["Detail"] != "" && c["Longitude"] != "" && c["Latitude"] != "" && c["Category"] != "" && c["id"] != "")
            {
                name = c["Name"];
                detail = c["Detail"];
                longi = c["Longitude"];
                lat = c["Latitude"];
                category = Convert.ToInt32(c["Category"]);

                int id = Convert.ToInt32(c["id"]);

                using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
                {

                    var q = from s in ctx.Places
                            where s.Id == id
                            select s;
                    foreach (var t in q)
                    {
                        t.Name = name;

                        t.Description = detail;

                        t.Longitude = longi;

                        t.Latitude = lat;

                        t.Image = image;

                        t.Category_Id = category;

                        t.isApproved = "True";


                    }
                    ctx.SaveChanges();


                }
                return RedirectToAction("location", "Login");
            }
            else
            {
                ViewBag.MSG = "*Required fields are empty";
                return View();
            }


        }

        public ActionResult Delete(int id)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                var q = from s in db.Places
                        where s.Id == id
                        select s;
                foreach (var t in q)
                {
                    db.Places.Remove(t);
                }
                db.SaveChanges();

                return RedirectToAction("location", "Login");
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



        public ActionResult Change_Password()
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
        public ActionResult Change_Password(FormCollection c)
        {
            string currpass = c["CP"];
            string newpass=c["NP"];
            string conpass = c["ConP"];
            


          if(currpass!="" && newpass!="" && conpass!="")
          {
            if (newpass == conpass)
            {
                using (dbf522ec9140464818abf6a1c4013479aeEntities1 db1 = new dbf522ec9140464818abf6a1c4013479aeEntities1())
                {
                    var q1 = from s1 in db1.Admins
                             where s1.Password == currpass
                             select s1;

                    if (q1.Count()!=0)
                    {
                        foreach (var t1 in q1)
                        {
                            t1.Password = newpass;


                        }
                        db1.SaveChanges();


                        ViewBag.msg = "Password has been updated successfully";

                        return View();
                    }
                    else
                    {
                        ViewBag.msg = "*Incorrect current password";
                        ViewBag.n1 = currpass;
                        ViewBag.n2 = newpass;
                        ViewBag.n3 = conpass;
                        return View();
                    }
                }
            
            }
            else
            {
                ViewBag.msg = "*New and Confirm Password doesnot match";
                ViewBag.n1 = currpass;
                ViewBag.n2 = newpass;
                ViewBag.n3 = conpass;
                return View();

            }

              
          }
           else
          {
              ViewBag.msg = "*Required fields are empty";
              ViewBag.n1 = currpass;
              ViewBag.n2 = newpass;
              ViewBag.n3 = conpass;
              return View();
          }
                

            
        }
    }
}
