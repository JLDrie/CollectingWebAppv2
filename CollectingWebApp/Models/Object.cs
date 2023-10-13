using System.ComponentModel.DataAnnotations;

namespace CollectingWebApp.Models
{
    public class Object
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ObjectName { get; set; }
        public string ObjectDescription { get; set; }
        public int Price { get; set; }
        public int Worth { get; set; }
        public int PriceWorthDifference => Price - Worth; 
        public int CollectionId { get; set; }
        public Collection Collection { get; set; }
    }
}
