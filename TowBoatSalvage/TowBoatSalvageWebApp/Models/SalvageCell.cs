namespace TowBoatSalvageWebApp.Models
{
    public class SalvageCell
    {
        public int Id { get; set; } // Unique cell ID
        public int RowId { get; set; } // Foreign key to Salvage Row
        public SalvageRow Row { get; set; }

        public int ColumnId { get; set; } // Foreign key to SalvageColumn
        public SalvageColumn Column { get; set; }

        public string Value { get; set; } // Value for Text/Status/Number/Date
        public ICollection<SalvageFile> Files { get; set; } // Files (for file upload columns)
    }
}
