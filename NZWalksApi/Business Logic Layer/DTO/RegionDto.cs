namespace NZWalksApi.DTO
{
    // DTO is the subset of domainmodel Region 
    // DTO is send the data to Client
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }
        public string? RegionImageUrl {get; set; }

    }
}
