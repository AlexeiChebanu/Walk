using System.ComponentModel.DataAnnotations;
using WebApi.Models.Domain;

namespace WebApi.Models.DTO
{
    public class AddWalkRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUri { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        public Walk ToWalk()
        {
            return new Walk()
            {
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
