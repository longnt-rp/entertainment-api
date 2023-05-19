namespace EntertainmentAPI.Entities
{
    public class QuizQuestion : BaseEntity
    {
        public Guid QuizId { get; set; }
        public Guid QuestionId { get; set; }
    }
}
