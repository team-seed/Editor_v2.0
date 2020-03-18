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
        public bool newSection = true;
        public bool remove = false;
        public int SectionIndex;
        public  Label Label__ref;

        public SubForm_setting()
        {
            set_ready = false;
            InitializeComponent();
        }
        public SubForm_setting(ref Label l1,int index,string name, double bpm, double offset, int beat)
        {
            set_ready = false;
            InitializeComponent();
            Label__ref = l1;
            SectionIndex = index;
            textBox1.Text = bpm.ToString();
            textBox2.Text = offset.ToString();
            textBox3.Text = beat.ToString();
            textBox4.Text = name;
            newSection = false;
            Remove.Enabled = true;
            Remove.Visible = true;
        }
        public SubForm_setting(int count)
        {
            set_ready = false;
            InitializeComponent();
            textBox4.Text = "Section_" + count.ToString();
        }

        public void SetData(ref string name,ref double bpm, ref double offset, ref int beat) {
            name = textBox4.Text;
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Remove_Click(object sender, EventArgs e)
        {
            remove = true;
            Close();
        }
    }
}
