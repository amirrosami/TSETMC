using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class StocksHistory
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string HistDate { get; set; } = null!;
        public int? StatusId { get; set; }
        public int? FristPrice { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? LastPrice { get; set; }
        public int? ClosedPrice { get; set; }
        public long? Volume { get; set; }
        public long? VolAmount { get; set; }
        public int? BuyCountI { get; set; }
        public int? BuyCountN { get; set; }
        public long? BuyIvolume { get; set; }
        public long? BuyNvolume { get; set; }
        public long? SellIvolume { get; set; }
        public long? SellNvolume { get; set; }
        public int? SellCountI { get; set; }
        public int? SellCountN { get; set; }
        public long? StockValue { get; set; }

        public virtual Stock Stock { get; set; } = null!;
    }
}
