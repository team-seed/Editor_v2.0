using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    
    public partial class Form1 : Form
    {
        class Note {
            public int pos,type,first,last,nextpos,dir;
            public Note (int Position,int Type,int First, int Last) {
               this.pos     =   Position;
               this.type    =   Type;
               this.first   =   First;
               this.last    =   Last;
            }
            public Note(int Position, int Type, int First, int Last, int Extra) : this(Position, Type,  First,  Last) {
                if (Type == 1)      this.nextpos = Extra;
                else if (Type == 2) this.dir     = Extra; 
            }
        };
        List<Note> NoteList;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Height = 10000;
            panel1.ScrollToBottom();

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //畫線
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0),5);
            e.Graphics.DrawLine(pen, 1 * panel1.Width / 4, 0, 1 * panel1.Width / 4, panel1.Height);
            e.Graphics.DrawLine(pen, 2 * panel1.Width / 4, 0, 2 * panel1.Width / 4, panel1.Height);
            e.Graphics.DrawLine(pen, 3 * panel1.Width / 4, 0, 3 * panel1.Width / 4, panel1.Height);
            e.Graphics.DrawLine(pen, 0, panel1.Height * 6 / 7 , panel1.Width, panel1.Height * 6 / 7);

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            
        }
        private void form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
        private void panel2_Click(object sender, EventArgs e)
        {
            Point point   = panel2.PointToClient(Cursor.Position);
           // Note  current = new Note();

            panel1.Refresh();
        }

    }
    public static class PanelExtension
    {
        public static void ScrollToBottom(this Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }
    }
}
