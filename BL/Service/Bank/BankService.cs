using DAL.DataAccess;
using Shared.Filters;
using Shared.Modifiers;
using BL.Models;

namespace BL.Service
{
    public class BankService : BaseModifiableGetService<Bank, DAL.Data.Bank, Banks, BankFilter, BankModifier>, IBankService
    {
    }
}
