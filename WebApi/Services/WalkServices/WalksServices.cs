using AutoMapper;
using WebApi.Models.Domain;
using WebApi.Models.DTO;
using WebApi.Repository;

namespace WebApi.Services.WalkServices
{
    public class WalksServices : IWalksService
    {
        private readonly IWalkRepository walkRepository;

        public WalksServices(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }

        public async Task<Walk> CreateAsync(AddWalkRequestDTO? walk)
        {
            if (walk == null) throw new ArgumentNullException(nameof(walk));

            ValidationHelper.ModelValidation(walk);

            Walk walks = walk.ToWalk();

            walks.Id = Guid.NewGuid();

            await walkRepository.CreateAsync(walks);

            return walks;
        }

        public async Task<bool> DeleteAsync(Guid? Id)
        {
            if (Id == null) throw new ArgumentNullException(nameof(Id));

            Walk? walk = await walkRepository.GetByIdAsync(Id.Value);

            if (walk == null) return false;

            await walkRepository.DeleteAsync(Id);

            return true;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery,
                                             string? sortBy, bool IsAscending,
                                             int pageNumber = 1, int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, IsAscending, pageNumber, pageSize);

            return walksDomainModel;
        }


        public async Task<Walk> GetByIdAsync(Guid? Id)
        {
            if (Id == null) return null;

            Walk? walk = await walkRepository.GetByIdAsync(Id);
            if (walk == null) return null;

            return walk;
        }

        public async Task<Walk> UpdateAsync(UpdateWalkRequestDTO? walk)
        {
            ValidationHelper.ModelValidation(walk);

            if (walk == null) return null;

            var walkDomainModel = await walkRepository.GetByIdAsync(walk.Id);

            if (walkDomainModel == null) return null;

            walkDomainModel = walk.ToWalk();

            await walkRepository.UpdateAsync(walkDomainModel);

            return walkDomainModel;

        }
    }
}
