using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PU_CampusTour.Models;

namespace PU_CampusTour.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
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

        string url;
        public ActionResult Cat_Edit(int id)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();

            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                var q = from s in db.Categories
                        where s.Category_Id == id
                        select s;
                //Pro_Class product = new Pro_Class();

                foreach (var t in q)
                {
                    ViewBag.id = t.Category_Id;
                }
                return View();
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Cat_Edit(string i, FormCollection c)
        {
            url = i;
            string name = "";


           if(c["Name"]!="")
           {
            name = c["Name"];


            int id = Convert.ToInt32(c["id"]);
            using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
            {

                var q = from s in ctx.Categories
                        where s.Category_Id == id
                        select s;
                foreach (var t in q)
                {
                    t.Name = name;

                    t.Description = "detail";





                }
                ctx.SaveChanges();


            }
            return RedirectToAction("Index", "Category");

           }
           else
           {
               ViewBag.MSG = "*Required field is empty";
               return View();
           }
        }


        public ActionResult Cat_Add()
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
        public ActionResult Cat_Add(FormCollection c)
        {

            string name = "";

            if (c["Name"] != "")
            {

                // url = Request.Url[0];
                name = c["Name"];



                using (dbf522ec9140464818abf6a1c4013479aeEntities1 ctx = new dbf522ec9140464818abf6a1c4013479aeEntities1())
                {
                    Category p = new Category()
                    {
                        //p.Place_Id = 1;
                        Name = name,
                        Description = "detail",

                        //Category_Id = 1

                    };

                    ctx.Categories.Add(p);
                    ctx.SaveChanges();


                }
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ViewBag.MSG = "*Required field is empty";
                return View();
            }


        }

        public ActionResult Cat_Delete(int id)
        {
            dbf522ec9140464818abf6a1c4013479aeEntities1 db = new dbf522ec9140464818abf6a1c4013479aeEntities1();
            if (Request.Cookies["Login"].Value.ToLower() == "true")
            {
                var q = from s in db.Categories
                        where s.Category_Id == id
                        select s;
                foreach (var t in q)
                {
                    db.Categories.Remove(t);
                }
                db.SaveChanges();

                return RedirectToAction("Index", "Category");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

    }
}
