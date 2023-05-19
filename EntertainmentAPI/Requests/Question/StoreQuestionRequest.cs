using System.ComponentModel.DataAnnotations;
using static EntertainmentAPI.Enums.QuestionEnums;

namespace EntertainmentAPI.Requests.Question
{
    public class StoreQuestionRequest
    {
        [Required]
        public Guid TopicId { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        public string? Image { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        [Required]
        public QuestionLevel Level { get; set; }
    }
}
