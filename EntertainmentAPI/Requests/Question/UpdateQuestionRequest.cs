using static EntertainmentAPI.Enums.QuestionEnums;
using System.ComponentModel.DataAnnotations;

namespace EntertainmentAPI.Requests.Question
{
    public class UpdateQuestionRequest
    {
        [Required]
        public string Content { get; set; } = string.Empty;
        public string? Image { get; set; }
        [Required]
        public QuestionLevel Level { get; set; }
    }
}
