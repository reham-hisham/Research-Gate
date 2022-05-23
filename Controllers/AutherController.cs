using IAproject.Models;
using IAproject.ViewModel;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace IAproject.Controllers
{
    public class AutherController : Controller
    {
        // GET: Auther

        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".PNG" };
        // GET: Auther
        // accsess database 
        DefaultConnection db = new DefaultConnection();
        private readonly  UserManager<myIdentityUser> userManager;
        public AutherController()
        {
            var dbs = new AutherIdentity();
            var userStore = new UserStore<myIdentityUser>(dbs);
            userManager = new UserManager<myIdentityUser>(userStore);
        }
        public ActionResult Index()
        {
            return View();
        }
        public bool verifyPassword(String realPassword , String EnteredPassword)
        {
          return  Crypto.VerifyHashedPassword(realPassword, EnteredPassword);

        }
    
    public Auther AddId(Auther auther)
    {
        Guid id = Guid.NewGuid();

        auther.Id = id;

        return auther;
    }
    public Auther hashPassword(Auther auther)
    {

        var hashPassword = Crypto.HashPassword(auther.Password);
        auther.Password = hashPassword.ToString();
        return auther;
    }
    public Boolean IsEmailFound(Auther auther)
    {
        try
        {
            var x = db.auther.Where(y => y.Email == auther.Email);
            if (x.Count() == 0)
            {
                return false;
            }
            return true;
        }
        catch
        {
            return false;

        }



    }
    public ActionResult DataDetailes()
        {



            var data = db.auther.ToList();

            return View(data);
        }
        public ActionResult UploadProfileImage(Auther auther, HttpPostedFileBase upload)
        {


            //Method 2 Get file details from HttpPostedFileBase class    


            if (ImageExtensions.Contains(Path.GetExtension(upload.FileName).ToUpperInvariant()))
            {
               
                   String Extintion = Path.GetExtension(upload.FileName);
                String path = Path.Combine(Server.MapPath(("~/Upload/profileImages")), auther.Id.ToString() + Extintion);
                upload.SaveAs(path);
                auther.ProfileImage = path;

                   

            }
           
            return View();

        }
        [HttpGet]
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(Auther NewAuther, HttpPostedFileBase upload)
        {
            if (Session["ID"] != null) { return RedirectToAction("Login"); }
            if (!ModelState.IsValid)
            {
                AddId(NewAuther);
                hashPassword(NewAuther);
                UploadProfileImage(NewAuther, upload);
                db.auther.Add(NewAuther);


            }
            if (IsEmailFound(NewAuther))
            {
                return HttpNotFound();

            }
            ModelState.Clear();
           
            db.SaveChanges();
            return RedirectToAction("login");


        }
        [HttpGet]
        public ActionResult Login()
        {

            if (Session["ID"] != null)
            {
                return RedirectToAction("show", "Paper");
            }

                return View();
        }
        [HttpPost]
        public  ActionResult Login(Auther auther)
        {        
                if (IsEmailFound(auther))
                {
                    foreach (Auther user in db.auther)
                    {
                        if (user.Email == auther.Email && verifyPassword(user.Password, auther.Password))
                        {

                          
                                Session["ID"] = user.Id.ToString();
                                Session["AutherName"] = user.FirstName.ToString();
                             Session["Email"] = user.Email.ToString();
                        return RedirectToAction("show","Paper");


                    }

                }

                }
               
                
            
                                ViewBag.state = "Email or Password Incorrect";
                                return View();
                            
         

        }
        public ActionResult Loggedin()
        {
            if (Session["ID"]!= null)
            {


                return View(Session["ID"]);


            }
           
                return RedirectToAction("Login");
            
        }
        [HttpGet]
        public ActionResult Edit()
        {
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login");

            }
            var Email = Session["Email"].ToString();
                var std = db.auther.Where(s => s.Email == Email).FirstOrDefault();
                return View(std);
           
        }
        [HttpPost]
        public ActionResult Edit(Auther EditedAuther, HttpPostedFileBase upload)
        {
           
                 if (!ModelState.IsValid)
                            {
                                var Email = Session["Email"].ToString();
                                var oldAuther = db.auther.Where(s => s.Email == Email).FirstOrDefault();
                                db.auther.Remove(oldAuther);
                                AddId(EditedAuther);
                                hashPassword(EditedAuther);
                                UploadProfileImage(EditedAuther, upload);
                                db.auther.Add(EditedAuther);
                              
                         
                            
                            }

            ModelState.Clear();

            db.SaveChanges();


       
            return RedirectToAction("Login");
           
        }
        public ActionResult Logout()
        {
            Session["ID"] = null;
            return RedirectToAction("login");
        }
        public ActionResult DeleteAcount()
        {
            var Email = Session["Email"].ToString();
            var std = db.auther.Where(s => s.Email == Email).FirstOrDefault();
            db.auther.Remove(std);
            db.SaveChanges();
            Session["ID"] = null;

            return RedirectToAction ("login");
        }
        public ActionResult ShowAutherProfile(Guid id)
        {
            
            var auther = db.auther.Find(id);
            var papers = db.papersAuhters.Where(u => u.AuhterID == id).ToList();
            List<Paper> pap = new List<Paper>();
            foreach (var i in papers)
            {
                var x = db.paper.Find(i.paperID);
                pap.Add(x);
                    };
            AutherProfile p = new AutherProfile
            {   auther= auther,
                paper = pap
            };
            List<AutherProfile> profile = new List<AutherProfile>();
            profile.Add(p);
            var data = profile.ToList();

            return View(data);
        }
        [HttpPost]
        public ActionResult Search (String Search)
        {

            string name = Request.Form["Name"].ToString();
            ViewBag.name = name;
            var auth = db.auther.Where(y => y.Email == name).ToList();
            if (auth.Count==0)
            {
                auth = db.auther.Where(y => y.University == name).ToList();

            }
            if(auth.Count == 0)
            {
                auth = db.auther.Where(y => y.Department== name).ToList();
            }
           
            return View(auth);
        }
    }
}