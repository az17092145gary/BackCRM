using BackCRM.Base;
using Dapper;
using System.Data.SqlClient;

namespace BackCRM.Model
{
    public class Amount_DFactory:FactoryBase<Amount_D>
    {
        public Amount_DFactory(IConfiguration configuration) : base(configuration) { }

    }
}
