namespace EntertainmentAPI.Entities
{
    public class QuizAnswerUser : BaseEntity
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public Guid QuizQuestionId { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsCorrect { get; set; }
    }
}
