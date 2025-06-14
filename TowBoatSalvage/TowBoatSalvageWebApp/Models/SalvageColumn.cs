namespace TowBoatSalvageWebApp.Models
{
    public class SalvageColumn
    {
        public int Id { get; set; } // Unique Column ID
        public string Label { get; set; } // Column label
        public string Type { get; set; } // "Text", "Status", "Date", etc.
        public int Order { get; set; } // Column order for display

        public ICollection<SalvageCell> Cells { get; set; } // Navigation property for cell values
    }
}
