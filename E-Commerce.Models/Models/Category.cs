using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required] [DisplayName("Category Name")]   
        public string Name { get; set; }
        
        [DisplayName("Category Display Order")]
        [Required]
        [Range(0,50)]
        public int DisplayOrder { get; set; }
    }
}
