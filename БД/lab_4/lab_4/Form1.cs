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
        private MySqlCommand com;
        private MySqlConnection con;
        private MySqlDataReader dr;
        MySqlConnectionStringBuilder mysqlCSB;
        private string queryString;
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
            Database();
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Server = "127.0.0.1";
            mysqlCSB.Database = "customproducts";
            mysqlCSB.UserID = "root";
            mysqlCSB.Password = "kubovych49";
            //queryString = "select * from transports where exists (select * from custom_products where FKTransport = transports.id_Transport); ";
            //using (MySqlConnection con = new MySqlConnection())
            //{
            //    con.ConnectionString = mysqlCSB.ConnectionString;
            //    MySqlCommand com = new MySqlCommand(queryString, con);
            //    try
            //    {
            //        con.Open();
            //        using (dr = com.ExecuteReader())
            //        {
            //            if (dr.HasRows)
            //            {

            //                dt.Load(dr);
            //                for (int i = 0; i < dt.Columns.Count; i++)
            //                    dataGridView1.Columns.Add("", "");
            //                for (int i = 0; i < dt.Rows.Count; i++)
            //                    dataGridView1.Rows.Add();
            //                for (int j = 0; j < dt.Rows.Count; j++)
            //                {
            //                    DataRow row = dt.Rows[j];

            //                    for (int n = 0; n < dt.Columns.Count; n++)
            //                    {
            //                        DataColumn column = dt.Columns[n];

            //                        dataGridView1[n, j].Value = row[column];
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }

            //   }
        }

        public void Database()
        {

        }



        private void button1_Click_1(object sender, EventArgs e)
        {

            queryString = " select name_Cargo, masa, name_Transport, roominess, spead" +
                          " from cargo, custom_products, transports" +
                          " where FKCargo = id_Cargo and FKTransport = id_Transport; ";
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
            }
        }

        public void Load()
        {

            com = new MySqlCommand(queryString, con);
            try
            {

                using (dr = com.ExecuteReader())
                {
                    if (dr.HasRows)
                    {

                        dt.Load(dr);
                        for (int i = 0; i < dt.Columns.Count; i++)
                            dataGridView1.Columns.Add("", "");
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
                Console.WriteLine(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            queryString = " select name_Cargo, masa, name_Transport, roominess, spead" +
                          " from cargo, custom_products, transports" +
                          " where FKCargo = id_Cargo and FKTransport = id_Transport; ";
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
            }
        }
        

       
    }
}
