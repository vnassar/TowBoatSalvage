using Microsoft.EntityFrameworkCore;
using TowBoatSalvageWebApp.Data;
using TowBoatSalvageWebApp.Models;

namespace TowBoatSalvageWebApp.Services
{
    public class SalvageTableService
    {

        private readonly SalvageDbContext _db;
        private readonly IWebHostEnvironment _env;

        public SalvageTableService(SalvageDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        // get columnds (ordered)
        public async Task<List<SalvageColumn>> GetColumnsAsync()
        {
            return await _db.Columns.OrderBy(c => c.Order).ToListAsync();
        }

        // get all rows with cells and files 
        public async Task<List<SalvageRow>> GetRowsWithCellsAsync()
        {
            return await _db.Rows
                .Include(r => r.Cells)
                    .ThenInclude(cell => cell.Files)
                .ToListAsync();
        }

        /*
         * No Longer using this since switing to EF core, at first using in-memory for initial development
         */
       // public List<SalvageColumn> Columns { get; set; } = new();
        //public List<SalvageRow> Rows { get; set; } = new();

        private int _colIdCounter = 1;

        // ============ Columns =============
        //Add new column
        public async Task<SalvageColumn> AddColumnAsync(string label, string type, int order = 0)
        {
            var col = new SalvageColumn
            {
                Label = label,
                Type = type,
                Order = order
            };
            _db.Columns.Add(col);
            await _db.SaveChangesAsync();
            return col;
        }

        // rename column
        public async Task RenameColumnAsync(int columnId, string newLabel)
        {
            var col = await _db.Columns.FindAsync(columnId);
            if (col != null)
            {
                col.Label = newLabel;
                await _db.SaveChangesAsync();
            }
        }

        // delete column (cascades to cells/files)
        public async Task DeleteColumnAsync(int columnId)
        {
            var col = await _db.Columns.FindAsync(columnId);
            if (col != null)
            {
                _db.Columns.Remove(col);
                await _db.SaveChangesAsync();
            }
        }

        //====================ROWS===================
        //add new row and auto-create empty cell for each 
        public async Task<SalvageRow> AddRowAsync()
        {

            var row = new SalvageRow();
            _db.Rows.Add(row);

            // Optionally, auto-create cells for each columns
            var columns = await _db.Columns.ToListAsync();
            foreach (var col in columns)
            {
                var cell = new SalvageCell
                {
                    Row = row,
                    ColumnId = col.Id,
                   // Value = string.Empty
                   Value = col.Type == "Status" ? "In Progress" : string.Empty
                };
                _db.Cells.Add(cell);
            }

            await _db.SaveChangesAsync();
            return row;
        }

        //delete row (cascades to cells/files)
        public async Task DeleteRowAsync(int rowId)
        {
            var row = await _db.Rows.FindAsync(rowId);
            if (row != null)
            {
                _db.Rows.Remove(row);
                await _db.SaveChangesAsync();
            }
        }

        //===========CELLS=============
        // update (or create) cell value for given row & column
        public async Task UpdateCellValueAsync(int rowId, int columnId, string value)
        {
            var cell = await _db.Cells
                .FirstOrDefaultAsync(c => c.RowId == rowId && c.ColumnId == columnId);

            if (cell == null)
            {
                cell = new SalvageCell
                {
                    RowId = rowId,
                    ColumnId = columnId,
                    Value = value
                };
                _db.Cells.Add(cell);
            }
            else
            {
                cell.Value = value;
            }
            await _db.SaveChangesAsync();
        }

        //==============FILES=================
        public async Task AddFileToCellAsync(int rowId, int columnId, string fileName, string originalName, string fileType)
        {
            var cell = await _db.Cells.FirstOrDefaultAsync(c => c.RowId == rowId && c.ColumnId == columnId);

            if (cell == null)
            {
                cell = new SalvageCell
                {
                    RowId = rowId,
                    ColumnId = columnId,
                    Value = string.Empty
                };
                _db.Cells.Add(cell);
                await _db.SaveChangesAsync();
            }

            var file = new SalvageFile
            {
                FileName = fileName,
                OriginalName = originalName,
                UploadedAt = DateTime.UtcNow,
                FileType = fileType, 
                CellId = cell.Id
            };

            _db.Files.Add(file);
            await _db.SaveChangesAsync();
        }

        //====== TO DO =======
        /*
         * Add delete logic to allow removal of images/files
         */
        public async Task DeleteFileAsync(int fileId)
        {
            var file = await _db.Files.FindAsync(fileId);
            if (file!= null)
            {
                //remove physical file
                var filePath = Path.Combine(_env.WebRootPath, "salvage_uploads", file.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                // remove from database
                _db.Files.Remove(file);
                await _db.SaveChangesAsync();
            }
        }

        //===== Loading data for UI =====


        public async Task<List<SalvageColumn>> GetColumnAsync()
        {
            return await _db.Columns.OrderBy(c => c.Order).ToListAsync();
        }

        public async Task<List<SalvageRow>> GetRowsWithCellAsync()
        {
            return await _db.Rows
                .Include(r => r.Cells)
                    .ThenInclude(cell => cell.Files)
                .ToListAsync();
        }
    }
}
