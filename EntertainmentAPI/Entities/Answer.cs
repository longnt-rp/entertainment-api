namespace EntertainmentAPI.Entities
{
    public class Answer : BaseEntity
    {
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int IndexOrder { get; set; }
    }
}
