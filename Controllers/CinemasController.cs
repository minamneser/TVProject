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
using TVProject.Data.Services.CinemaServices;
using TVProject.Models;

namespace TVProject.Controllers
{
    [Authorize]
    public class CinemasController : Controller
    {
        
        private readonly ICinemaService _cinemaService;

        public CinemasController(ICinemaService cinemaService)
        {
            
            _cinemaService = cinemaService;
        }

        // GET: Cinemas
        public async Task<IActionResult> Index()
        {
            var cinemas = await _cinemaService.GetAllAsync();
            return View(cinemas);
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);
            if(cinema == null)
            {
                return NotFound();
            }
            return View(cinema);

        }
        // GET: Cinemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if(ModelState!=null)
            {
                await _cinemaService.AddAsync(cinema);
                return RedirectToAction("Index");
            }
            return View(cinema);
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }
        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _cinemaService.UpdateAsync(cinema);
                return RedirectToAction("Index");
            }
            return View(cinema);
        }
        
        
        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _cinemaService.GetByIdAsync(id);
            if(cinema == null)
            {
                return NotFound();
            }
            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
                await _cinemaService.DeleteAsync(id);
                return RedirectToAction("Index");
        }
    }
}
