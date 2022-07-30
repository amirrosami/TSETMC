using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsetmc.Application.Dto.ShareHoldersDto;
using Tsetmc.Application.Dto.StocksDto;
using Tsetmc.Domain.Entities.App;

namespace Tsetmc.Application.Interfaces.Repository
{
    public interface IShareHoldersRepo
    {
        List<GetUpdatedListDto> GetUpdatedShareHoldersList(List<GetHoldersUpdateList> updatelist);
        Task<List<GetUpdatedListDto>> GetUpdatedShareHoldersListV2Async(List<GetHoldersUpdateList> updatelist);
        int ChekPersons(string personcode);
        Task<int> AddPerson(AddPersonDto model);
    }
}
