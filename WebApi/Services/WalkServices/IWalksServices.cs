using WebApi.Models.Domain;
using WebApi.Models.DTO;

namespace WebApi.Services.WalkServices
{
    public interface IWalksService
    {
        Task<Walk> CreateAsync(AddWalkRequestDTO walk);
        Task<Walk> UpdateAsync(UpdateWalkRequestDTO? walk);
        Task<bool> DeleteAsync(Guid? Id);
        Task<Walk> GetByIdAsync(Guid? Id);
        Task<List<Walk>> GetAllAsync(string? filterOn, string? filterQuery,
                                             string? sortBy, bool IsAscending,
                                             int pageNumber = 1, int pageSize = 1000);
    }
}
