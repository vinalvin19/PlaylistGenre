using PlaylistGenreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistGenreMvc.Controllers
{
    public class SongController : Controller
    {
        private OperationDataContext context;

        public SongController()
        {
            context = new OperationDataContext();
        }

        public ActionResult Index()
        {
            IList<SongModel> SongList = new List<SongModel>();
            var query = from song in context.Songs
                        join genre in context.Genres
                        on song.GenreId equals genre.Id
                        select new SongModel
                        {
                            Id = song.Id,
                            Title = song.Title,
                            Singer = song.Singer,
                            Year = song.Year,
                            Writer = song.Writer,
                            GenreName = genre.Name
                        };
            SongList = query.ToList();

            return View(SongList);
        }

        public ActionResult Create()
        {
            SongModel model = new SongModel();
            PreparePublisher(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SongModel model)
        {
            try
            {
                Song song = new Song()
                {
                    Title = model.Title,
                    Singer = model.Singer,
                    Writer = model.Writer,
                    Year = model.Year,
                    GenreId = model.GenreId
                };
                context.Songs.InsertOnSubmit(song);
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            SongModel model = context.Songs.Where(some => some.Id == id).Select(some =>
                                new SongModel()
                                {
                                    Id = some.Id,
                                    Title = some.Title,
                                    Singer = some.Singer,
                                    Year = some.Year,
                                    Writer = some.Writer,
                                    GenreId = some.Id
                                }).SingleOrDefault();

            PreparePublisher(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SongModel model)
        {
            try
            {
                Song song = context.Songs.Where(some => some.Id == model.Id).Single<Song>();
                song.Title = model.Title;
                song.Singer = model.Singer;
                song.Year = model.Year;
                song.Writer = model.Writer;
                song.GenreId = model.GenreId;
                context.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        private void PreparePublisher(SongModel model)
        {
            model.Genres = context.Genres.AsQueryable<Genre>().Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    });
        }

        public ActionResult Delete(int id)
        {
            SongModel model = context.Songs.Where(some => some.Id == id).Select(some =>
                                  new SongModel()
                                  {
                                      Id = some.Id,
                                      Title = some.Title,
                                      Singer = some.Singer,
                                      Year = some.Year,
                                      Writer = some.Writer,
                                      GenreName = some.Genre.Name
                                  }).SingleOrDefault();
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(SongModel model)
        {
            try
            {
                Song song = context.Songs.Where(some => some.Id == model.Id).Single<Song>();
                context.Songs.DeleteOnSubmit(song);
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