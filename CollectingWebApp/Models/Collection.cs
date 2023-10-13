using System.ComponentModel.DataAnnotations;

namespace CollectingWebApp.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        //public int NumberofObjects { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Object> Objects { get; set; }

    }
}
