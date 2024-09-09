using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TVProject.Data.Services.ActorServices;
using TVProject.Models;

namespace TVProject.Controllers
{
    [Authorize]
    public class ActorController : Controller
    {
        private readonly IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }
        public async Task<IActionResult> Index()
        {
            var actors = await _actorService.GetAllAsync();
            return View(actors);
        }
        public async Task<IActionResult> Details(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _actorService.AddAsync(actor);
                return RedirectToAction("Index");
            }
            return View(actor);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);    
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                await _actorService.UpdateAsync(actor);
                return RedirectToAction("Index");
            }
            return View(actor);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _actorService.GetByIdAsync(id);
            if(actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }
        [HttpPost,ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _actorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
