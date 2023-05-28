namespace ITIL.Model 
{
    public class IncidentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ConfigurationItemId { get; set; }
    }
}