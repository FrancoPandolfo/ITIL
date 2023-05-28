namespace ITIL.Model 
{
    public class ChangeDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ConfigurationItemId { get; set; }
    }
}