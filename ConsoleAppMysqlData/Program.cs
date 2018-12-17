using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

using Dapper;

namespace ConsoleAppMysqlData
{
    class Program
    {
        static void Main(string[] args)
        {
            var connStr = "server=127.0.0.1;userId=root;pwd=123456;port=3306;database=testdb;SslModel=none;";

            //类似ado.net的旧用法
            //MySqlConnection conn = new MySqlConnection(connStr);
            //MySqlCommand xxxxxx


            //Mysql.Data提供的封装方法
            //ExecuteDataset
            var ds = MySqlHelper.ExecuteDataset(connStr, "select * from users");

            //ExecuteNonQuery执行Update、Delete操作
            var res = MySqlHelper.ExecuteNonQuery(connStr, "update users set Email='11.com' where UserID=1");

            //ExecuteReader
            var reader = MySqlHelper.ExecuteReader(connStr, "select * from users");
            var userList = new List<Users>();
            while (reader.Read())
            {
                var user = new Users();
                user.userID = reader.GetInt32("UserID");
                user.UserNick = reader.GetString("UserNick");
                user.Email = reader.GetString("Email");
                user.LoginIP = reader.GetString("LoginIP");
                userList.Add(user);
            }
            reader.Close();


            //Dapper是对MySqlConnection的一对提高效率使用的封装方法
            MySqlConnection mysqlConn = new MySqlConnection(connStr);
            var userLst = mysqlConn.Query<Users>("select * from users");

        }
    }


    class Users
    {
        public int userID { get; set; }
        public string UserNick { get; set; }
        public string LoginIP { get; set; }
        public string Email { get; set; }
    }
}
