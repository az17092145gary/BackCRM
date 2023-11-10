using Dapper;
using System.Data.SqlClient;

namespace BackCRM.Model
{
    public class Amount_DFactory
    {
        private SqlConnection _conn;
        public Amount_DFactory(IConfiguration configuration)
        {
            _conn = new SqlConnection(configuration.GetConnectionString("JINDI"));
        }

        public List<Amount_D> GetAll()
        {
            return _conn.Query<Amount_D>("SELECT * FROM AMOUNT_D").ToList();
        }
    }
}
