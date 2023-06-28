using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a new value.")]//validation annotations 
        [MaxLength(50)]
        //no id - can be used for validation 
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
        
    }
}