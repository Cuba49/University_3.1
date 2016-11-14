using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_lab1
{
    public partial class Form2 : Form
    {
        public Form2(int[] mas)
        {
            InitializeComponent();
            dataGridView1.Columns.Add("", "");
            for (int i = 0; i < mas.Length; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = mas[i];
            }
        }
    }
}
