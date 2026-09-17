using System.ComponentModel.DataAnnotations;

namespace LmsProject.DTOs.Category
{
    public class CreateCategoryDto
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
