using Newtonsoft.Json.Linq;
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
        List<Note> NoteList=new List<Note>();
        double bpm, offset;
        int beat;
        bool set_ready;
        SubForm_setting SetForm;
		
        bool isDragging = false;
        bool isLoaded = false;
		
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
                isLoaded = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
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
        int MainPanel_BasePosition; 
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (ProgressBar.Height == 0)
                {
                    int ExtendedHeight = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * 1000) - MainPanel.Height; 
                    MainPanel.Height = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * 1000);
                    MainPanel.Location = new Point(MainPanel.Location.X,MainPanel.Location.Y + ExtendedHeight);
                    MainPanel_BasePosition = MainPanel.Location.Y + ExtendedHeight;
                    //MessageBox.Show(MainPanel_BasePosition.ToString());
                    music_duration.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString;
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                }
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
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
        private void MoveMainPanel(int progressMove) 
        { 
            Point newPoint = MainPanel.Location;
            if (ProgressBar.Height == 0)
            {
                MainPanel.Location = new Point(newPoint.X, MainPanel_BasePosition);
                return;
            }
            int MoveDistance = MainPanel.Height * progressMove / ProgressBar.Height;
            newPoint = new Point(newPoint.X, newPoint.Y + MoveDistance);
            MainPanel.Location = newPoint;
        }
        private void ProgressBar_Bottom_MouseWheel(object sender, MouseEventArgs me)
        {
            int Delta_Y;
            if (!isLoaded) return;   
            if (me.Delta > 0)
            {
                Delta_Y = 2;
                ProgressBar.Height += 2;
                if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
            }
            else
            {
                Delta_Y = -2;
                ProgressBar.Height -= 2;
            }
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
            music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            MoveMainPanel(Delta_Y);
        }
        private void ProgressBar_Bottom_MouseClick(object sender, MouseEventArgs me)
        {
            if (!isLoaded) return;
            int Delta_Y;
            Delta_Y = ProgressBar.Height;
            ProgressBar.Height = ProgressBar_Background.Height - me.Y + 20;
            if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
            Delta_Y = ProgressBar.Height - Delta_Y;
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
            music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            MoveMainPanel(Delta_Y);
        }
        private void ProgressBar_Bottom_MouseDown(object sender, MouseEventArgs me)
        {
            if (!isLoaded) return;
            isDragging = true;
        }
        private void ProgressBar_Bottom_MouseMove(object sender, MouseEventArgs me)
        {
            if (!isLoaded) return;
            if (isDragging)
            {
                int Delta_Y;
                Delta_Y = ProgressBar.Height;
                ProgressBar.Height = ProgressBar_Background.Height - me.Y + 20;
                if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
                Delta_Y = ProgressBar.Height - Delta_Y;
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
                music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                MoveMainPanel(Delta_Y);
            }
        }
        private void ProgressBar_Bottom_MouseUp(object sender, MouseEventArgs me)
        {
            if (!isLoaded) return;
            isDragging = false;
        }

        ///
        // MainPanel
        ///
        /// 令MainPanel 高度為8個Beats
        /// 隨著ProgressBar , 計算出當前Panel上應該要顯示出的畫面 
        /// 
        private void MainPanel_SetHeight()
        {
            //int BeatLength = 60 / bpm * 1000;   // 計算出 1個Beat花多少 ms   
        }
        private void MainPanel_DrawBeatLine(object sender, PaintEventArgs e)
        {
            // System.Drawing.Graphics formGraphics = this.CreateGraphics();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            float x = 90.0F;
            float y = 50.0F;
            for (int i = 0; i < MainPanel.Height; i += 500) {
                string drawString = i.ToString();
                y = i;
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, MainPanel.Height - y, drawFormat);
            }
            drawFont.Dispose();
            drawBrush.Dispose();
        }
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
            //MessageBox.Show((MainPanel.Height - point.Y).ToString());
            MessageBox.Show(point.Y.ToString());
            MainPanel_Background.Refresh();
        }
		private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            MainPanel_DrawBeatLine(sender, e);
        }

        private void MainPanel_Move(object sender, EventArgs e)
        {
            //MessageBox.Show("move");
            MainPanel.Refresh();
        }
		
		///
		// Yabadado
		///
		private void button6_Click(object sender, EventArgs e)
        {
            //create data
            //click
            Note n_c = new Note(533, 0, 0, 1);
            NoteList.Add(n_c);

            //swipe
            Note n_s = new Note(1066, 2, 6, 2, 4);
            NoteList.Add(n_s);

            //hold
            Note n_h1 = new Note(1599, 2, 4, 10, 2033);
            Note n_h2 = new Note(2033, 2, 6, 16, 2566);
            Note n_h3 = new Note(2566, 2, 0, 8, 3099);
            NoteList.Add(n_h1);
            NoteList.Add(n_h2);
            NoteList.Add(n_h3);

            foreach (Note n in NoteList) {
                if(n.type == 0)
                Console.WriteLine(n.pos.ToString() + " " + n.first.ToString() + " "
                        + n.last.ToString() + " " + n.type.ToString());
                if (n.type == 1)
                    Console.WriteLine(n.pos.ToString() + " " + n.first.ToString() + " "
                        + n.last.ToString() + " " + n.type.ToString()+" "+n.nextpos.ToString());
                if (n.type == 2)
                    Console.WriteLine(n.pos.ToString() + " " + n.first.ToString() + " "
                        + n.last.ToString() + " " + n.type.ToString() + " " + n.dir.ToString());
            }

        }
         private void Settings_Click(object sender, EventArgs e)
        {
            SetForm = new SubForm_setting();            
            SetForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetForm_Closing);
            SetForm.Show(this);
        }
        private void SetForm_Closing(object sender, EventArgs e) {
            if (SetForm.set_ready == true)
            {
                SetForm.SetData(ref bpm, ref offset, ref beat);
                MessageBox.Show("Set Done!");
            }
            else {
                MessageBox.Show("Set Failed!");
            }
        }


        private string SetJsonData() {
            string sb;
            foreach (Note n in NoteList) {
                sb = n.pos.ToString() + "," + n.first.ToString() + "," + n.last.ToString() + "," + n.type.ToString();
            }
                

            JObject jObj =
                new JObject(
                    new JProperty("BPM_RANGE", 0),
                    new JProperty("SECTION",
                        new JObject(
                            new JProperty("BEATS", beat),
                            new JProperty("BPM", bpm),
                            new JProperty("NOTES",
                                new JArray(
                                    from nt in NoteList
                                    select new JValue(nt)
                                )
                            ),
                            new JProperty("OFFSET", offset)
                            )));
            return jObj.ToString();
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
