using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_lab2
{
    public partial class Form1 : Form
    {
        public bool isOn = true;
        private int position;
        int mass;
        private bool isRotate = false;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            position = 1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            isOn = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            isOn = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isRotate == true) MessageBox.Show("Перед тем как снова выполнить поворот необходимо поднять груз!");
            else
            {
                position += -1;
                if (position == 0) position = 4;
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox1.Refresh();
                isRotate = true;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isRotate == true) MessageBox.Show("Перед тем как снова выполнить поворот необходимо поднять груз!");
            else
            {
                position += 1;
                if (position == 5) position = 1;
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Refresh();
                isRotate = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isRotate = false;
            try
            {
                mass = Convert.ToInt32(textBox5.Text);
                if (mass < 1) MessageBox.Show("Некоректно введено значение! Маса должна быть в пределах от 1 до 200 Кг.");
                else if (mass > 200)
                    MessageBox.Show("Некоректно введено значение! Маса должна быть в пределах от 1 до 200 Кг.");
                else AddMass(mass);
            }
            catch (Exception)
            {
                MessageBox.Show("Некоректно введено значение! Маса должна быть в пределах от 1 до 200 Кг.");
                textBox5.Text = "";
            }
            
        }

        void AddMass(int mass)
        {
            if (position == 1) label1.Text = (Convert.ToInt32(label1.Text) + mass).ToString();
            if (position == 2) label2.Text = (Convert.ToInt32(label2.Text) + mass).ToString();
            if (position == 3) label3.Text = (Convert.ToInt32(label3.Text) + mass).ToString();
            if (position == 4) label4.Text = (Convert.ToInt32(label4.Text) + mass).ToString();
        }
    }
}
