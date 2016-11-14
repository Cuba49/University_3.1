using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] mas = new int[Convert.ToInt32(textBox1.Text)];

            Random r = new Random();
            for (int i = 0; i < mas.Length; i++)
            {
                mas[i] = r.Next(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            }
            double sum = 0;
            Form columnsAndRows = new Form2(mas);
            columnsAndRows.Show();
        }
    }
}
