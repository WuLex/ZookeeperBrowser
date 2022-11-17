using System;
using System.Collections.Generic;
using System.Text;

namespace AllModel.MyOrm.DapperAdapter
{
    public class MysqlAdapter : ISqlAdapter
    {
        public virtual string PagingBuild(ref PartedSql partedSql, object args, long skip, long take)
        {
            var pageSql = $"{partedSql.Raw} LIMIT {take} OFFSET {skip}";
            return pageSql;
        }
    }
}