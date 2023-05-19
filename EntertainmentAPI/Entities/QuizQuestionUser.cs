namespace EntertainmentAPI.Entities
{
    public class QuizQuestionUser : BaseEntity
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public Guid QuestionId { get; set; }
        public Guid QuizQuestionId { get; set; }
    }
}
