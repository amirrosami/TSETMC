using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tsetmc.Application.Dto.StocksDto;
using Tsetmc.Application.Interfaces.IUnitOfWork;
using Tsetmc.Application.Interfaces.Repository;
using Tsetmc.Application.Services.StockHistory;
using Tsetmc.Domain.Entities.App;

namespace Endpoint.TsetmcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksHistoryController : ControllerBase
    {
        private readonly Iunitofwork _uow;
        private readonly IStocksHistoryRepo _stockshistoryServices;
        private readonly DbSet<StocksHistory> _stocksHistoriesSet;
        public StocksHistoryController(Iunitofwork uow,IStocksHistoryRepo stocksHistoryServices)
        {
            _uow = uow;
            _stockshistoryServices = stocksHistoryServices;
            _stocksHistoriesSet = uow.Set<StocksHistory>();
        }
        [HttpPost("Update")]
        public IActionResult UpdateStocksHistory()
        {
            var a = _stockshistoryServices.GetAllStockRealLegalHistoryAsync();
            var errorlist=new List<string>();
            int rows=_stockshistoryServices.ResetHistory();
            _uow.savechanges();
            var updatelist = _stockshistoryServices.GetAllStocksHistory(@"F:\tseclient\Adjusted");
            foreach (var item in updatelist)
            {
                if (string.IsNullOrEmpty(item.ShamsiDate))
                {
                    errorlist.Add(item.Stockid.ToString());
                    continue;
                }
                _stocksHistoriesSet.Add(new StocksHistory
                {
                    StockId=item.Stockid,
                    HistDate=item.ShamsiDate,
                    StatusId=item.StatusId,
                    FristPrice=item.FirstPrice,
                    MaxPrice=item.MaxPrice,
                    MinPrice=item.MinPrice,
                    LastPrice=item.LastPrice,
                    ClosedPrice=item.ClosedPrice,
                    VolAmount=item.VolAmount,
                    Volume=item.Volume,
                    StockValue=item.Value,
                });
            }

            _uow.savechanges();


            return Ok();
        }

        [HttpPut("UpdateRealLegalHistory")]
        public async Task<IActionResult> UpdateRealLegalHistory()
        {

            
            List<StockRealLegalHistoryDto> RealLegallist=await _stockshistoryServices.GetAllStockRealLegalHistoryAsync();
            foreach (var item in RealLegallist)
            {
                var Stockhistory = _stockshistoryServices.GetStockHistorybyId(item.HistoryId);
                if (Stockhistory != null)
                {
                    Stockhistory.BuyCountI=item.BuyCountI;
                    Stockhistory.BuyCountN=item.BuyCountN;
                    Stockhistory.SellCountI=item.SellCountI;
                    Stockhistory.SellCountN=item.SellCountN;
                    Stockhistory.BuyIvolume=item.BuyIvolume;
                    Stockhistory.BuyNvolume=item.BuyNvolume;
                    Stockhistory.SellIvolume=item.SellIvolume;
                    Stockhistory.SellNvolume=item.SellNvolume;
                    _stocksHistoriesSet.Update(Stockhistory);
                    await _uow.savechangesAsync();
                }
            }
            
            return Ok();
        }
    }
}
