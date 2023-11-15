using BackCRM.Base;
using Dapper;

namespace BackCRM.Model
{
    public class BudgetFactory:FactoryBase<Budget>
    {
        public BudgetFactory(IConfiguration configuration) : base(configuration) { }
    }
}
