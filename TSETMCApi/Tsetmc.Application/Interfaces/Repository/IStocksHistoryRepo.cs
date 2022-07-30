using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsetmc.Application.Dto.StocksDto;
using Tsetmc.Domain.Entities.App;

namespace Tsetmc.Application.Interfaces.Repository
{
    public interface IStocksHistoryRepo
    {
       List<CsvOutputDto> ReadCsvFiles(string filename);
        List<StockHistoryDto> GetAllStocksHistory(string Directory);
        int ResetHistory();
        Task<List<StockRealLegalHistoryDto>> GetAllStockRealLegalHistoryAsync();
        StocksHistory GetStockHistorybyId(int id);
    }

}
