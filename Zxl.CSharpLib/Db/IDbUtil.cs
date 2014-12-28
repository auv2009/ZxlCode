using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Zxl.CSharpLib.Db
{
    public interface IDbUtil
    {
        IDataParameter CreateParameter(string paraName, object value);
        IDataParameter CreateParameter(string paraName, object value, DbType dbType);
        
        bool Execute(string sql, IList<IDataParameter> parameters);

        DataSet GetDataSet(string sql);
        DataSet GetDataSet(string sql, IList<IDataParameter> paras);
        DataSet GetDataSet(string sql, IList<IDataParameter> paras, string tableName);
        DataSet GetDataSet(string columnStr, string tableSte, string whereStr, IList<IDataParameter> whereParas, string orderStr, int pageid, int pageSize);
        DataSet GetDataSet(string columnStr, string tableSte, string whereStr, IList<IDataParameter> whereParas, string orderStr, int pageid, int pageSize, ref int count);

        IDataReader GetDataReader(string sql);
        IDataReader GetDataReader(string sql, IList<IDataParameter> paras);
        IDataReader GetDataReader(string sql, IList<IDataParameter> paras, int pageIndex, int pageSize);
        IDataReader GetDataReader(string sql, IList<IDataParameter> paras, int pageIndex, int pageSize, ref int count);
        IDataReader GetDataReader(string columnStr, string tableSte, string whereStr, IList<IDataParameter> whereParas, string orderStr, int pageid, int pageSize);
        IDataReader GetDataReader(string columnStr, string tableSte, string whereStr, IList<IDataParameter> whereParas, string orderStr, int pageid, int pageSize, ref int count);
        IDataReader GetDataReader(string columnStr, string tableSte, string whereStr, IList<IDataParameter> whereParas, string orderStr, int pageid, int pageSize, ref int count, string orderDired);

        int GetNextVal(string sequenceName);
        int GetScalar(string sql, IList<IDataParameter> paras);
        object GetScalarObject(string sql, IList<IDataParameter> paras);
        bool isTableExist(string tableName);
    }
}
