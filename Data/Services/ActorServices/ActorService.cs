using TVProject.Data.Interfaces;
using TVProject.Models;

namespace TVProject.Data.Services.ActorServices
{
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Actor actor)
        {
            await _unitOfWork.Actors.AddAsync(actor);
            await _unitOfWork.saveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Actors.DeleteAsync(id);
            await _unitOfWork.saveAsync();
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _unitOfWork.Actors.GetAllAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _unitOfWork.Actors.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Actor actor)
        {
            await _unitOfWork.Actors.UpdateAsync(actor);
            await _unitOfWork.saveAsync();
        }
    }
}
