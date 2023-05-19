using static EntertainmentAPI.Enums.QuestionEnums;

namespace EntertainmentAPI.Entities
{
    public class Question : BaseEntity
    {
        public Guid TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Image { get; set; }
        public QuestionType Type { get; set; }
        public QuestionLevel Level { get; set; }
        public List<Guid> AnswerId { get; set; }
    }
}
