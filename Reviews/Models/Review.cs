using System.ComponentModel.DataAnnotations;

namespace Reviews.Models
{
    // a class for reviewing an app, with id, username, date, score, and feedback
    public class Review
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="User")]
        public string? Username { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        [Range(1, 5, ErrorMessage = "Score must be between 1 and 5")]
        public int Score { get; set; }
        public string? Feedback { get; set; }
    }
}
