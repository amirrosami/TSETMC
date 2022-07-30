using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsetmc.Application.Dto.StocksDto
{
   public class StockHistoryDto
    {
        
        public int  Stockid { get; set; }
        public int StatusId { get; set; }
        [MaxLength(8)]
        public string ShamsiDate  { get; set; }
        
        public int FirstPrice { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set; }
        public int LastPrice { get; set; }
        public int ClosedPrice { get; set; }
        public long Volume { get; set; }
        public long VolAmount { get; set; }
        public long Value { get; set; }
    }
}
