using System.ComponentModel.DataAnnotations;

namespace EntertainmentAPI.Requests.User
{
    public class UpdateUserRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
