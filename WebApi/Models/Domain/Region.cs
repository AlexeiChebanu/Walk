namespace WebApi.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ImageRegionUri { get; set; }

        public UpdRegionRequest ToRegion()
        {
            return new UpdRegionRequest()
            {
                Id = Id,
                Code = Code,
                Name = Name,
                ImageRegionUri = ImageRegionUri
            };
        }
    }
}
