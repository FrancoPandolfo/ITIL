namespace ITIL.Data.Domain
{
    public class Change : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public virtual User User {get; set;}
        public int? ConfigurationItemId {get; set;}
        public virtual ConfigurationItem ConfigurationItem {get; set;}
        public string ClientName {get;set;}
        public string ClientEmail {get;set;}
    }
}