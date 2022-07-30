using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class Stockswillupdate
    {
        public int StockHistoryId { get; set; }
        public string? I { get; set; }
        public int Id { get; set; }
        public string HistDate { get; set; } = null!;
    }
}
