using Dapper;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Numerics;

namespace BackCRM.Model
{
    public class MemberFactory
    {
        private string _connectionString = "Data Source=F1M121101N;Initial Catalog=JINDI;Integrated Security=True";
        private SqlConnection _connection;
        public MemberFactory()
        {
            _connection = new SqlConnection(_connectionString);
        }
        public IEnumerable<Member> getAllMember()
        {
            var memlists = _connection.Query<Member>("select * from EMPL");
            return memlists;
        }
        public dynamic getMember(string id)
        {
            var mem = _connection.QueryFirst("select * from EMPL where id = @id", new { id });
            return mem;
        }
        public dynamic createMember(Member member)
        {
            string str = "Insert EMPL (EMPID,EMPNAME,PHONE,EMAIL,DEPTID,BIRTHDAY,A_SYSDT,A_USER)";
            str += "values(@id,@EMPNAME,@PHONE,@EMAIL,@DEPTID,@BIRTHDAY,@A_SYSDT,@A_USER)";
            var result = _connection.Execute(str, new
            {
                id = member.EMPID,
                member.EMPNAME,
                member.PHONE,
                member.EMAIL,
                member.DEPTID,
                member.BIRTHDAY,
                member.A_SYSDT,
                member.A_USER
            });
            return result;
        }
        public dynamic editMember(Dictionary<string, string> member)
        {
            //sqlinject沒檔
            int currentItem = 0;
            string str = "Update EMPL set ";
            foreach (var item in member)
            {
                currentItem++;
                str += $" {item.Key} = '{item.Value}' ";
                if (currentItem < member.Count)
                {
                    str += ", ";
                }
            }
            str += $"where EMPID = '{member["EMPID"]}'";
            var result = _connection.Execute(str);
            return result;
        }
        public dynamic deleteMember(string id)
        {
            string str = "Delete EMPL where EMPID = @id";
            var result = _connection.Execute(str, new { id });
            return result;
        }
    }

}
