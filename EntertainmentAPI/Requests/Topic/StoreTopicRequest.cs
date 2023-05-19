using System.ComponentModel.DataAnnotations;

namespace EntertainmentAPI.Requests.Topic
{
    public class StoreTopicRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
