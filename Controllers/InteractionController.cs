using IAproject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAproject.Controllers
{
    public class InteractionController : Controller
    {
        DefaultConnection db = new DefaultConnection();

        // GET: Interaction
        public ActionResult Index()
        {
            return View();
        }
        public void AddLike (Guid id)
        {
           if(Session["ID"]==null)
            {
                 RedirectToAction("Auther", "Login");

            }
            var isPaperLikedBefore = db.likes.Where(l => l.paperID == id);
            if (isPaperLikedBefore == null)
            {
                likes newlike = new likes
                {
                    paperID = id,
                    AutherID = Guid.Parse(Session["ID"].ToString())
                };
                db.likes.Add(newlike);
            }
          
           
          

        }
    }
}