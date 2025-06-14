namespace TowBoatSalvageWebApp.Models
{
    public class SalvageRow
    {
        public int Id { get; set; }

        // Key: ColumnId, Value: Cell content

        /*
         * EF Core cant map dictionaries or complex types therefor commenting these out
         */
       // public Dictionary<int, List<string>> CellFiles { get; set; } = new();
        //public Dictionary<int, string> CellValues { get; set; } = new();

        // Navigation property: a row has many cells (once for each column)
        public ICollection<SalvageCell> Cells { get; set; } = new List<SalvageCell>();
    }
}
