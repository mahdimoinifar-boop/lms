namespace LmsProject.Models.Entities
{
    public class Lesson
    {

        public int LessonId { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? VideoUrl { get; set; }

        public string? FileUrl { get; set; }

        public string? OnlineClassUrl { get; set; }

        public int Order { get; set; }
    }
}
