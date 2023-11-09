using Dapper;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Numerics;

namespace BackCRM.Model
{
    public class MemberService
    {
        public string connectionString = "Data Source=F1M121101N;Initial Catalog=JINDI;Integrated Security=True";
        public MemberService()
        {
        }
        public IEnumerable<Member> getAllMember(SqlConnection connection)
        {
            var memlists = connection.Query<Member>("select * from EMPL");
            return memlists;
        }

        public dynamic getMember(string id, SqlConnection connection)
        {
            var mem = connection.QueryFirst("select * from EMPL where id = @id", new { id });
            return mem;
        }
        public void createMember(Member member, SqlConnection connection)
        {
            string str = "Insert EMPL (EMPID,EMPNAME,PHONE,EMAIL,DEPTID,BIRTHDAY,A_SYSDT,A_USER)";
            str += "values(@id,@EMPNAME,@PHONE,@EMAIL,@DEPTID,@BIRTHDAY,@A_SYSDT,@A_USER)";
            connection.Execute(str, new
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
        }
        public void editMember(Dictionary<string, string> member, SqlConnection connection)
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
            connection.Execute(str);
        }
        public void deleteMember(string id, SqlConnection connection)
        {
            string str = "Delete EMPL where EMPID = @id";
            connection.Execute(str, new { id });
        }
    }

}
