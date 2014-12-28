using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zxl.ConsolePro.Common
{
    public class Constant
    {
        public const string OracleConnectionString = "Data Source=oradb;User ID=db_user;Password=user_db;Persist Security Info=True;Unicode=True";
        //internal static string CONN_STR = "Data Source=testdb;User ID=testdb;Password=testdb;Persist Security Info=True;Unicode=True";
        public const string MySqlConnectionString = @"Server=127.0.0.1;Port=3306;Database=db;Uid=root;Pwd=123456;";
        public const string SQLiteConnectionString = @"Data Source=E:\ZxlDB\zxl.sqlite;Version=3;UTF8Encoding=True;";
    }
}
