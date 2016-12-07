using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using BrnMall.Core.DBUtility;
namespace BrnMall.Core.BLL
{
     public class SProcedure
    {
        
         public static DataSet GetAllChileByFun(int ParentUID,int lov, string strwhere)
         {

             return DbHelperSQL.Query(" select * from f_cid("+ParentUID+","+lov+ ") where 1=1 "+ strwhere);
         }
         public static DataSet GetAllParentByFun(int UID,int lov,string strwhere )
         {
             return DbHelperSQL.Query(" select * from f_Parent(" + UID + "," + lov + ") where 1=1 "+ strwhere);
         }
        
    }
    
}
