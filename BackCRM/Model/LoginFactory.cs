using BackCRM.Base;
using Dapper;
using System.Data.SqlClient;

namespace BackCRM.Model
{
    public class LoginFactory : FactoryBase<EMPL>
    {
        private SqlConnection _sqlConnection;
        public LoginFactory(IConfiguration configuration) : base(configuration) { }
        public EMPL LoginEMPLCheck(string sql, string EMPID, string EMPPWD)
        {
            var result = _conn.QueryFirst<EMPL>(sql, new { EMPID, EMPPWD });
            return result;
        }
    }
}
