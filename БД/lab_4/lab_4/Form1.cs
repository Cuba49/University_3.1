using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace lab_4
{
    public partial class Form1 : Form
    {
        string[] editTrain;
        int numberOfEditRows;
       
        private MySqlCommand com;
        private MySqlConnection con;
        private MySqlDataReader dr;
        MySqlConnectionStringBuilder mysqlCSB;
        private string queryString;
        DataTable dt = new DataTable();
        private bool isFirst = true;
        public Form1()
        {
            InitializeComponent();
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "127.0.0.1";
            mysqlCSB.Database = "customproducts";
            mysqlCSB.UserID = "root";
            mysqlCSB.Password = "kubovych49";
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
           
            if (isFirst == false)
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dr = null;
                dt = new DataTable();

            }
            
           

            queryString = " select name_Serial, name_City, street, house, flat" +
                          " from store, city, adress" +
                          " where store.FKCity = id_City and adress.FKCity = id_City; ";
            //   queryString = "select * from transports where exists(select * from custom_products where FKTransport = transports.id_Transport);";

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                   
                    using (dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            
                            dt.Load(dr);
                            
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    dataGridView1.Columns.Add("", "");
                                    dataGridView1.Columns[i].HeaderText = dt.Columns[i].Caption;

                                }
                                for (int i = 0; i < dt.Rows.Count; i++)
                                    dataGridView1.Rows.Add();
                           
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
                isFirst = false;
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            int selRowNum = dataGridView1.CurrentCell.RowIndex;
            Edit(selRowNum);
        }

        private void Edit(int numberOfRows)
        {
            editTrain = new string[dataGridView1.ColumnCount];
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, numberOfRows].Style.BackColor = Color.Yellow;
                editTrain[i] = dataGridView1[i, numberOfRows].Value.ToString();
                if (editTrain[i] == "") editTrain[i] = null;
            }

            numberOfEditRows = numberOfRows;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, dataGridView1.RowCount - 2].Style.BackColor = Color.Green;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] train = new string[dataGridView1.ColumnCount];
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, numberOfEditRows].Style.BackColor = Color.White;
                train[i] = dataGridView1[i, numberOfEditRows].Value.ToString();
                
            }
            string queryString = "";
            if (!Test(" select * from store where name_Serial =  '" + train[0] + "'  ;"))
            {
                queryString += " UPDATE store set  name_Serial= '" + train[0] + "' where name_Serial = '" + editTrain[0] + "' ; ";
            }
           
            if (!Test("select * from adress where street = '" + train[2] + "' and house =  '" + train[3] + "' and flat =  '" + train[4] + "'; "))
            {
                queryString += " UPDATE adress set  street= '" + train[2] + "' , house= '" + train[3] + "',flat= '" + train[4] + "' where street = '" + editTrain[2] + "'and house = '" + editTrain[3] + "'and flat = '"+editTrain[4]+"'; ";
            }


            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            dt.Load(dr);
                            
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] train = new string[dataGridView1.ColumnCount];

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                train[i] = dataGridView1[i, dataGridView1.RowCount - 2].Value?.ToString();
            }
            string queryString = "";
            if (!Test("select * from city where name_City = '" + train[1] + "' ;"))
            {
                queryString += " INSERT INTO city (name_City)"
                               + " VALUE('" + train[1] + "');";
            }
            if (!Test(" select * from store where name_Serial =  '" + train[0] + "'  ;"))
            {
                queryString += " INSERT INTO store (name_Serial, FKCity) VALUE('" + train[0] + "', ("
                           + " SELECT city.id_City as id"
                           + " FROM city"
                            + " WHERE name_City = '" + train[1] + "' ));";
            }
            if (!Test("select * from adress where street = '" + train[2] + "' and house = '" + train[3] +
                "' and flat = '" + train[4] + "'; "))
            {
                queryString += " INSERT INTO adress  (street, house, flat, FKCity)"
                           + " VALUE('" + train[2] + "', '" + train[3] + "', '" + train[4] + "', ("
                           + " SELECT city.id_City as id"
                           + " FROM city"
                            + " WHERE name_City = '" + train[1] + "' ));";
            }

            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            dt.Load(dr);
                            for (int i = 0; i < dt.Rows.Count; i++)
                                dataGridView1.Rows.Add();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                            dataGridView1.Refresh();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1[i, dataGridView1.RowCount - 2].Style.BackColor = Color.White;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int selRowNum = dataGridView1.CurrentCell.RowIndex;
            Delete(selRowNum);
            
        }
        private void Delete(int numberOfRows)
        {
            string[] train = new string[dataGridView1.ColumnCount];

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {

                train[i] = dataGridView1[i, numberOfRows].Value?.ToString();
            }
            string queryString = "DELETE FROM adress WHERE street = '" + train[2] + "' and  house = '" + train[3] + "' and  flat = '" + train[4] + "'; ";
            using (MySqlConnection con = new MySqlConnection())
            {
                con.ConnectionString = mysqlCSB.ConnectionString;
                com = new MySqlCommand(queryString, con);
                try
                {
                    con.Open();
                    using (dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {

                            dt.Load(dr);
                            for (int i = 0; i < dt.Rows.Count; i++)
                                dataGridView1.Rows.Add();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                DataRow row = dt.Rows[j];

                                for (int n = 0; n < dt.Columns.Count; n++)
                                {
                                    DataColumn column = dt.Columns[n];

                                    dataGridView1[n, j].Value = row[column];
                                }
                            }
                            dataGridView1.Refresh();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private bool Test(string queryString_1)
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
    }
}
