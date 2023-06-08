namespace ITIL.Model 
{
    public class ProblemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ConfigurationItemId { get; set; }
        public int AssignedUserId {get;set;}
        public string Impact {get;set;}
        public string Priority {get;set;}
        public List<int> IncidentIds { get; set; }

        public ProblemDto()
        {
            IncidentIds = new List<int>();
        }
    }
}