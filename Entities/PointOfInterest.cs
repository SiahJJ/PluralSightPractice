using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class PointOfInterest
    {
        //system.componentmodel.dataannotations - key annotation 
        [Key]
        //Generation of primary keys - using system.componenmodel.dataannotation.schema 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        
        public string Name { get; set; }//do not want a null value 
        [ForeignKey("CityId")]//foregin key - not necessary 
        public City? City { get; set; }//property - navagation property, relationship to be discovered by Id
        public int CityId { get; set; }

        //adding constuctor to this class - 
        public PointOfInterest(string name)
        {
            Name = name;
        }
    }
}