using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieSiteV2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSiteV2.Controllers
{
    public class HomeController : Controller
    {
        private MovieListContext Context { get; set; }

        public HomeController(MovieListContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Podcast()
        {
            return View();
        }

        //ADD
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                Context.Movies.Add(newMovie);
                Context.SaveChanges();
            }

            return View("AddConfirm", newMovie);
        }

        //LIST OF ALL FILMS -- WHERE DELETE AND EDIT CAN BE ACCESSED
        public IActionResult Films()
        {
            return View(Context.Movies);
        }

        [HttpGet]
        public IActionResult Delete(int selectedId)
        {
            Movie selectedMovie = Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault();

            return View(selectedMovie);
        }

        //DELETE
        [HttpPost]
        public IActionResult Delete(Movie selectedMovie)
        {
            //find and delete movie from database
            Context.Movies.Remove(selectedMovie);
            Context.SaveChanges();

            //send user to delete confirmation page
            return View("DeleteConfirm", selectedMovie.title);
        }

        //DELETE CONFIRM -- TELLS USER NAME OF DELETED MOVIE
        public IActionResult DeleteConfirm()
        {

            return View();
        }

        //RETURNS SELECTED MOVIE TO FORM PAGE
        [HttpGet]
        public IActionResult Edit(int selectedId)
        {
            //find the movie in db to return to edit page
            Movie selectedMovie = Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault();

            return View(selectedMovie);
        }

        //UPDATES THE DATABASE
        [HttpPost]
        public IActionResult Edit(Movie updatedMovie, int selectedId)
        {

            if (Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault() != null)
            {
                //transfer the new information in updatedMovie to the movie in the database
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().title = updatedMovie.title;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().category = updatedMovie.category;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().director = updatedMovie.director;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().year = updatedMovie.year;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().rating = updatedMovie.rating;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().edited = updatedMovie.edited;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().lent_to = updatedMovie.lent_to;
                Context.Movies.Where(m => m.MovieId == selectedId).FirstOrDefault().notes = updatedMovie.notes;
            }

            Context.SaveChanges();

            return View("UpdateConfirm", updatedMovie);
        }

       

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
