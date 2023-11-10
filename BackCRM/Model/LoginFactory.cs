using Dapper;
using System.Data.SqlClient;

namespace BackCRM.Model
{
    public class LoginFactory
    {
        private SqlConnection _sqlConnection;
        public LoginFactory(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("JINDI"));
        }
        public dynamic LoginCheckMember(Dictionary<string, string> account)
        {
            var id = account["id"];
            var pwd = account["pwd"];
            var result = _sqlConnection.Query($"select * from EMPL where EMPID = @id and EMPPWD = @pwd", new { id, pwd }).Count();
            return result;
        }
    }
}
