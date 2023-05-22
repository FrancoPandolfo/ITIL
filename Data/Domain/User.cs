using System.ComponentModel.DataAnnotations;

namespace ITIL.Data.Domain
{
    public class User : EntityBase
    {
        [StringLength(100)]
        public string Email {get; set;}

        [StringLength(100)]
        public string Password {get; set;}

        public virtual ICollection<Incident> Incidents {get; set;}
        public virtual ICollection<Problem> Problems {get;set;}
    }
}