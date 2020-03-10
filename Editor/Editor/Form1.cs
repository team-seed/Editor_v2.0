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
            public int pos, type, first, last, nextpos, dir;
            public Note(int Position, int Type, int First, int Last) {
                this.pos = Position;
                this.type = Type;
                this.first = First;
                this.last = Last;
            }
            public Note(int Position, int Type, int First, int Last, int Extra) : this(Position, Type, First, Last) {
                if (Type == 1) this.nextpos = Extra;
                else if (Type == 2) this.dir = Extra;
            }
        };
        List<Note> NoteList = new List<Note>();
        double bpm, offset;
        int beat;
        bool data_is_ready = false;
        SubForm_setting SetForm;
        Graphics BackGraphics;
        Bitmap backBmp;

        bool isDragging = false;
        bool isLoaded = false;

        public Form1()
        {
            InitializeComponent();
            backBmp = new Bitmap(this.MainPanel.Width, this.MainPanel.Height);
            BackGraphics = Graphics.FromImage(backBmp);
            
            this.SetStyle(ControlStyles.UserPaint |
                            ControlStyles.AllPaintingInWmPaint |
                            ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            
        }
        private void form1_SizeChanged(object sender, EventArgs e)
        {
            MainPanel_Background.Refresh();
        }

        ///
        // Music Control
        ///
        bool First_Setting = false;
        private void SelectMusic_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.RestoreDirectory = true;

            if (file.ShowDialog() == DialogResult.OK) {
                axWindowsMediaPlayer1.URL = file.FileName;
                music.Text = file.FileName;
                string [] temp = music.Text.Split('\\');
                music.Text = temp[temp.Length - 2];

                First_Setting = true;
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
        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                if (First_Setting)
                {
                    music_duration.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString;
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                    First_Setting = false;
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
                Refresh_Layout();
            }
        }
        
        ///
        // MusicProgressBar
        ///

        private void Refresh_Layout() 
        {
            MainPanel.Refresh();
            music_position.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            music_position.Text += "\n" + (axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000).ToString();
        }
        private void ProgressBar_Bottom_MouseWheel(object sender, MouseEventArgs me)
        {
            if (!isLoaded) return;   
            if (me.Delta > 0)
            {
                ProgressBar.Height += 2;
                if (ProgressBar.Height > ProgressBar_Background.Height) ProgressBar.Height = ProgressBar_Background.Height;
            }
            else
            {
                ProgressBar.Height -= 2;
            }
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
            Refresh_Layout();
        }
        private void ProgressBar_Bottom_MouseClick(object sender, MouseEventArgs me)
        {
            if (!isLoaded) return;
            ProgressBar.Height = ProgressBar_Background.Height - me.Y + 20;
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
            Refresh_Layout();
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
                ProgressBar.Height = ProgressBar_Background.Height - me.Y + 20;
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration * ProgressBar.Height / ProgressBar_Background.Height;
                Refresh_Layout();
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
        /// 令MainPanel 高度為 3000 , 顯示出的範圍約700ms 
        /// 隨著ProgressBar , 計算出當前Panel上應該要顯示出的畫面 
        /// 
        class Beatline 
        {
            public  Beatline(int Time,int Y) 
            {
                this.time = Time;
                this.y = Y;
            }
            public int time, y;
        };
        List<Beatline> Current_BeatLines = new List<Beatline>();        // 紀錄BeatLine在圖上的Y值 及 時間
        private void MainPanel_DrawBeatLine(object sender, PaintEventArgs e)
        {
            if (!data_is_ready) return;
            if (bpm == 0) return;
            int BeatLength = Convert.ToInt32( 60 / bpm * 1000);
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 0, 0, 0), 5);
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            float x = 0.0F;
            float y;

            Current_BeatLines.Clear();
            int cur_pos = Convert.ToInt32(axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000);
            for (int i = 2900; i > 0; i -= BeatLength) {
                int Cur_Line = Convert.ToInt32(cur_pos / BeatLength) * BeatLength + 2900 - i;
                string drawString = Cur_Line.ToString();

                if (beat != 0 && (Cur_Line / BeatLength) % beat == 0)
                {
                    pen.Color = System.Drawing.Color.FromArgb(180, 40, 100, 100);
                    pen.Width = 8;
                }
                else 
                {
                    pen.Color = System.Drawing.Color.FromArgb(255, 0, 0, 0);
                    pen.Width = 5;
                }
                y = i;
                Current_BeatLines.Add( new Beatline( Cur_Line, Convert.ToInt32(cur_pos % BeatLength + y)) );
                e.Graphics.DrawLine(pen, 0, cur_pos % BeatLength + y, MainPanel.Width, cur_pos % BeatLength + y);

                int index = NoteList.FindIndex(x1 => x1.pos == Cur_Line);
                Console.WriteLine("DrawNoteIndex:" + index.ToString());
                if (index >= 0)
                {
                    int l = NoteList[index].first;
                    int r = NoteList[index].last;
                    Console.WriteLine("CurrentTime: " + NoteList[index].pos.ToString() + " left:" + l.ToString() + 
                        " right:" + r.ToString());
                    ///
                    // 畫出選取範圍
                    ///
                    pen.Color = System.Drawing.Color.Coral;
                    e.Graphics.DrawLine(pen, l * MainPanel.Width / 16, cur_pos % BeatLength + y, r * MainPanel.Width / 16, cur_pos % BeatLength + y);
                    ///
                    //  畫出Note
                    ///
                    switch (NoteList[index].type) 
                    {
                        case 0:
                            e.Graphics.DrawString("0", drawFont, drawBrush, (l+r) / 2 * MainPanel.Width, cur_pos % BeatLength + y - 3, drawFormat);
                            break;
                        case 1:
                            e.Graphics.DrawString("1", drawFont, drawBrush, (l + r) / 2 * MainPanel.Width, cur_pos % BeatLength + y - 3, drawFormat);
                            break;
                        case 2:
                            e.Graphics.DrawString("2", drawFont, drawBrush, (l + r) / 2 * MainPanel.Width, cur_pos % BeatLength + y - 3, drawFormat);
                            break;
                    }
                }
                
                e.Graphics.DrawString(drawString, drawFont, drawBrush, x, cur_pos % BeatLength + y + 2, drawFormat);
            }

            pen.Dispose();
            drawFormat.Dispose();
            drawFont.Dispose();
            drawBrush.Dispose();
        }
      
        private void MainPanel_Background_Paint(object sender, PaintEventArgs e)
        {
            //if (HasBackground) return;
            //HasBackground = true;
            //畫線


            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 0, 0, 0),5);
            int mpw = MainPanel_Background.Width;
            int mph = MainPanel_Background.Height;
            e.Graphics.DrawLine(pen, 1 * mpw / 4, 0, 1 * mpw / 4, mph);
            e.Graphics.DrawLine(pen, 2 * mpw / 4, 0, 2 * mpw / 4, mph);
            e.Graphics.DrawLine(pen, 3 * mpw / 4, 0, 3 * mpw / 4, mph);

            pen.Color = System.Drawing.Color.FromArgb(180, 200, 100, 100);
            pen.Width = 10;
            e.Graphics.DrawLine(pen, 0, 2900, mpw, 2900);

            pen.Color = System.Drawing.Color.FromArgb(100, 200, 100, 100); ;
            pen.Width = 1;
            e.Graphics.DrawLine(pen, 1 * mpw / 16, 0, 1 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 2 * mpw / 16, 0, 2 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 3 * mpw / 16, 0, 3 * mpw / 16, mph);

            e.Graphics.DrawLine(pen, 5 * mpw / 16, 0, 5 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 6 * mpw / 16, 0, 6 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 7 * mpw / 16, 0, 7 * mpw / 16, mph);

            e.Graphics.DrawLine(pen,  9 * mpw / 16, 0,  9 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 10 * mpw / 16, 0, 10 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 11 * mpw / 16, 0, 11 * mpw / 16, mph);

            e.Graphics.DrawLine(pen, 13 * mpw / 16, 0, 13 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 14 * mpw / 16, 0, 14 * mpw / 16, mph);
            e.Graphics.DrawLine(pen, 15 * mpw / 16, 0, 15 * mpw / 16, mph);

            pen.Dispose();
        }
        private void MainPanel_Click(object sender, EventArgs e)
        {
            Point point   = MainPanel.PointToClient(Cursor.Position);
            // MessageBox.Show(point.X.ToString());
            MainPanel_Background.Refresh();
        }
		private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            if(isLoaded)  MainPanel_DrawBeatLine(sender, e);
        }
        private void MainPanel_MouseWheel(object sender, MouseEventArgs me)
        {
            if (!isLoaded || !data_is_ready) return;
            if (me.Delta >0)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition += 0.1;
            }
            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition -= 0.1;
            }
            ProgressBar.Height = Convert.ToInt32(Convert.ToDouble(ProgressBar_Background.Height) * axWindowsMediaPlayer1.Ctlcontrols.currentPosition / axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration);
            Refresh_Layout();
        }
        bool isEditting = false;
        int point_x,point_y;
        private void MainPanel_MouseDown(object sender, MouseEventArgs me)
        {
            if (!isLoaded || !data_is_ready) return;
            isEditting = true;
            point_x = me.X;
            point_y = me.Y;
        }
        private void MainPanel_MouseMove(object sender, MouseEventArgs me)
        {
            if (!isLoaded || !data_is_ready) return;
            if (isEditting) 
            {
                // 1. 先找出 所選擇到的BeatLine , 找出BeatLine在圖上的位置 和 point_y 做比對
                int selectline = 0 ; 
                foreach  (Beatline cur_beatline in Current_BeatLines)
                {
                    if (cur_beatline.y < point_y) break;
                    selectline++;
                }
                Console.WriteLine(Current_BeatLines[selectline].time.ToString());
                // 2. 找出所有橫跨的行  left~right  (0~15)
                int time  = Current_BeatLines[selectline].time; 
                int left  = point_x / (MainPanel.Width / 16);
                int right = me.X    / (MainPanel.Width / 16);

                int index = NoteList.FindIndex(x => x.pos == time);
                if (index >= 0)
                {
                    Console.WriteLine("ClearNoteIndex:" + index.ToString());
                    NoteList.RemoveAt(index);
                }
                NoteList.Add( new Note(time, 0, left, right) );
                Console.WriteLine("ListCount: " + NoteList.Count.ToString());
                MainPanel.Refresh();
            }
        }
        private void MainPanel_MouseUp(object sender, MouseEventArgs me)
        {
            if (!isLoaded || !data_is_ready) return;
            isEditting = false;
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
                data_is_ready = true;
                SetForm.SetData(ref bpm, ref offset, ref beat);
                MainPanel.Refresh();
                MessageBox.Show("Set Done!");
            }
            else
            {
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
    public partial class BackgroundPanel : Panel
    {
        public BackgroundPanel()
        {
            this.SetStyle(ControlStyles.UserPaint |
                            ControlStyles.AllPaintingInWmPaint |
                            ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
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
