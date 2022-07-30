using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsetmc.Application.Dto.StocksDto
{
    public class StockRealLegalHistoryDto
    {
        public int HistoryId { get; set; }
        [MaxLength(8)]
        public string ShamsiDate { get; set; }
        public string InternalId { get; set; }
        public int? BuyCountI { get; set; }
        public int? BuyCountN { get; set; }
        public long? BuyIvolume { get; set; }
        public long? BuyNvolume { get; set; }
        public long? SellIvolume { get; set; }
        public long? SellNvolume { get; set; }
        public int? SellCountI { get; set; }
        public int? SellCountN { get; set; }
      
    }
}
