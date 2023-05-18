namespace ITIL.Data.Domain 
{
    public class Incident : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public virtual User User {get; set;}
    }
}