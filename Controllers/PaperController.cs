using IAproject.Models;
using IAproject.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IAproject.Controllers
{
    public class PaperController : Controller
    {
        // GET: Paper
        DefaultConnection db = new DefaultConnection();
        public ActionResult Home()
        {
            var data = db.paper.First();
            List<Paper> papers = new List<Paper>

            {
            new Paper
            {
                Title =data.Title,
                Content = data.Content,
                id = data.id,
                authers = data.authers
       

            }

            };

            
            return View(papers);
        }
        public Paper AddId(Paper Paper)
        {
            Guid id = Guid.NewGuid();

            Paper.id = id;

            return Paper;
        }
        public ActionResult ShowOnePaper(String id)
        {

            Guid prevID = Guid.NewGuid();
         

            var allpapars = db.paper;
            var allauthers = db.auther;
            var paperAuthersIDs = db.papersAuhters.Where(i => i.paperID.ToString() == id);
            List<PaperAuthors> paperAuther = new List<PaperAuthors>();



            // var number = paperAuthersIDs.Where(i => i.paperID == paper.id).Count();
            PaperAuthors temp = new PaperAuthors
                    {
                        paper = allpapars.FirstOrDefault(u => u.id.ToString() ==id),

                    };
                    List<Auther> a = new List<Auther>();
                    foreach (var i in paperAuthersIDs.Where(y => y.paperID.ToString() ==id ))
                    {
                        var authers = allauthers.Where(u => u.Id == i.AuhterID);

                        if (authers != null)
                        {

                            foreach (var l in authers)
                            {
                                a.Add(l);

                            }
                            temp.auther = a;
                        }

                    }
            paperAuther.Add(temp);

                    


                
           
            var data = paperAuther.ToList();
            return View(data);
        }
        public ActionResult show()
        {
            Guid prevID = Guid.NewGuid();
            // var data = db.Database.ExecuteSqlCommand("SELECT  ID, FirstName, Contant, Title, Paper_id , Auther_Id FROM PaperAuthers JOIN Papers ON papers.id=PaperAuhters.Paper_id Join Authers on Auhters.ID= PaperAuhters.Auther_Id; ");
            //ViewBag.st = data.ToString();

 
            var allpapars = db.paper;
            var allauthers = db.auther;
            var paperAuthersIDs = db.papersAuhters.OrderByDescending(i => i.paperID);
            List<PaperAuthors> paperAuther = new List<PaperAuthors>();
            foreach (var papers in paperAuthersIDs)
            {
                if (papers.paperID != prevID)
                {


                    // var number = paperAuthersIDs.Where(i => i.paperID == paper.id).Count();
                    PaperAuthors temp = new PaperAuthors
                    {
                        paper = allpapars.FirstOrDefault(u => u.id == papers.paperID),

                    };
                        List<Auther> a = new List<Auther>();
                    foreach (var i in paperAuthersIDs.Where(y => y.paperID == papers.paperID))
                    {
                        var authers = allauthers.Where(u => u.Id == i.AuhterID);

                        if (authers != null)
                        {
                          
                            foreach (var l in authers)
                            {
                                a.Add(l);

                            }
                            temp.auther = a;
                        }

                    }

                    paperAuther.Add(temp);

                    prevID = papers.paperID;


                }
            }
           var data = paperAuther.ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult AddPaper()
        {
            

            return View();

        }
        [HttpPost]
        public ActionResult AddPaper(Paper paper)
        {var Email = Session["Email"].ToString();
                    var writer = db.auther.Where(s => s.Email == Email).FirstOrDefault(); 
            if (Session["ID"] != null)
            {  
                if (ModelState.IsValid)
                {


                  
                    AddId(paper);
                    writer.papers = new List<Paper>
                    {
                        paper
                    };
                    paper.authers = new List<Auther>
                    {
                       writer
                    };
                   
                    paper.authers.Add(writer);
                    papersAuther p = new papersAuther
                    {
                        paperID= paper.id,
                        AuhterID= writer.Id
                    };
                    db.papersAuhters.Add(p);
                    db.paper.Add(paper);

                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("show");
                }
                ViewBag.state = "Data not valid";
                return View();

            }
            return RedirectToAction("Auther/Login");


        }
        
        public ActionResult AddLike(Guid id)
        {
            Boolean isLikedByThisUser = false;
            likes newlike = new likes();
            if (Session["ID"] == null)
            {
                return RedirectToAction( "Login","Auther");

            }
            var isPaperLikedBefore = db.likes.FirstOrDefault(l => l.paperID == id);
           if(isPaperLikedBefore==null)
            {

                


                newlike.paperID = id;
                newlike.AutherID = Guid.Parse(Session["ID"].ToString());

                db.likes.Add(newlike);
                db.SaveChanges();
            


            }
           
            else
            {
                var allLikesForeThishPaper = db.likes.Where(i => i.paperID == id);
                foreach(var i in allLikesForeThishPaper)
                {
                    if (i.AutherID == Guid.Parse(Session["ID"].ToString()))
                    {
                       
                        isLikedByThisUser = true;
                       
                      
                        db.likes.Remove(i);
                      
                       
                    }
                }  db.SaveChanges();
            }
            if (!isLikedByThisUser)
            {
                    newlike.paperID = id;
                    newlike.AutherID = Guid.Parse(Session["ID"].ToString());
                    db.likes.Add(newlike);
                    db.SaveChanges();
            }
           
            return View();

        }

        public ActionResult AddDisLike(Guid id)
        {
            Boolean isdisLikedByThisUser = false;
            dislike newDislike = new dislike();
            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Auther");

            }
            var isPaperdisLikedBefore = db.dislikes.FirstOrDefault(l => l.paperID == id);
            if (isPaperdisLikedBefore == null)
            {

                RedirectToAction("Auther", "Login");


                newDislike.paperID = id;
                newDislike.AutherID = Guid.Parse(Session["ID"].ToString());

                db.dislikes.Add(newDislike);
                db.SaveChanges();



            }

            else
            {
                var alldisLikesForeThishPaper = db.dislikes.Where(i => i.paperID == id);
                foreach (var i in alldisLikesForeThishPaper)
                {
                    if (i.AutherID == Guid.Parse(Session["ID"].ToString()))
                    {

                        isdisLikedByThisUser = true;


                        db.dislikes.Remove(i);


                    }
                }
                db.SaveChanges();
            }
            if (!isdisLikedByThisUser)
            {
                newDislike.paperID = id;
                newDislike.AutherID = Guid.Parse(Session["ID"].ToString());
                db.dislikes.Add(newDislike);
                db.SaveChanges();
            }

            return View();

        }
   

        [HttpPost]
        public ActionResult addComment(String Id  )

        {

            Guid paperID = Guid.Parse(Id);
            String commentContent = Request.Form["Comment"].ToString();

            if (Session["ID"] == null)
            {
                return RedirectToAction("Login", "Auther");
            }
            Comments comment = new Comments
            {
                paperID = paperID,
                AutherID = Guid.Parse(Session["ID"].ToString()),
                Comment = commentContent
            };
          
            db.comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("show");
        }
       
    }
}
/*
 *  var paperAuthersIDs=db.paperAuthers;
            var allpapars = db.paper;
            var allauthers = db.auther;
            List<paperAutherModel> paperAuther = new List<paperAutherModel>();
            foreach(var papers in paperAuthersIDs)
            {
                int indextemp = 0;
                paperAutherModel temp = new paperAutherModel
                {
                    Paper = allpapars.FirstOrDefault(u => u.id == papers.PaperID),
                    
                };
                foreach (var i in paperAuthersIDs.Where(y => y.PaperID == papers.PaperID))
                {
                    var authers = allauthers.FirstOrDefault(u =>u.Id == papers.AutherID);
                    if (authers != null)
                    {
                        temp.Authers = new List<Auther>{
                            authers
                        };
                        indextemp = indextemp + 1;
                    }
                    
                }
                paperAuther.Add(temp);
            }


            paperAuther.ToList();
          
            
*/

/*  var id = Session["ID"];
        var viewModel = new paperAutherModel();
        viewModel.papers = viewModel.Authers.Where(x => x.Email == Session["Email"].ToString()).Single().papers;
        if (viewModel == null)
        {
            return RedirectToAction("AddPaper");
        }
*/
