using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.Domain;

namespace WebApi.Repository;

public class SQLWalkRepository : IWalkRepository
{
    private readonly DbContextWalks dbContextWalks;

    public SQLWalkRepository(DbContextWalks dbContextWalks)
    {
        this.dbContextWalks = dbContextWalks;
    }
    public async Task<Walk> CreateAsync(Walk walk)
    {
        await dbContextWalks.Walks.AddAsync(walk);

        await dbContextWalks.SaveChangesAsync();

        return walk;
    }

    public async Task<bool> DeleteAsync(Guid? id)
    {
        dbContextWalks.Walks.RemoveRange(dbContextWalks.Walks.Where(temp => temp.Id == id));
        int rowsDetected = await dbContextWalks.SaveChangesAsync();

        return rowsDetected > 0;
    }

    public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
                                                string? sortBy = null, bool isAscending = true,
                                                int pageNumber = 1, int pageSize = 1000)
    {
        var walks = dbContextWalks.Walks.Include("Difficulty").Include("Region").AsQueryable();

        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = walks.Where(x => x.Name.Contains(filterQuery));
            }
        }

        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }
        }

        var skipResult = (pageNumber - 1) * pageSize;        

        return await walks.Skip(skipResult).Take(pageSize).ToListAsync();

        //return await dbContextWalks.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid? id)
    {
        return await dbContextWalks.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateAsync(Walk walk)
    {
        var existWalk = await dbContextWalks.Walks.FirstOrDefaultAsync(x => x.Id == walk.Id);

        if (existWalk == null) return null;

        existWalk.Name = walk.Name;
        existWalk.Description = walk.Description;
        existWalk.LengthInKm = walk.LengthInKm;
        existWalk.WalkImageUri = walk.WalkImageUri;
        existWalk.DifficultyId = walk.DifficultyId;
        existWalk.RegionId = walk.RegionId;

        await dbContextWalks.SaveChangesAsync();

        return existWalk;
    }
}
