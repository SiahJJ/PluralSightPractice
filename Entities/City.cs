using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class City
    {
        //system.componentmodel.dataannotations - key annotation 
        [Key]
        //Generation of primary keys - using system.componenmodel.dataannotation.schema 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]

        public string Name { get; set; }//description 
        [MaxLength(200)]
        public string ? Description { get; set; }

        public ICollection<PointOfInterest> PointsOfInterest { get; set; }
            = new List<PointOfInterest>();
        
        //adding constuctor to this class - 
        public City(string name)
        {
            Name = name;
        }
    }
}