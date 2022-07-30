using Microsoft.EntityFrameworkCore;



using System;
using System.Net;
using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tsetmc.Application.Dto.StocksDto;
using Tsetmc.Application.Interfaces.IUnitOfWork;
using Tsetmc.Application.Interfaces.Repository;
using Tsetmc.Domain.Entities.App;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Tsetmc.Application.Services.StockHistory
{
    public class StocksHistoryRepo:IStocksHistoryRepo
    {
        private readonly Iunitofwork _uow;
        private readonly DbSet<StocksHistory> _stocksHistories;
        private readonly DbSet<Stock> _stocks;
        private readonly DbSet<Stockswillupdate> _stockswillupdate;
        public StocksHistoryRepo(Iunitofwork uow)
        {
            _uow = uow;
            _stocksHistories=uow.Set<StocksHistory>();
            _stocks = uow.Set<Stock>();
            _stockswillupdate=uow.Set<Stockswillupdate>();
        }

       

        public StocksHistory GetStockHistorybyId(int id)
        {
          return  _stocksHistories.Where(s => s.Id == id).SingleOrDefault();
        }
        public int GetStockIdByCode(string StockCode)
        {
          
            int id=_stocks.Where(x=>x.StockNo == StockCode).Any() ? _stocks.Where(x => x.StockNo == StockCode).Select(x=>x.Id).First() :-1;
            return id ;
        }

        public List <CsvOutputDto> ReadCsvFiles(string filename)
        {
            var stockhistory = new List<CsvOutputDto>();
            
                var reader = new StreamReader(filename);
            while (!reader.EndOfStream)
            {
                var lineinfo = reader.ReadLine().Split(",");
                if (lineinfo.Length > 0)
                {
                    stockhistory.Add(new CsvOutputDto
                    {
                    ShamsiDate = lineinfo[0],
                    FirstPrice = int.Parse(lineinfo[1].Split(".")[0]),
                    MaxPrice = int.Parse(lineinfo[2].Split(".")[0]),
                    MinPrice = int.Parse(lineinfo[3].Split(".")[0]),
                    LastPrice = int.Parse(lineinfo[4].Split(".")[0]),
                    ClosedPrice = int.Parse(lineinfo[5].Split(".")[0]),
                    Value = long.Parse(lineinfo[6].Split(".")[0]),
                    Volume = long.Parse(lineinfo[7].Split(".")[0]),
                    VolAmount = long.Parse(lineinfo[8].Split(".")[0]),

                    });
                    
                     
                }
            
            }
            return stockhistory;
        }

      

            
        public List<StockHistoryDto> GetAllStocksHistory(string Directory)
        {
                
                DirectoryInfo dir = new DirectoryInfo(Directory);
                FileInfo[] Csvfiles = dir.GetFiles("*.csv");
                List<StockHistoryDto> historylist = new List<StockHistoryDto>();
                
                foreach (FileInfo file in Csvfiles)
                {
                var stockcode = file.Name.Split("-")[0];
                int id = GetStockIdByCode(stockcode);
                if (id != -1)
                {
                    var csvoutputlist = ReadCsvFiles(file.FullName);
                    foreach (var csvoutput in csvoutputlist)
                    {

                        historylist.Add(new StockHistoryDto
                        {
                            Stockid = id,
                            StatusId = 1,
                            ShamsiDate = csvoutput.ShamsiDate,
                            FirstPrice = csvoutput.FirstPrice,
                            MaxPrice = csvoutput.MaxPrice,
                            MinPrice = csvoutput.MinPrice,
                            LastPrice = csvoutput.LastPrice,
                            ClosedPrice = csvoutput.ClosedPrice,
                            Value = csvoutput.Value,
                            VolAmount = csvoutput.VolAmount,
                            Volume = csvoutput.Volume



                        });
                    }
                }
                
                }
                return historylist;

        }


        public int ResetHistory()
        {
       return _uow.TruncateTable();
           
        }

        public async Task<List<StockRealLegalHistoryDto>> GetAllStockRealLegalHistoryAsync()
        {
            var historylist = new List<StockRealLegalHistoryDto>();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://cdn.tsetmc.com/api/ClientType/GetClientTypeHistory/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));

            
            var updatelist = _stockswillupdate.ToList();
          //  updatelist = updatelist.GetRange(0, 400);
            foreach (var item in updatelist)
            {
              string miladidate= Commons.Methods.commonmethod.ConvertDate(item.HistDate, "miladi");
                
                
                    var response = await client.GetAsync(item.I + "/" + miladidate);
                    var x = response.Content.ReadAsStringAsync().Result;
                    JObject resultdata = JObject.Parse(x);
                    var clienttype = resultdata.First.First;
                
               
                historylist.Add(new StockRealLegalHistoryDto
                {
                    HistoryId = item.StockHistoryId,
                    ShamsiDate = item.HistDate,
                    InternalId = (string)clienttype["insCode"],
                    BuyCountI = (int)clienttype["buy_I_Count"],
                    BuyCountN = (int)clienttype["buy_N_Count"],
                    SellCountN = (int)clienttype["sell_N_Count"],

                    SellCountI = (int)clienttype["sell_I_Count"],
                    BuyNvolume = (int)clienttype["buy_N_Volume"],
                    BuyIvolume = (int)clienttype["buy_I_Volume"],
                    SellNvolume = (int)clienttype["sell_N_Volume"],
                    SellIvolume = (int)clienttype["sell_I_Volume"],



                });
            }

           
           
            return historylist;
        }

        //public async Task<List<StockRealLegalHistoryDto>> GetAllStockRealLegalHistoryAsync(string Fromdate,string Todate )
        //{
        //    var client=new HttpClient();
        //    var response=await client.GetAsync("")

        //}
    }
}
