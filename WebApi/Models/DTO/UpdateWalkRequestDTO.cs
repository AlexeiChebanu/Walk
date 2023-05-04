using System.ComponentModel.DataAnnotations;
using WebApi.Models.Domain;

namespace WebApi.Models.DTO
{
    public class  UpdateWalkRequestDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double LengthInKm { get; set; }
        public string? WalkImageUri { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
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
