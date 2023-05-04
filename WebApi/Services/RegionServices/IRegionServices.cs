using WebApi.Models.Domain;

namespace WebApi.Services.RegionServices;

public interface IRegionServices
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetByIdAsync(Guid? id);
    Task<Region> CreateAsync(AddRegionRequestDTO region);
    Task<Region?> UpdateAsync(UpdRegionRequest? region);
    Task<bool> DeleteAsync(Guid? regionId);
}
