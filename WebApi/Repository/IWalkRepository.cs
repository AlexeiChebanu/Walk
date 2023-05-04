using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApi.Models.Domain;

namespace WebApi.Repository
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, 
            string? sortBy = null, bool isAscending = true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid? id); 

        Task<Walk?> UpdateAsync(Walk walk);

        Task<bool> DeleteAsync(Guid? id);
     }
}
