using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SME.Models
{
    public class LearningPlan
    {
        public string LearningPlanId { get; set; }
        public string Name { get; set; }
        public List<Topic> Topics { get; set; }
        public bool HasPublished { get; set; }
        // foreign key to technology
        public string TechnologyId { get; set; }
        public Technology Technology { get; set; }

    }
}