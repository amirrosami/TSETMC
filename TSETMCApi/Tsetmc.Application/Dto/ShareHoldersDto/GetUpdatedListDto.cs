using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsetmc.Application.Dto.ShareHoldersDto
{
    public class GetUpdatedListDto
    {
        public int StockId { get; set; }
        public long StockAmount { get; set; }
        public string HistDate { get; set; } = null!;
        public string? PersonId { get; set; }
        public int? IsMain { get; set; }
        public string? PersonName { get; set; }

    }
}
