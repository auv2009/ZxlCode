using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zxl.ConsolePro.Pro;

namespace Zxl.ConsolePro
{
    class Program
    {
        //const string sss = "你好兄弟";

        static void Main(string[] args)
        {
            TestStringIntern();

            Console.Read();
        }

        static void RunCa411Searcher()
        {
            //Console.WriteLine("请输入从excel表格的第几行开始查询？");
            //string str = Console.ReadLine();
            //int fromRowNumber = Convert.ToInt32(str);

            //Console.WriteLine(@"请输入excel的文件路径，例如：E:\doc\table2.xls");
            //string filePath = Console.ReadLine();

            //Ca411Searcher searcher = new Ca411Searcher(filePath, fromRowNumber);
            Ca411Searcher searcher = new Ca411Searcher(@"E:\Download\ming07040722.xls", 965);
            searcher.Search();

            Console.Read();
        }

        static void TestStringIntern()
        {
            string str1 = "ABCD1234";
            string str2 = "ABCD1234";
            string str3 = "ABCD";
            string str4 = "1234";
            string str5 = "ABCD" + "1234";
            string str6 = "ABCD" + str4;
            string str7 = str3 + str4;

            Console.WriteLine("string str1 = \"ABCD1234\";");
            Console.WriteLine("string str2 = \"ABCD1234\";");
            Console.WriteLine("string str3 = \"ABCD\";");
            Console.WriteLine("string str4 = \"1234\";");
            Console.WriteLine("string str5 = \"ABCD\" + \"1234\";");
            Console.WriteLine("string str6 = \"ABCD\" + str4;");
            Console.WriteLine("string str7 = str3 + str4;");

            Console.WriteLine("\nobject.ReferenceEquals(str1, str2) = {0}", object.ReferenceEquals(str1, str2));
            Console.WriteLine("object.ReferenceEquals(str1,  \"ABCD1234\") = {0}", object.ReferenceEquals(str1, "ABCD1234"));

            Console.WriteLine("\nobject.ReferenceEquals(str1, str5) = {0}", object.ReferenceEquals(str1, str5));
            Console.WriteLine("object.ReferenceEquals(str1, str6) = {0}", object.ReferenceEquals(str1, str6));
            Console.WriteLine("object.ReferenceEquals(str1, str7) = {0}", object.ReferenceEquals(str1, str7));
            Console.WriteLine("object.ReferenceEquals(str6, str7) = {0}", object.ReferenceEquals(str6, str7));


            Console.WriteLine("\nobject.ReferenceEquals(str1, string.Intern(str6)) = {0}", object.ReferenceEquals(str1, string.Intern(str6)));
            Console.WriteLine("object.ReferenceEquals(str1, string.Intern(str7)) = {0}", object.ReferenceEquals(str1, string.Intern(str7)));
            
        }
        private static string GetStr()
        {
            return "abc";
        }
    }

    struct STR
    {
        string name;
        Student stu;

        public STR(string nm, Student s)
        {
            this.name = nm;
            this.stu = s;
        }
    }

    class Student
    {
        int Id = 1;
        string Name = "hello";
    }
}