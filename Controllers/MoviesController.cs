using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TVProject.Data.DataBase;
using TVProject.Data.Interfaces;
using TVProject.Data.Services.ElasticSearch;
using TVProject.Data.Services.MovieServices;
using TVProject.Models;

namespace TVProject.Controllers
{
    
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ElasticSearchService _elasticsearchService;

        public MoviesController(IMovieService movieService, ElasticSearchService elasticSearchService)
        {
            _movieService = movieService;
            _elasticsearchService = elasticSearchService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetAllAsync();
            ViewBag.Cinemas = new SelectList(await _movieService.GetAllCinemasAsync(), "Id", "Name");
            ViewBag.Producers = new SelectList(await _movieService.GetAllProducersAsync(), "Id", "FullName");
            ViewBag.Actors = new SelectList(await _movieService.GetAllActorsAsync(), "Id", "FullName");
            return View(movies);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Cinemas = new SelectList(await _movieService.GetAllCinemasAsync(), "Id", "Name");
            ViewBag.Producers = new SelectList(await _movieService.GetAllProducersAsync(), "Id", "FullName");
            ViewBag.Actors = new SelectList(await _movieService.GetAllActorsAsync(), "Id", "FullName");
            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Cinemas = new SelectList(await _movieService.GetAllCinemasAsync(), "Id", "Name");
            ViewBag.Producers = new SelectList(await _movieService.GetAllProducersAsync(), "Id", "FullName");
            ViewBag.Actors = new SelectList(await _movieService.GetAllActorsAsync(), "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.AddAsync(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.Cinemas = new SelectList(await _movieService.GetAllCinemasAsync(), "Id", "Name");
            ViewBag.Producers = new SelectList(await _movieService.GetAllProducersAsync(), "Id", "FullName");
            ViewBag.Actors = new SelectList(await _movieService.GetAllActorsAsync(), "Id", "FullName");
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                var movieModel = new Movie
                {
                    Name = movie.Name,
                    Id = movie.Id,
                    Cinema = movie.Cinema,
                    CinemaId = movie.CinemaId,
                    ImageUrl = movie.ImageUrl,
                    Price = movie.Price,
                    ProducerId = movie.ProducerId,
                    Producer = movie.Producer,
                    Description = movie.Description,
                    StartDate = movie.StartDate,
                    EndDate = movie.EndDate,
                    MovieCategory = movie.MovieCategory,
                    Actor_Movies = new List<Actor_Movie>()
                };
                if (movie.Actor_Movies != null)
                {
                    foreach(var  actorMovie in movie.Actor_Movies)
                    {
                        movie.Actor_Movies.Add(new Actor_Movie
                        {
                            ActorId = actorMovie.ActorId,
                            MovieId = actorMovie.MovieId,
                        });
                    }
                }
                await _movieService.UpdateAsync(movie);
                return RedirectToAction("Index");
            }
            return View(movie);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if(movie == null)
            {
                return NotFound(); 
            }
            return View(movie);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieService.DeleteAsync(id);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Search(string query)
        {
            var searchResult = await _elasticsearchService.SearchMoviesAsync(query);
            return View(searchResult.Documents);
        }

    }
}
