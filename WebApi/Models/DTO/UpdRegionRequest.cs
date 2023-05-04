using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Domain
{
    public class UpdRegionRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Range(3,3)]
        public string Code { get; set; }
        [Required]
        [Range(1, 100)]
        public string Name { get; set; }
        public string? ImageRegionUri { get; set; }

        public Region ToRegion()
        {
            return new Region()
            {
                Id = Id,
                Code = Code,
                Name = Name,
                ImageRegionUri = ImageRegionUri
            };
        }
    }
}
