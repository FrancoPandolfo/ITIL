namespace ITIL.Model
{
    public class IncidentMetricsDto
    {
        public float[] IncidentsPerDay {get;set;}
        public float[] IncidentsPerHour {get;set;}
        public int HourWithMostIncidents {get;set;}
        public string DayWithMostIncidents {get;set;}
        public TimeSpan AvgResolutionTime {get;set;}
    }
}