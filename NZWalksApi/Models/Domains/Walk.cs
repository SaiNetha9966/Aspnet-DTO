namespace NZWalksApi.Models.Domains
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double LengthInKM { get; set; }

        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }

        public Guid RegionID { get; set; }


        // Navigation Proporty  this is the connection between Difficulty and walk tables in database 

        public Difficulty Difficulty { get; set;}

        // this is the connection between Walk and Region Tables in the data base
        public Region Region { get; set; }
    }
}
