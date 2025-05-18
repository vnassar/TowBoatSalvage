namespace TowBoatSalvageWebApp.Models
{
    public class SalvageColumn
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; } // "Text", "Status", "Date", etc.
    }
}
