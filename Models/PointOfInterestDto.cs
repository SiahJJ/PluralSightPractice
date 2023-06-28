namespace CityInfo.API.Models
{ 
    public class PointOfInterestDto
    {
        public int Id { get; set; }//id value not used in post, not chosen by user (in this design)
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Descriptin { get; internal set; }

    }
}
