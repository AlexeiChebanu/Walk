using WebApi.Models.Domain;
using WebApi.Repository;

namespace WebApi.Services.RegionServices;

public class RegionServices : IRegionServices
{
    private readonly IRegionRepository regionRepository;

    public RegionServices(IRegionRepository regionRepository)
    {
        this.regionRepository = regionRepository;
    }

    public Task<Region> CreateAsync(AddRegionRequestDTO region)
    {
        if (region == null) throw new ArgumentNullException(nameof(region));

        ValidationHelper.ModelValidation(region);

        var transferDTORegion = region.ToRegion();

        transferDTORegion.Id = Guid.NewGuid();

        var createAsync = regionRepository.CreateAsync(transferDTORegion);

        return createAsync;
    }

    public async Task<bool> DeleteAsync(Guid? regionId)
    {
        if(regionId == Guid.Empty) throw new ArgumentException(nameof(regionId));

        Region? region = await regionRepository.GetByIdAsync(regionId.Value);

        if (region == null) return false;

        await regionRepository.DeleteAsync(regionId);

        return true;
    }

    public async Task<List<Region>> GetAllAsync()
    {
        var regions = await regionRepository.GetAllAsync();

        return regions;
    }

    public async Task<Region?> GetByIdAsync(Guid? id)
    {
        if (id == null) return null;

        Region? region = await regionRepository.GetByIdAsync(id);

        if (region == null) return null;
        return region;
    }

    public async Task<Region?> UpdateAsync(UpdRegionRequest? region)
    {
        ValidationHelper.ModelValidation(region);

        if (region == null) return null;

        var regionDomainModel = await regionRepository.GetByIdAsync(region.Id);

        if(regionDomainModel == null) return null;

        regionDomainModel = region.ToRegion();

        await regionRepository.UpdateAsync(regionDomainModel);

        return regionDomainModel;
    }
}
