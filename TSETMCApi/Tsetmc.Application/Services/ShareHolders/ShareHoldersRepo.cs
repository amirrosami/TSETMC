using Commons.Methods;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tsetmc.Application.Dto.ShareHoldersDto;
using Tsetmc.Application.Dto.StocksDto;
using Tsetmc.Application.Interfaces.IUnitOfWork;
using Tsetmc.Application.Interfaces.Repository;
using Tsetmc.Domain.Entities.App;

namespace Tsetmc.Application.Services.ShareHolders
{
    public class ShareHoldersRepo : IShareHoldersRepo
    {
        private readonly Iunitofwork _uow;
        private readonly DbSet<Person> _PersonSet;

        public ShareHoldersRepo(Iunitofwork uow)
        {
            _uow = uow;
            _PersonSet = uow.Set<Person>();
        }

        public List<GetUpdatedListDto> GetUpdatedShareHoldersList(List<GetHoldersUpdateList> updatelist)
        {
            var holderlist=new List<GetUpdatedListDto>();
            WebClient wc = new WebClient();
            foreach (var stock in updatelist)
            {
                var miladidate = commonmethod.ConvertDate(stock.HistDate, "miladi");
                string url = "http://cdn.tsetmc.com/api/Shareholder/" + stock.StockInternalId + "/" + miladidate ;
                var str = wc.DownloadString(url);
                var jsonfile = JObject.Parse(str).First;




                foreach (var item in jsonfile.First)
                {
                    holderlist.Add(new GetUpdatedListDto
                    {
                        StockId = stock.Id,
                        HistDate = stock.HistDate,
                        StockAmount = Int64.Parse(item["numberOfShares"].ToString())
                    });
                    
                    
                 
                          
                       
                        
                  
                    //sahamId =,
                    //SahamdarId = item["shareHolderID"].ToString(),
                    //name = item["shareHolderName"].ToString(),
                    //amount = item["numberOfShares"].ToString(),
                    //date = item["dEven"].ToString()


                }



            }

            return holderlist;
        }


        public async Task<List<GetUpdatedListDto>> GetUpdatedShareHoldersListV2Async(List<GetHoldersUpdateList> updatelist)
        {
            var holderlist = new List<GetUpdatedListDto>();

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://cdn.tsetmc.com/api/Shareholder/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var stock in updatelist)
            {
                var erros = new List<Exception>();
                string miladidate = commonmethod.ConvertDate(stock.HistDate, "miladi");
                var response = await client.GetAsync(stock.StockInternalId + "/" + miladidate);

                try
                {

                    var x = response.Content.ReadAsStringAsync().Result;
                    JObject resultdata = JObject.Parse(x);
                    var clienttype = resultdata.First.First;

                    foreach (var holder in clienttype)
                    {
                        holderlist.Add(new GetUpdatedListDto
                        {
                            HistDate = stock.HistDate,
                            StockId = stock.Id,
                            StockAmount = (Int64)holder["numberOfShares"],
                            PersonId=(string)holder["shareHolderID"],
                            PersonName=(string)holder["shareHolderName"]
                        });



                    }

                }
                catch (Exception e)
                {
                    erros.Add(e);
                    
                }

               

            }

            return holderlist;
        }



        public int ChekPersons(string personcode)
        {
            return  _PersonSet.Where(x=>x.PersonCode == personcode).Any() ? _PersonSet.Where(x=>x.PersonCode==personcode).Select(x=>x.Id).SingleOrDefault() : -1 ;
        }

        public async Task<int> AddPerson(AddPersonDto model)
        {

            _PersonSet.Add(new Person
            {
                PersonCode = model.PersonCode,
                PersonName = model.PersonName,
            });
           return await _uow.savechangesAsync();
        }

    }
}

