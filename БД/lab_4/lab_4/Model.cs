using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    public delegate void ReturnDBArrayHandler(string[,] db);
    class Model
    {
        DataTable dt = new DataTable();
        MySqlConnectionStringBuilder mysqlCSB;

        public ReturnDBArrayHandler returnDBArrayHandler;

        public Model()
        {

            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "127.0.0.1";
            mysqlCSB.Database = "sport";
            mysqlCSB.UserID = "root";
            mysqlCSB.Password = "vlad462148";




        }
        public string[,] GetTable()
        {
            //   Console.

            string[,] dataBase2DArray = null;
            //       string queryString = "SHOW tables";

            string queryString = "sET SQL_SAFE_UPDATES = 0;" +
                "select  player.FirstName, player.MiddleName,player.SecondName," +
                                " player.YearOfBirth, command.NameOfComand, command.YearOfCreate," +
                                " matches.result, matches.description" +
                               " from player,command, participation,matches" +
                               " where player.FKСommand = command.id and" +
                                " participation.FKСommand = command.id and" +
                                " participation.FKMatch = matches.id" +
                                " group by(SecondName);";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString, con);

                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            dt.Load(dr);
                            dataBase2DArray = new string[dt.Columns.Count, dt.Rows.Count + 1];
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];


                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];
                                    // bd = dt.[n, j];

                                    dataBase2DArray[n, 0] = column.ColumnName;

                                    string s = row.ItemArray[n].ToString();
                                    dataBase2DArray[n, j + 1] = s;
                                    // Console.Write(row[column]);
                                    // Console.Write("\t");
                                }
                                //   Console.WriteLine();
                            }
                        }
                        //  Console.ReadKey();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                con.Close();
                return (dataBase2DArray);
            }
        }
        public void Insert(string[] train)
        {
            string queryString = "";
            if (!Inspection(" select * from matches where result =  '" + train[6] + "' and description = '" + train[7] + "' ;"))
            {
                queryString += " INSERT INTO matches (result, description) VALUE('" + train[6] + "','" + train[7] + "');";
            }
            if (!Inspection("select * from command where NameOfComand = '" + train[4] + "' and YearOfCreate =  " + Int32.Parse(train[5]) + "; "))
            {
                queryString += " INSERT INTO command (NameOfComand, YearOfCreate)"
                               + " VALUE('" + train[4] + "', " + Int32.Parse(train[5]) + ");";
            }
            if (!Inspection("select * from player where FirstName = '" + train[0] + "' and MiddleName = '" + train[1] +
                "' and SecondName = '" + train[2] + "' and YearOfBirth = " + Int32.Parse(train[3]) + "; "))
            {
                queryString += " INSERT INTO player  (FirstName, MiddleName, SecondName, YearOfBirth, FKСommand)"
                           + " VALUE('" + train[0] + "', '" + train[1] + "', '" + train[2] + "', " + Int32.Parse(train[3]) + ", ("
                           + " SELECT command.id as id"
                           + " FROM command"
                            + " WHERE NameOfComand = '" + train[4] + "' and YearOfCreate=" + Int32.Parse(train[5]) + ")); ";
            }
            queryString += " insert into participation(FKMatch, FKСommand)"
                    + " value(("
                    + " SELECT  matches.id as id"
                    + " FROM matches"
                    + " WHERE result = '" + train[6] + "' and description = '" + train[7] + "'), ("

                    + " SELECT  command.id as id"
                    + " FROM command"
                    + " WHERE NameOfComand = '" + train[4] + "' and YearOfCreate = " + Int32.Parse(train[5]) + "));";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {

                        if (dr.HasRows)
                        {

                            dt.Load(dr);

                        }
                        //  Console.ReadKey();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


        }
        private bool Inspection(string queryString_1)
        {
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString_1, con);

                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            con.Close();
                            return true;


                        }
                        else
                        {
                            con.Close();
                            return false;
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                con.Close();


                return false;
            }


        }



        public void DeleteRows(string[] train)
        {
            string queryString = "DELETE FROM player WHERE player.FirstName = '" + train[0] + "' and  player.MiddleName = '" + train[1] + "' and  player.SecondName = '" + train[2] + "' and player.YearOfBirth =" + Int32.Parse(train[3]) + "; ";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {

                        if (dr.HasRows)
                        {

                            dt.Load(dr);

                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }


        public void EditRows(string[] oldTrain, string[] train)
        {

            string queryString = "";
            if (!Inspection(" select * from matches where result =  '" + train[6] + "' and description = '" + train[7] + "' ;"))
            {
                queryString += " UPDATE matches set  result= '" + train[6] + "' , description='" + train[7] + "' where result = '" + oldTrain[6] + "' and description = '" + oldTrain[7] + "'; ";
            }
            else if (train[6] != oldTrain[6] || train[7] != oldTrain[7])
            {
                queryString += " ";
            }
            if (!Inspection("select * from command where NameOfComand = '" + train[4] + "' and YearOfCreate =  " + Int32.Parse(train[5]) + "; "))
            {
                queryString += " UPDATE command set  NameOfComand= '" + train[4] + "' , YearOfCreate= " + Int32.Parse(train[5]) + " where NameOfComand = '" + oldTrain[4] + "' and YearOfCreate = " + Int32.Parse(oldTrain[5]) + "; ";
            }
            if (!Inspection("select * from player where FirstName = '" + train[0] + "' and MiddleName = '" + train[1] + "' and SecondName = '" + train[2] + "' and YearOfBirth = " + Int32.Parse(train[3]) + "; "))
            {
                queryString += " UPDATE player set  FirstName= '" + train[0] + "' , MiddleName= '" + train[1] + "',SecondName= '" + train[2] + "',YearOfBirth= " + Int32.Parse(train[3]) + " where FirstName = '" + oldTrain[0] + "'and MiddleName = '" + oldTrain[1] + "'and SecondName = '" + oldTrain[2] + "'and YearOfBirth = " + Int32.Parse(oldTrain[3]) + "; ";

            }

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                con.Close();
            }
        }
    }
}
