using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class GetHoldersUpdateList
    {
        public string? StockInternalId { get; set; }
        public int Id { get; set; }
        public string HistDate { get; set; } = null!;
    }
}
