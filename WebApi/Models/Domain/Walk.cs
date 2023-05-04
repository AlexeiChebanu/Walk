using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection;
using WebApi.Models.DTO;

namespace WebApi.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUri { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
        //Nav
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }

        public UpdateWalkRequestDTO ToWalk()
        {
            return new UpdateWalkRequestDTO()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                LengthInKm = LengthInKm,
                WalkImageUri = WalkImageUri,
                DifficultyId = DifficultyId,
                RegionId = RegionId
            };
        }
    }
}
