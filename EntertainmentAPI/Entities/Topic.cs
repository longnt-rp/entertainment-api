namespace EntertainmentAPI.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<Question> Questions { get; set; }
    }
}
