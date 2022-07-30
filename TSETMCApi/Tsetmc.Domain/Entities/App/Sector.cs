using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class Sector
    {
        public Sector()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string? SectorNo { get; set; }
        public string SectorName { get; set; } = null!;

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
