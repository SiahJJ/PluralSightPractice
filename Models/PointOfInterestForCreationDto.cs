using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        
        [Required(ErrorMessage = "You should provide a new value.")]//attribute, using system.componentModel.DataAnnotations (name field requred)
        [MaxLength(50)]//validation related annotations 
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]//validation related annotations (DTO)
        public string Description { get; set; }//got rid of the ?
        //public string? Descriptin { get; internal set; }
    }
}
