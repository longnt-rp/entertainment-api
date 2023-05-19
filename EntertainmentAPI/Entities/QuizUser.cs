namespace EntertainmentAPI.Entities
{
    public class QuizUser : BaseEntity
    {
        public Guid QuizId { get; set; }
        public Guid UserId { get; set; }
        public int TotalQuestionNum { get; set; }
        public int CorrectQuestionNum { get; set; }
    }
}
