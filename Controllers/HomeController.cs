using firsaty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace firsaty.Controllers
{
    public class HomeController : Controller
    {
        private firsatyEntities db = new firsatyEntities();

        public ActionResult Index()
        {
         
            return View();
        }


        public ActionResult Sliders()
        {
            return View(db.Silders.OrderByDescending(e => e.ID).ToList());
        }
        public ActionResult header()
        {
            return PartialView(db.Categories.ToList());
        }
        public ActionResult HomePromo()
        {
            Promotion pr = db.Promotions.Where(e => e.Name == "Home").FirstOrDefault();
            return PartialView(pr);
        
        }
        public ActionResult Mobileheader()
        {
            return PartialView(db.Categories.ToList());
        }

        public ActionResult HomeProducts()
        {
            var r = new Random();
            var n = db.Categories.ToList().OrderBy(u => r.Next()).Take(1);
            Category m = n.Single();
            return PartialView(m);
        }
        public ActionResult ProductsBySub(int? id)
        {
            return PartialView(db.Products.OrderByDescending(e => e.ID).ToList().Where(e => e.CategoryID == id.ToString()).Take(6));
        }


        public ActionResult proshur()
        {
            return PartialView(db.PDFs.OrderByDescending(e => e.ID).ToList().Take(6));
        }
        public ActionResult categories()
        {
            return PartialView(db.Categories.OrderByDescending(e => e.ID).ToList().Take(4));
        }
        public ActionResult PDF(int? id)
        {
            PDF pdf = db.PDFs.Find(id);
            return View(pdf);
        }

        public ActionResult category(int? page, int? id)
        {
            // var users = db.Products.Where(a => a.CategoryID == id.ToString()).Where(e => e.Title.StartsWith(search) || search == null).OrderByDescending(e => e.ID);
            var products = db.Products.Where(a => a.CategoryID == id.ToString()).OrderByDescending(e => e.ID);
            ViewBag.cateName = db.Categories.Find(id).Name;
            return View(products.ToList().ToPagedList(page ?? 1, 42));

        }
        public ActionResult Subcategory(int? page, int? id)
        {
            // var users = db.Products.Where(a => a.CategoryID == id.ToString()).Where(e => e.Title.StartsWith(search) || search == null).OrderByDescending(e => e.ID);
            var products = db.Products.Where(a => a.SubID == id.ToString()).OrderByDescending(e => e.ID);
            ViewBag.cateName = db.SubCategories.Find(id).Name;
            return View(products.ToList().ToPagedList(page ?? 1, 36));

        }
        public ActionResult search(string search)
        {
            string[] arry = search.Split(' ');
            string key = arry[0].ToString();
            var products = db.Products.Where(e => e.Title.Contains(key) || search == null).OrderByDescending(e => e.ID);
            foreach (string item in arry)
            {
            products = products.Where(e => e.Title.Contains(item)).OrderByDescending(e => e.ID);
            }
           

            return View(products.ToList());

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult subscribe(NewMail newMail)
        {

            if (ModelState.IsValid)
            {
                db.NewMails.Add(newMail);
                db.SaveChanges();

                ViewBag.added = "شكرا على اهتمامك سيتم ارسال اخر الاخبار الى حسابك";
                return View("Index");

            }
            return RedirectToAction("Index");

        }


        public ActionResult LastAddedProducts()
        {
            return PartialView(db.Products.OrderByDescending(e=>e.ID).ToList().Take(6));
        }   
        public ActionResult Partners()
        {
            return PartialView(db.Partners.OrderByDescending(e=>e.ID).ToList());
        }

        public ActionResult Promostions()
        {
            return View(db.Promotions.ToList().Where(e=>e.Name=="442-310"));
        }

        public ActionResult Product(int? id)
        {
            Product pd = db.Products.Find(id);
            Category cate = db.Categories.Find(int.Parse(pd.CategoryID));
            SubCategory sub = db.SubCategories.Find(int.Parse(pd.SubID));
            ViewBag.cateName = cate.Name ;
            ViewBag.cateID = cate.ID ;
            ViewBag.SubName = sub.Name;
            ViewBag.SubID = sub.ID;

            return View(pd);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(NewMail newMail)
        {
            if (ModelState.IsValid)
            {
                db.NewMails.Add(newMail);
                db.SaveChanges();

                ViewBag.added = "شكرا على اهتمامك سيتم التواصل معك باقرب وقت ";
                return View();

            }
            return View();
        }
    }
}