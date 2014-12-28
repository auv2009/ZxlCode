using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using Zxl.ConsolePro.Common;

namespace Zxl.ConsolePro.Pro
{
    public class ProSQLite
    {

        public static void ExcuteSQLitePro()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Constant.SQLiteConnectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select id,name from user";
                    cmd.CommandType = CommandType.Text;
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int id = Convert.ToInt32(dr[0]);
                            string name = dr[1].ToString();
                        }
                    }
                }
            }
        }
    }
}
