using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class MarketHistory
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public string HistDate { get; set; } = null!;
        public string? StatusId { get; set; }
        public int? TotalIndex { get; set; }
        public int? EquivalentIndex { get; set; }
        public int? PriceIndex { get; set; }
        public long? Volume { get; set; }
        public long? VolumeAmount { get; set; }

        public virtual Market Market { get; set; } = null!;
    }
}
