using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;

namespace Zxl.CSharpLib.Db
{
    public class OracleUtil : IDbUtil
    {
        private String connectionString;

        public OracleUtil(String connectionStr)
        {
            this.connectionString = connectionStr;
        }

        public IDataParameter CreateParameter(String parameterName, Object value)
        {
            return new OracleParameter(parameterName, value);
        }
        public IDataParameter CreateParameter(String parameterName, Object value, DbType dbType)
        {
            return new OracleParameter
            {
                ParameterName = parameterName.Replace("@", ""),
                Value = value,
                DbType = dbType
            };
        }
        
        public Boolean Execute(String sql, IList<IDataParameter> paras)
        {
            try
            {
                Int32 result = -1;
                using (OracleConnection conn = new OracleConnection(this.connectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql.Replace("@", ":");
                        foreach (OracleParameter para in paras)
                        {
                            cmd.Parameters.AddWithValue(para.ParameterName, para.Value);
                        }
                        result = cmd.ExecuteNonQuery();
                    }
                }
                return result > 0;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public DataSet GetDataSet(String sql)
        {
            return GetDataSet(sql, null, "ResultInfo");
        }
        public DataSet GetDataSet(String sql, IList<IDataParameter> paras)
        {
            return GetDataSet(sql, paras, "ResultInfo");
        }
        public DataSet GetDataSet(String sql, IList<IDataParameter> paras, String tableName)
        {
            try
            {
                DataSet ds = new DataSet();
                using (OracleConnection conn = new OracleConnection(this.connectionString))
                {
                    conn.Open();
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql.Replace("@", ":");
                        if (paras != null)
                        {
                            foreach (OracleParameter para in paras)
                            {
                                cmd.Parameters.AddWithValue(para.ParameterName, para.Value);
                            }
                        }
                        OracleDataAdapter adp = new OracleDataAdapter(cmd);
                        adp.Fill(ds, tableName);
                    }
                }
                return ds;
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public DataSet GetDataSet(String columnStr, String tableStr, String whereStr, IList<IDataParameter> paras, String orderStr, int pageid, int pageSize)
        {
            String sql;
            try
            {
                if (pageid >= 0 && pageSize >= 0)
                {
                    int StartIndex = (pageid) * pageSize + 1;
                    int EndIndex = (pageid + 1) * pageSize;
                    sql = string.Format(@"
select * from 
        (select rownum rn, tablea.* 
           from (select {0} from {1} where {2} order by {3}) tablea 
           {4}) 
where rn>={5}", 
              columnStr, tableStr, whereStr, orderStr, 
              (EndIndex > 0 ? "where rownum<=" + EndIndex : ""), StartIndex);
                    //sql = "select * from (select rownum rn, tablea.* from (select " + columnStr + " from " + tableStr + " where " + whereStr + " order by " + orderStr + ") tablea " + (EndIndex > 0 ? "where rownum<=" + EndIndex : "") + ") where rn>=" + StartIndex;
                }
                else
                {
                    sql = "SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + " ORDER BY " + orderStr;
                }
                return this.GetDataSet(sql, paras, "ResultInfo");
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public DataSet GetDataSet(String columnStr, String tableStr, String whereStr, IList<IDataParameter> paras, String orderStr, int pageid, int pageSize, ref int count)
        {
            String sql;
            try
            {
                if (pageid >= 0 && pageSize >= 0)
                {
                    int StartIndex = (pageid) * pageSize + 1;
                    int EndIndex = (pageid + 1) * pageSize;
                    sql = "select * from (select rownum rn, tablea.* from (select " + columnStr + " from " + tableStr + " where " + whereStr + " order by " + orderStr + ") tablea " + (EndIndex > 0 ? "where rownum<=" + EndIndex : "") + ") where rn>=" + StartIndex;
                }
                else
                    sql = "SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + " ORDER BY " + orderStr;
                count = this.GetScalar("SELECT count(*) FROM (SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + ")", paras);
                return this.GetDataSet(sql, paras, "ResultInfo");
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public IDataReader GetDataReader(String sql)
        {
            return GetDataReader(sql, null);
        }
        public IDataReader GetDataReader(String sql, IList<IDataParameter> paras)
        {
            try
            {
                OracleConnection con = new OracleConnection(this.connectionString);
                con.Open();
                OracleCommand com = con.CreateCommand();
                com.CommandText = sql.Replace("@", ":");
                if (paras != null)
                {
                    foreach (OracleParameter para in paras)
                    {
                        com.Parameters.AddWithValue(para.ParameterName, para.Value);
                    }
                }
                return com.ExecuteReader();
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public IDataReader GetDataReader(String sql, IList<IDataParameter> paras, int pageIndex, int pageSize)
        {
            string mySql = string.Empty;
            try
            {
                if (pageIndex >= 0 && pageSize >= 0)
                {
                    long low = (pageIndex) * pageSize + 1;
                    long high = (pageIndex + 1) * pageSize;

                    mySql = @" select * from (   
                                        select rownum rn, tableA.* 
                                          from ( " + sql + ") tableA " +
                                        (high > 0 ? "where rownum <= " + high : string.Empty) +
                                @") where rn >= " + low;
                }
                return this.GetDataReader(mySql, paras);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public IDataReader GetDataReader(String sql, IList<IDataParameter> paras, int pageIndex, int pageSize, ref int count)
        {
            string mySql = string.Empty;
            try
            {
                if (pageIndex >= 0 && pageSize >= 0)
                {
                    long low = (pageIndex) * pageSize + 1;
                    long high = (pageIndex + 1) * pageSize;

                    mySql = @"  select * from (   
                                        select rownum rn, tableA.* 
                                        from ( " + sql + ") tableA " +
                                        (high > 0 ? "where rownum <= " + high : string.Empty) +
                                @") where rn >= " + low;
                }

                string countSql = string.Format(@"select count(*) from ({0})", sql);
                count = Convert.ToInt32(this.GetScalar(countSql, paras));
                return this.GetDataReader(mySql, paras);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public IDataReader GetDataReader(String columnStr, String tableStr, String whereStr, IList<IDataParameter> paras, String orderStr, int pageid, int pageSize)
        {
            String SqlCmd = "";
            try
            {
                if (pageid >= 0 && pageSize >= 0)
                {
                    long StartIndex = (pageid) * pageSize + 1;
                    long EndIndex = (pageid + 1) * pageSize;
                    SqlCmd = "select * from (select rownum rn, tablea.* from (select " + columnStr + " from " + tableStr + " where " + whereStr + " order by " + orderStr + ") tablea " + (EndIndex > 0 ? "where rownum<=" + EndIndex : "") + ") where rn>=" + StartIndex;
                }
                else
                    SqlCmd = "SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + " ORDER BY " + orderStr;
                return this.GetDataReader(SqlCmd, paras);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public IDataReader GetDataReader(String columnStr, String tableStr, String whereStr, IList<IDataParameter> paras, String orderStr, int pageid, int pageSize, ref int count)
        {
            String SqlCmd = "";
            try
            {
                if (pageid >= 0 && pageSize >= 0)
                {
                    long StartIndex = (pageid) * pageSize + 1;
                    long EndIndex = (pageid + 1) * pageSize;
                    SqlCmd = "select * from (select rownum rn, tablea.* from (select " + columnStr + " from " + tableStr + " where " + whereStr + " order by " + orderStr + ") tablea " + (EndIndex > 0 ? "where rownum<=" + EndIndex : "") + ") where rn>=" + StartIndex;
                }
                else
                    SqlCmd = "SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + " ORDER BY " + orderStr;
                count = this.GetScalar("SELECT count(*) FROM (SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + ")", paras);
                return this.GetDataReader(SqlCmd, paras);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        public IDataReader GetDataReader(String columnStr, String tableStr, String whereStr, IList<IDataParameter> paras, String orderStr, int pageid, int pageSize, ref int count, string orderDired)
        {
            String SqlCmd = "";
            try
            {
                if (pageid >= 0 && pageSize >= 0)
                {
                    long StartIndex = (pageid) * pageSize + 1;
                    long EndIndex = (pageid + 1) * pageSize;
                    SqlCmd = "select * from (select rownum rn, tablea.* from (select " + columnStr + " from " + tableStr + " where " + whereStr + " order by " + orderStr + " " + orderDired + ") tablea " + (EndIndex > 0 ? "where rownum<=" + EndIndex : "") + ") where rn>=" + StartIndex;
                }
                else
                    SqlCmd = "SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + " ORDER BY " + orderStr;
                count = this.GetScalar("SELECT count(*) FROM (SELECT " + columnStr + " FROM " + tableStr + " WHERE " + whereStr + ")", paras);
                return this.GetDataReader(SqlCmd, paras);
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        
        public int GetNextVal(string sequenceName)
        {
            int next_id = -1;
            using (OracleConnection con = new OracleConnection(this.connectionString))
            {
                con.Open();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT " + sequenceName + ".nextval FROM DUAL";
                    Object next = cmd.ExecuteScalar();
                    next_id = Convert.ToInt32(next);
                }
            }
            return next_id;
        }
        public int GetScalar(String sql, IList<IDataParameter> paras)
        {
            object obj = this.GetScalarObject(sql, paras);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        public Object GetScalarObject(String sql, IList<IDataParameter> paras)
        {
            try
            {
                Object result = null;
                using (OracleConnection con = new OracleConnection(this.connectionString))
                {
                    con.Open();
                    using (OracleCommand com = con.CreateCommand())
                    {
                        com.CommandText = sql.Replace("@", ":");
                        if (paras != null)
                        {
                            foreach (OracleParameter para in paras)
                            {
                                com.Parameters.AddWithValue(para.ParameterName, para.Value);
                            }
                        }
                        result = com.ExecuteScalar();
                    }
                }
                return result;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        public Boolean isTableExist(String tableName)
        {
            string selStr = "SELECT COUNT(*) FROM TAB WHERE upper(TNAME)=upper(:tablename)";
            IList<IDataParameter> paras = new List<IDataParameter>();
            OracleParameter para = new OracleParameter();
            para.ParameterName = ":tablename";
            para.Value = tableName;
            para.DbType = DbType.String;
            paras.Add(para);
            return this.GetScalar(selStr, paras) > 0;
        }
        
        /// <summary>
        /// 批量插入或更新，事务控制
        /// </summary>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sqlList">sql列表</param>
        public void SaveWithTransaction(string connStr, List<string> sqlList)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                OracleTransaction trans = conn.BeginTransaction();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        foreach (var sql in sqlList)
                        {
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}