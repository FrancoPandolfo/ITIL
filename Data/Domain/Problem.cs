using ITIL.Data.Domain;

namespace ITIL.Data.Domain 
{
    public class Problem : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}