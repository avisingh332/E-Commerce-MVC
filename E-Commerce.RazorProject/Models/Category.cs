using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace E_Commerce.RazorProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Category Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Category Display Order")]
        [Required]
        [Range(0, 50)]
        public int DisplayOrder { get; set; }
    }
}
