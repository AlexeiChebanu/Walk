using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.Domain;

namespace WebApi.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly DbContextWalks dbContextWalks;

        public SQLRegionRepository(DbContextWalks dbContextWalks)
        {
            this.dbContextWalks = dbContextWalks;
        }

        public async Task<Region?> CreateAsync(Region region)
        {
            await dbContextWalks.Regions.AddAsync(region);
            await dbContextWalks.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid? regionId)
        {
            var exist = await dbContextWalks.Regions.FirstOrDefaultAsync(x => x.Id == regionId);

            if (exist == null) return null;

            dbContextWalks.Regions.Remove(exist);
            await dbContextWalks.SaveChangesAsync(); 
            
            return exist;

        }

        public async Task<List<Region>> GetAllAsync()
        {
           return await dbContextWalks.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid? id)
        {
            return await dbContextWalks.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Region region)
        {
            var regionExist = await dbContextWalks.Regions.FirstOrDefaultAsync(x=>x.Id  == region.Id);
            if (regionExist == null) return null;

            regionExist.Id = region.Id;
            regionExist.Code = region .Code;
            regionExist.Name = region .Name;
            regionExist.ImageRegionUri = region.ImageRegionUri;

            await dbContextWalks.SaveChangesAsync();

            return regionExist;
        }
    }
}
