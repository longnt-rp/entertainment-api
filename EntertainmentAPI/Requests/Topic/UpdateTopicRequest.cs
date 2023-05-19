using System.ComponentModel.DataAnnotations;

namespace EntertainmentAPI.Requests.Topic
{
    public class UpdateTopicRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
