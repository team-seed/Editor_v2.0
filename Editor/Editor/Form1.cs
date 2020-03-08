using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

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
        bool isDragging = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void form1_SizeChanged(object sender, EventArgs e)
        {
            MainPanel_Background.Refresh();
        }

        ///
        // Music Control
        ///
        private void SelectMusic_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.RestoreDirectory = true;

            if (file.ShowDialog() == DialogResult.OK) {
                axWindowsMediaPlayer1.URL = file.FileName;
                music.Text = file.FileName;
               
            }

        }
        private void PlayPause_Click(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused ||
                    axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsReady
                )
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped) 
            {
                ProgressBar.Height = 0;
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }

        }
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                music_duration.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString;
                if (ProgressBar.Height != 0)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
                }
                timer1.Start();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
            else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                axWindowsMediaPlayer1.Ctlcontrols.stop();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                ProgressBar.Height = Convert.ToInt32(Convert.ToDouble(ProgressBar_Background.Height) * axWindowsMediaPlayer1.Ctlcontrols.currentPosition / axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration);
                music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            }
        }
        ///
        // MusicProgressBar
        ///

        private void ProgressBar_Bottom_MouseWheel(object sender, MouseEventArgs me)
        {
            if (me.Delta > 0)
            {
                ProgressBar.Height += 2;
                if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
            }
            else
            {
                ProgressBar.Height -= 2;
            }
        }
        private void ProgressBar_Bottom_MouseClick(object sender, MouseEventArgs me)
        {
            ProgressBar.Height = ProgressBar_Background.Height - me.Y + 20;
            if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
            music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
        }
        private void ProgressBar_Bottom_MouseDown(object sender, MouseEventArgs me)
        {
            isDragging = true;
        }
        private void ProgressBar_Bottom_MouseMove(object sender, MouseEventArgs me)
        {
            if (isDragging)
            {
                ProgressBar.Height = ProgressBar_Background.Height - me.Y + 20;
                if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
                music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            }
        }
        private void ProgressBar_Bottom_MouseUp(object sender, MouseEventArgs me)
        {
            isDragging = false;
        }
        
        ///
        // MainPanel
        private void MainPanel_Background_Paint(object sender, PaintEventArgs e)
        {
            //畫線
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 0, 0, 0),5);
            e.Graphics.DrawLine(pen, 1 * MainPanel_Background.Width / 4, 0, 1 * MainPanel_Background.Width / 4, MainPanel_Background.Height);
            e.Graphics.DrawLine(pen, 2 * MainPanel_Background.Width / 4, 0, 2 * MainPanel_Background.Width / 4, MainPanel_Background.Height);
            e.Graphics.DrawLine(pen, 3 * MainPanel_Background.Width / 4, 0, 3 * MainPanel_Background.Width / 4, MainPanel_Background.Height);
            e.Graphics.DrawLine(pen, 0, MainPanel_Background.Height * 6 / 7 , MainPanel_Background.Width, MainPanel_Background.Height * 6 / 7);

            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            // e.Graphics.FillEllipse(myBrush, new Rectangle(point.X, point.Y, 20, 20));
        }
        private void MainPanel_Click(object sender, EventArgs e)
        {
            Point point   = MainPanel.PointToClient(Cursor.Position);
           // Note  current = new Note();

            MainPanel_Background.Refresh();
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
