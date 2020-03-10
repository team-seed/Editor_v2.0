using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class SubForm_setting : Form
    {

        //private Control _F1 = new Control();

        public bool set_ready;
        public SubForm_setting()
        {
            set_ready = false;
            InitializeComponent();
        }

        public void SetData(ref double bpm, ref double offset, ref int beat) {
            bpm = Convert.ToDouble(textBox1.Text);
            offset = Convert.ToDouble(textBox2.Text);
            beat = Convert.ToInt32(textBox3.Text);
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a, b; int c;
            if (double.TryParse(textBox1.Text, out a) && double.TryParse(textBox2.Text, out b)
                && int.TryParse(textBox3.Text, out c))
            {
                if (a >= 0)
                {
                    set_ready = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("BPM must be positive");
                }
            }
            else
                MessageBox.Show("請輸入數字");
        }

    }
}
