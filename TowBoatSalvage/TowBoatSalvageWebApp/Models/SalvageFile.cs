namespace TowBoatSalvageWebApp.Models
{
    public class SalvageFile
    {
        public int Id { get; set; } // Unique File ID
        public string FileName { get; set; } // Actual file name on disk
        public string OriginalName { get; set; } // Original name uploaded by user
        public DateTime UploadedAt { get; set; } // Timestamp
        
        public string FileType { get; set; } // image/png, word/pdf
        public int CellId { get; set; } // Foreign key to SalvageCell
        public SalvageCell Cell { get; set; }
    }
}
