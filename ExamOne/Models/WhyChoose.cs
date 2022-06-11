using System.ComponentModel.DataAnnotations;

namespace ExamOne.Models
{
    public class WhyChoose
    {
        public int Id { get; set; }
        [Required]
        public string Icon { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
