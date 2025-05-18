using TowBoatSalvageWebApp.Models;

namespace TowBoatSalvageWebApp.Services
{
    public class SalvageTableService
    {
        public List<SalvageColumn> Columns { get; set; } = new();
        public List<SalvageRow> Rows { get; set; } = new();

        private int _colIdCounter = 1;

        public void AddColumn(string label, string type)
        {
            Columns.Add(new SalvageColumn
            {
                Id = _colIdCounter++,
                Label = label,
                Type = type
            });

            foreach (var row in Rows)
            {
                row.CellValues.Add(Columns.Last().Id, string.Empty);
            }
        }

        public void AddRow()
        {
            var row = new SalvageRow { Id = Rows.Count + 1 };
            foreach (var col in Columns)
            {
                row.CellValues[col.Id] = string.Empty;
            }
            Rows.Add(row);
        }
    }
}
