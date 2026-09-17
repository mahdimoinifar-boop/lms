namespace LmsProject.DTOs.Lesson
{
    public class LessonDto
    {
        public int LessonId { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Content { get; set; }

        public string? VideoUrl { get; set; }

        public string? FileUrl { get; set; }

        public string? OnlineClassUrl { get; set; }

        public int Order { get; set; }
    }
}
