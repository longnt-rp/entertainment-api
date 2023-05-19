namespace EntertainmentAPI.Entities
{
    public class Quiz : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int QuestionNumber { get; set; }
        public bool IsShuffleQuestion { get; set; }
        public bool IsShuffleAnswer { get; set; }

    }
}
