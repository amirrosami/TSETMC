using System;
using System.Collections.Generic;

namespace Tsetmc.Domain.Entities.App
{
    public partial class ShareHolder
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public long StockAmount { get; set; }
        public string HistDate { get; set; } = null!;
        public int? PersonId { get; set; }
        public int? IsMain { get; set; }

        public virtual Person? Person { get; set; }
    }
}
