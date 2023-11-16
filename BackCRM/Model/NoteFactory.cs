using BackCRM.Base;
using Dapper;
using System.Diagnostics;

namespace BackCRM.Model
{
    public class NoteFactory:FactoryBase<Note>
    {
        public NoteFactory(IConfiguration configuration) : base(configuration) { }
        public dynamic getAll(string sql) 
        {
            var result = _conn.Query(sql);
            return result;
        }
    }
}
