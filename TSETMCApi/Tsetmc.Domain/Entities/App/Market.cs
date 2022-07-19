using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class Market
    {
        public Market()
        {
            MarketHistories = new HashSet<MarketHistory>();
        }

        public int Id { get; set; }
        public string? MarketNo { get; set; }
        public string MarketName { get; set; } = null!;

        public virtual ICollection<MarketHistory> MarketHistories { get; set; }
    }
}
