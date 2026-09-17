namespace LmsProject.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
