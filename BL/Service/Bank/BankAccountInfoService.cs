using DAL.DataAccess;
using Shared.Filters;
using BL.Models;

namespace BL.Service
{
    public class BankAccountInfoService : BaseCrudService<BankAccountInfo, DAL.Data.BankAccountInfo, BankAccountInfoes, BaseFilter>, IBankAccountInfoService
    {
    }
}
