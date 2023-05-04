using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Domain
{
    public class AddRegionRequestDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? ImageRegionUri { get; set; }

        public Region ToRegion()
        {
            return new Region()
            {
                Code = this.Code,
                Name = this.Name,
                ImageRegionUri = this.ImageRegionUri
            };
        }
    }
}
