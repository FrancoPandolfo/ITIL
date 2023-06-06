namespace ITIL.Model 
{
    public class ConfigurationItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string VersionId {get;set;}
        public string? VersionHistory {get; set;}
    }
}