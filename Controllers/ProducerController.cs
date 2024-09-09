using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TVProject.Data.DataBase;
using TVProject.Data.Services.ProducerService;
using TVProject.Models;

namespace TVProject.Controllers
{
    [Authorize]
    public class ProducerController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducerController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var producers = await _producerService.GetAllAsync();
            return View(producers);
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _producerService.GetByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _producerService.AddAsync(producer);
                return RedirectToAction("Index");
            }
            return View(producer);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _producerService.GetByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // GET: Actors/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _producerService.UpdateAsync(producer);
                return RedirectToAction("Index");
            }
            return View(producer);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _producerService.GetByIdAsync(id);
            if(producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _producerService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
