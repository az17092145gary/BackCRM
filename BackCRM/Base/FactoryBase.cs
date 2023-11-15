using Dapper;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Reflection;

namespace BackCRM.Base
{
    public abstract class FactoryBase<T>
    {
        public SqlConnection _conn { get; set; }
        public FactoryBase(IConfiguration IConfiguration)
        {
            _conn = new SqlConnection(IConfiguration.GetConnectionString("JINDI"));
        }

        public virtual List<T> getAll(string sql) => _conn.Query<T>(sql).ToList();

        public virtual T getOne(string sql, string id) => _conn.QueryFirst<T>(sql, new { id });
        public dynamic create(T model)
        {
            string sql = getInsertString(model);
            var result = _conn.Execute(sql, model);
            return result;
        }
        public dynamic edit(T model)
        {
            string sql = getUpdateString(model);
            var result = _conn.Execute(sql, model);
            return result;

        }
        public dynamic delete(string sql, string id)=> _conn.Execute(sql, new { id });

        public virtual string getInsertString(T model)
        {
            string[] modelArray = model.ToString().Split('.');
            string sql = $"INSERT {modelArray[modelArray.Length - 1]}";
            PropertyInfo[] properties = model.GetType().GetProperties();
            string values = "";
            foreach (PropertyInfo property in properties)
                if (property.Name != "id")
                {
                    values += "@" + property.Name.ToUpper() + ",";
                }
            values = " Values(" + values.Substring(0, values.Length - 1) + ")";
            return sql + values;
        }
        public virtual string getUpdateString(T model)
        {
            string[] modelArray = model.ToString().Split('.');
            string sql = "UPDATE " + modelArray[modelArray.Length - 1].ToUpper();
            PropertyInfo[] properties = model.GetType().GetProperties();
            string set = " SET ";
            foreach (PropertyInfo property in properties)
                if (property.Name != "id"
                    && (property.GetValue(model) != null))
                    if (property.PropertyType == typeof(int) && (int)property.GetValue(model) == 0)
                        continue;
                    else
                        set += property.Name.ToUpper() + " = @" + property.Name.ToUpper() + ",";
            return sql + set.Substring(0, set.Length - 1) + " WHERE ID = @ID";
        }
    }
}
