using PlaylistGenreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistGenreMvc.Controllers
{
    public class GenreController : Controller
    {
        private OperationDataContext context;
        public GenreController()
        {
            context = new OperationDataContext();
        }

        public ActionResult Index()
        {
            List<GenreModel> genreList = new List<GenreModel>();
            var query = from genre in context.Genres select genre;
            var genres = query.ToList();

            foreach (var genreItem in genres)
            {
                genreList.Add(new GenreModel()
                {
                    Id = genreItem.Id,
                    Name = genreItem.Name
                });
            }
            return View(genreList);
        }

        public ActionResult Create()
        {
            //GenreModel model = new GenreModel();
            //return View(model);
            return View();
        }

        [HttpPost]
        public ActionResult Create(GenreModel model)
        {
            try
            {
                Genre genre = new Genre()
                {
                    Name = model.Name
                };

                context.Genres.InsertOnSubmit(genre);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}