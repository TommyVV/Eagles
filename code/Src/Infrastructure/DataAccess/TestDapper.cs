using System.Collections.Generic;
using System.Linq;
using Dapper;
using Eagles.Interface.Infrastructure.DataAccess;
using Ealges.Infrastructure.DataBaseModel.Model;
namespace Eagles.Infrastructure.DataAccess
{
    public class TestDapper:IAreaDataAccess
    {
        private const string connstr = "Server=192.168.50.128;Database=jtly;uid=sa;pwd=you pwd ;";
        
        public List<Area> GetAreas(string id)
        {
            return  new List<Area>();
            //using (var con = new System.Data.SqlClient.SqlConnection(connstr))
            //{
            //    con.Open();
            //    var a = con.Query<Area>("SELECT TOP 1000 [areaId] ,[areaName] FROM[jtly].[dbo].[areaInfo]  where areaId =@id ", new {id=id});
            //    con.Close();
            //    return a.ToList();
            //}
        }
    }
}
