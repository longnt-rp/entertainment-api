using System.ComponentModel.DataAnnotations;

namespace EntertainmentAPI.Requests.User
{
    public class StoreUserRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string Username { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
