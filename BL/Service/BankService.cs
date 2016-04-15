using System.Collections.Generic;
using System.Threading.Tasks;
using BL.Models.BankModels;
using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Mapping;

namespace BL.Service
{
    public class BankService : BaseGetService<Bank, DAL.Data.Bank, Banks, BankFilter>, IBankService
    {
        public async Task<Bank> Get(int id, BankModifier modifier)
        {
            return MapperInstance.Mapper.Map<Bank>(await _instance.Get(id, modifier));
        }

        public async Task<IList<Bank>> Get(BankFilter filter, BankModifier modifier)
        {
            return MapperInstance.Mapper.Map<IList<Bank>>(await _instance.Get(filter, modifier));
        }

        public async Task<IList<Bank>> GetAll(BankModifier modifier)
        {
            return MapperInstance.Mapper.Map<IList<Bank>>(await _instance.GetAll(modifier));
        }
    }
}
