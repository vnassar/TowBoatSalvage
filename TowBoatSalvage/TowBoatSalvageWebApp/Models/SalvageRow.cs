namespace TowBoatSalvageWebApp.Models
{
    public class SalvageRow
    {
        public int Id { get; set; }

        // Key: ColumnId, Value: Cell content
        public Dictionary<int, string> CellValues { get; set; } = new();
    }
}
