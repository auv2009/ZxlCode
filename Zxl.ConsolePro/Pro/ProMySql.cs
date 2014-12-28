using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Zxl.ConsolePro.Common;

namespace Zxl.ConsolePro.Pro
{
    public class ProMySql
    {
        public static void ExecuteMySqlPro()
        {
            using (MySqlConnection conn = new MySqlConnection(Constant.MySqlConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select id,name from user";
                    cmd.CommandType = CommandType.Text;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
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
