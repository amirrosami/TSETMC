using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class Stock
    {
        public Stock()
        {
            StocksHistories = new HashSet<StocksHistory>();
        }

        public int Id { get; set; }
        public string StockNo { get; set; } = null!;
        public string? StockInternalId { get; set; }
        public string StockName { get; set; } = null!;
        public string StockSymbol { get; set; } = null!;
        public long? StockBvol { get; set; }
        public int? StockEps { get; set; }
        public long? StockAmount { get; set; }
        public int StockSectorId { get; set; }
        public int? StockMarketId { get; set; }
        public int? ImportantLevel { get; set; }

        public virtual Sector StockSector { get; set; } = null!;
        public virtual ICollection<StocksHistory> StocksHistories { get; set; }
    }
}
