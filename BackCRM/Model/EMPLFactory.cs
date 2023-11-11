using Dapper;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Numerics;
using BackCRM.Base;
namespace BackCRM.Model
{
    public class EMPLFactory: FactoryBase<EMPL>
    {
        public EMPLFactory(IConfiguration configuration) : base(configuration) { }
    }
}
