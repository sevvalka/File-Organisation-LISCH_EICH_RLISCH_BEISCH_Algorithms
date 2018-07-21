using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LISCH_EICH_RLISCH__BEISCH_Algorithms
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            int int_text = Convert.ToInt32(text);
            if (int_text > 0 && int_text <= 700) {
                Form3 alg = new Form3(int_text);
                this.Hide();
                alg.Show();
            }
            else
            {
                MessageBox.Show(" sayı 0-700 arasında ve pozitif olmalı");
            }
                

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
