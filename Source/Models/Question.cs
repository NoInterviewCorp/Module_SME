using System.Collections.Generic;
namespace SME.Models
{
    public class Question
    {
        [Key]
        public string QuestionId { get; set; }
        [Required]
        public string ProblemStatement { get; set; }
        [Required]
        public List<Option> Options { get; set; }
        [Required]
        public string ResourceLink { get; set; }
        [Required]
        public BloomTaxonomy BloomLevel { get; set; }
        public int TopicId { get; set; }
    }
}