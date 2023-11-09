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
        
    }
}
