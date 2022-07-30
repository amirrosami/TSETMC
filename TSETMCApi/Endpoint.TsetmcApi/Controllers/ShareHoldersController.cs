using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tsetmc.Application.Dto.ShareHoldersDto;
using Tsetmc.Application.Interfaces.IUnitOfWork;
using Tsetmc.Application.Interfaces.Repository;
using Tsetmc.Domain.Entities.App;

namespace Endpoint.TsetmcApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareHoldersController : ControllerBase
    {
        private readonly Iunitofwork _uow;
        private readonly DbSet<ShareHolder> _shareHoldersset;
        private readonly IShareHoldersRepo _shareHoldersRepo;
        
        public ShareHoldersController(Iunitofwork uow,IShareHoldersRepo ishareHolders)
        {
            _uow = uow;
            _shareHoldersset=uow.Set<ShareHolder>();
            _shareHoldersRepo =ishareHolders;
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateShareHolders()
        {
            var updatelist=_uow.Set<GetHoldersUpdateList>().ToList();
            updatelist=updatelist.GetRange(0, 50);
            var shareHolders =await _shareHoldersRepo.GetUpdatedShareHoldersListV2Async(updatelist);


            foreach (var item in shareHolders)
            {
               int personid= _shareHoldersRepo.ChekPersons(item.PersonId);
                if (personid == -1)
                {

                    await _shareHoldersRepo.AddPerson(new AddPersonDto
                    {
                        PersonCode = item.PersonId,
                        PersonName = item.PersonName
                    });

                    personid = _shareHoldersRepo.ChekPersons(item.PersonId);
                }    

            var newPersonId= _shareHoldersRepo.ChekPersons(item.PersonId);
                _shareHoldersset.Add(new ShareHolder
                {
                    StockId = item.StockId,
                    StockAmount = item.StockAmount,
                    PersonId= personid == -1 ? 1 : personid ,
                    IsMain = item.IsMain,
                    HistDate = item.HistDate,
                });

            }
          await  _uow.savechangesAsync();
            return Ok();
        }

    }
}

