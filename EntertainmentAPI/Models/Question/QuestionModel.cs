using static EntertainmentAPI.Enums.QuestionEnums;

namespace EntertainmentAPI.Models.Question
{
    public class QuestionModel
    {
        public Guid Id { get; set; }
        public Guid TopicId { get; set; }
        public string TopicName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Image { get; set; }
        public QuestionType Type { get; set; }
        public QuestionLevel Level { get; set; }
    }
}
