using System.ComponentModel.DataAnnotations.Schema;

namespace ITIL.Data.Domain 
{
    public class Problem : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User {get; set;}
        public int? ConfigurationItemId {get; set;}
        [ForeignKey("ConfigurationItemId")]
        public virtual ConfigurationItem ConfigurationItem {get; set;}
        public int AssignedUserId {get;set;}
        [ForeignKey("AssignedUserId")]
        public virtual User AssignedUser {get;set;}
        public string Impact {get;set;}
        public string Priority {get;set;}
        public List<string> Comments {get;set;}

        public Problem(){
            Comments = new List<string>();
        }

        public virtual ICollection<Incident> Incidents {get; set;}
        public virtual ICollection<Change> Changes { get; set; }
    }
}