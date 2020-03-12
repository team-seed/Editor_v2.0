using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
                this.pos   = Position;
                this.type  = Type;
                this.first = First;
                this.last  = Last;
            }
            public Note(int Position, int Type, int First, int Last, int Extra) : this(Position, Type, First, Last) {
                if (Type == 1) this.nextpos = Extra;
                else if (Type == 2) this.dir = Extra;
            }
            public void Refresh(int Position, int Type, int First, int Last) {
                this.pos   = Position;
                this.type  = Type;
                this.first = First;
                this.last  = Last;
            }
            public void Refresh(int Position, int Type, int First, int Last, int Extra) 
            {
                this.pos = Position;
                this.type = Type;
                this.first = First;
                this.last = Last;
                if (Type == 1)
                {
                    this.dir        = -1;
                    this.nextpos    = Extra;
                }
                else if (Type == 2)
                {
                    this.dir     = Extra;
                    this.nextpos = -1;
                }
            }
        };
        List<Note> NoteList = new List<Note>();

        double bpm, offset;
        int CurrentType = 0;
        int CurrentDir  = 0;
        int CurrentHoldMode = 0;
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
            this.KeyPreview = true;
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
        private int X_Cal(double x1, double x2, int y1, int y2, int y3)
        {
            double x3;
            x3 = (y2 - y3)  * (x1 - x2) / (y2 - y1) + x2;
            return Convert.ToInt32(x3);
        }
        private List<int> Coordinate_Cal(int index,int toptime,int bottomtime)
        {
            List<int> coordinate = new List<int>();
            int nextindex = NoteList.FindIndex(xn => xn.pos == NoteList[index].nextpos);

            double x2 = NoteList[nextindex].first * MainPanel.Width / 16;
            double x1 = NoteList[index].first * MainPanel.Width / 16;
            int y2 = NoteList[nextindex].pos;
            int y1 = NoteList[index].pos;
            int topleft_X    = X_Cal(x1, x2, y1, y2, toptime);
            int bottomleft_X = X_Cal(x1, x2, y1, y2, bottomtime);

            x1 = NoteList[index].last * MainPanel.Width / 16;
            x2 = NoteList[nextindex].last * MainPanel.Width / 16;
            int topright_X    = X_Cal(x1, x2, y1, y2, toptime);
            int bottomright_X = X_Cal(x1, x2, y1, y2,bottomtime);

            coordinate.Add(topleft_X);
            coordinate.Add(topright_X);
            coordinate.Add(bottomleft_X);
            coordinate.Add(bottomright_X);
            return coordinate;
        }
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
            // 1. 先從NoteList 中抓出Hold範圍 找出在畫面上顯示的Hold
            // Console.WriteLine("toptime : " + (cur_pos + BottomPanel.Location.Y - 100).ToString());
            // Console.WriteLine("bottomtime : " + (cur_pos - 100).ToString());
            pen.Color = System.Drawing.Color.DeepSkyBlue;
            for (int i = 0; i < NoteList.Count(); i++)
            {
                if (NoteList[i].nextpos == -1) continue;
                int toptime = cur_pos + BottomPanel.Location.Y - 100;
                int bottomtime = cur_pos - 100;
                if (NoteList[i].nextpos >= toptime && NoteList[i].pos <= bottomtime)    // 全版
                {
                    int top_y = 2900 - (toptime - cur_pos);
                    int bottom_y = 3000;
                    List <int> temp = Coordinate_Cal(i,toptime,bottomtime);
                    Point[] HoldPoints = { new Point(temp[0],top_y), new Point(temp[1],top_y)
                                        , new Point(temp[3],bottom_y),new Point(temp[2],bottom_y)};
                    e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                }
                else if (NoteList[i].nextpos >= toptime && NoteList[i].pos < toptime)   // 上版
                {
                    int top_y = 2900 - (toptime - cur_pos);
                    int bottom_y = 2900 - (NoteList[i].pos - cur_pos);
                    int bottom_x1 = NoteList[i].first * MainPanel.Width / 16;
                    int bottom_x2 = NoteList[i].last  * MainPanel.Width / 16;
                    List<int> temp = Coordinate_Cal(i, toptime, bottomtime);
                    Point[] HoldPoints = { new Point(temp[0],top_y), new Point(temp[1],top_y)
                                        , new Point(bottom_x1,bottom_y),new Point(bottom_x2,bottom_y)};
                    e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                }
                else if (NoteList[i].nextpos > bottomtime && NoteList[i].pos <= bottomtime) // 下版
                {
                    int top_y = 2900 - (NoteList[i].nextpos - cur_pos);
                    int bottom_y = 2900 - (bottomtime - cur_pos);
                    int nextindex = NoteList.FindIndex(xn => xn.pos == NoteList[i].nextpos);
                    int top_x1 = NoteList[nextindex].first * MainPanel.Width / 16;
                    int top_x2 = NoteList[nextindex].last  * MainPanel.Width / 16;
                    List<int> temp = Coordinate_Cal(i, toptime, bottomtime);
                    Point[] HoldPoints = { new Point(top_x1,top_y), new Point(top_x2,top_y)
                                        , new Point(temp[3],bottom_y),new Point(temp[2],bottom_y)};
                    e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                }
                 

            }

            for (int i = 2900; i > 0; i -= BeatLength) {
                int Cur_Line = Convert.ToInt32(cur_pos / BeatLength) * BeatLength + 2900 - i;
                string drawString = Cur_Line.ToString();
                // Console.WriteLine("CurrentTime: " + drawString + " Position: " + (cur_pos % BeatLength + i).ToString() + " Position2: " +(2900 - (Cur_Line-cur_pos)).ToString());
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
                if (index >= 0)
                {
                    int l = NoteList[index].first;
                    int r = NoteList[index].last;


                    ///
                    // 畫出選取範圍
                    ///
                    int leftBound  = l * MainPanel.Width / 16;
                    int rightBound = r * MainPanel.Width / 16;
                    int y_pos = Convert.ToInt32(cur_pos % BeatLength + y);
                    pen.Color = System.Drawing.Color.Coral;
                    e.Graphics.DrawLine(pen, leftBound, y_pos,rightBound , y_pos);
                    ///
                    //  畫出Note
                    ///
                    

                    switch (NoteList[index].type) 
                    {
                        case 0:
                            GraphicsPath path = new GraphicsPath();
                            path.AddEllipse(leftBound - 5, y_pos - 10, rightBound - leftBound + 10, 20);
                            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
                            pthGrBrush.CenterColor = System.Drawing.Color.FromArgb(255, 0, 0, 255);
                            System.Drawing.Color[] colors = { System.Drawing.Color.FromArgb(255, 0, 255, 255) };
                            pthGrBrush.SurroundColors = colors;
                            e.Graphics.FillRectangle(pthGrBrush,leftBound - 5, y_pos - 7, rightBound - leftBound + 10, 14);
                            break;
                        case 1:
                            if ( NoteList[index].nextpos != -1) 
                            {
                                int nextindex  = NoteList.FindIndex(x1 => x1.pos == NoteList[index].nextpos);
                                if(NoteList[nextindex].type != 1)
                                {
                                    NoteList[index].nextpos = -1;
                                    break;
                                }
                                int next_left  = NoteList[nextindex].first * MainPanel.Width / 16; ;
                                int next_right = NoteList[nextindex].last  * MainPanel.Width / 16; ;
                                int next_y     = cur_pos -  NoteList[nextindex].pos + 2900;
                                //Console.WriteLine("next_y: " + next_y.ToString());
                                pen.Color = System.Drawing.Color.DeepSkyBlue;
                                Point[] HoldPoints = { new Point(next_left,next_y), new Point(next_right,next_y),
                                                        new Point(rightBound,y_pos), new Point(leftBound,y_pos)};
                                e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                            }
                            break;
                        case 2:
                            System.Drawing.Pen pen2 = new System.Drawing.Pen(System.Drawing.Color.FromArgb(225, 100, 240, 200), 5);
                            e.Graphics.FillRectangle(pen2.Brush, leftBound, y_pos - 10, rightBound - leftBound, 20);
                            for (int count = leftBound; count< rightBound -10; count += 20)
                            {
                                switch (NoteList[index].dir) 
                                {
                                    case 0:
                                        e.Graphics.DrawString("▲", drawFont, drawBrush, count, y_pos -8, drawFormat);
                                        break;
                                    case 1:
                                        e.Graphics.DrawString("▼", drawFont, drawBrush, count, y_pos -8, drawFormat);
                                        break;
                                    case 2:
                                        e.Graphics.DrawString("❮", drawFont, drawBrush, count, y_pos -8, drawFormat);
                                        break;
                                    case 3:
                                        e.Graphics.DrawString("❯", drawFont, drawBrush, count, y_pos -8, drawFormat);
                                        break;
                                } 
                            }
                            pen2.Dispose();
                            //e.Graphics.DrawString("2", drawFont, drawBrush, (l + r) * MainPanel.Width / 32, cur_pos % BeatLength + y - 5, drawFormat);
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
            if (me.Button == MouseButtons.Right)
            {
                // Delete Note Here
                int selectline = 0;
                int time = -1;
                if (Current_BeatLines.Count() > 0)
                {
                    foreach (Beatline cur_beatline in Current_BeatLines)
                    {
                        if (cur_beatline.y < me.Y) break;
                        selectline++;
                    }
                    time = Current_BeatLines[selectline].time;
                }
                if (time != -1) 
                { 
                    for (int i = 0; i < NoteList.Count(); i++)
                    {
                        if (NoteList[i].nextpos == time) NoteList[i].nextpos = -1;
                        if (NoteList[i].pos == time) NoteList.RemoveAt(i);
                    }
                }
            }
            else if (me.Button == MouseButtons.Left)
            {
                isEditting = true;
                point_x = me.X;
                point_y = me.Y;
            }
        }
        private void MainPanel_MouseMove(object sender, MouseEventArgs me)
        {
            if (!isLoaded || !data_is_ready) return;
            if (isEditting)
            {
                // 1. 先找出 所選擇到的BeatLine , 找出BeatLine在圖上的位置 和 point_y 做比對
                
                int selectline = 0;
                foreach (Beatline cur_beatline in Current_BeatLines)
                {
                    if (cur_beatline.y < point_y) break;
                    selectline++;
                }
                // 2. 找出所有橫跨的行  left~right  (0~15)
                int time  = Current_BeatLines[selectline].time;
                int left  = point_x  / (MainPanel.Width / 16);
                int right = me.X     / (MainPanel.Width / 16);

                if (left > right)
                {
                    int temp = left;
                    left     = right;
                    right    = temp;
                }
                int index = NoteList.FindIndex(x => x.pos == time);
                bool old = false;
                if (index >= 0)     // 將舊的NoteList更新
                {
                    if (NoteList[index].type != CurrentType) 
                    {
                        for (int i = 0; i < NoteList.Count(); i++)
                        {
                            if (NoteList[i].nextpos == time) NoteList[i].nextpos = -1;
                            if (NoteList[i].pos == time) NoteList.RemoveAt(i);
                        }
                    }
                    else old = true;
                }
                if (left != right)
                {
                    switch (CurrentType)
                    {
                        case 0:
                            if (old == true)
                            {
                                for (int i = 0; i < NoteList.Count(); i++)
                                {
                                    if (NoteList[i].nextpos == time) NoteList[i].nextpos = -1;
                                }
                                NoteList[index].Refresh(time, 0, left, right);
                            }
                            else NoteList.Add(new Note(time, 0, left, right));
                            break;
                        case 1:
                            // mode switch  -  type0: 建立Hold起始位置，不與前面Hold建立連結
                            //              -  type1: 延伸Hold,抓前面一個Hold建立連結
                            
                            int NearestHold = -1;
                            for (int i = 0; i < NoteList.Count(); i++)
                            {
                                Console.WriteLine("time: " + NoteList[i].pos + " type: " + NoteList[i].type + " next: " + NoteList[i].nextpos);
                                if (NoteList[i].pos >= time || NoteList[i].type != 1 ) continue;
                                if (NearestHold == -1 || NoteList[i].pos > NoteList[NearestHold].pos) NearestHold = i;
                            }
                            if (NearestHold != -1)
                            {
                               // Console.WriteLine("Current Line: "+time.ToString()+" Next Line:"+ NoteList[NearestHold].pos.ToString()+ "CurrentHoldMode: " + CurrentHoldMode.ToString());
                                if (CurrentHoldMode == 0) NoteList[NearestHold].nextpos = time;
                                else NoteList[NearestHold].nextpos = -1;

                            }
                            
                            if (old == true) NoteList[index].Refresh(time, 1, left, right);
                            else NoteList.Add(new Note(time, 1, left, right, -1) );
                            break;
                        case 2:
                            if (old == true)
                            {
                                for (int i = 0; i < NoteList.Count(); i++)
                                {
                                    if (NoteList[i].nextpos == time) NoteList[i].nextpos = -1;
                                }
                                NoteList[index].Refresh(time, 2, left, right, CurrentDir);
                            }
                            else NoteList.Add(new Note(time, 2, left, right, CurrentDir));
                            break;
                    }
                }
                MainPanel.Refresh();
            }
        }
        private void MainPanel_MouseUp(object sender, MouseEventArgs me)
        {
            if (!isLoaded || !data_is_ready) return;
            isEditting = false;
        }

        ///
        //  TypeSwitchEvent
        ///
        private void TypeSwitch(int type)
        {
            switch (type)
            {
                case 49:
                    CurrentType = 0;
                    Swipe.BringToFront();
                    Hold.BringToFront();
                    Hold.BackColor = System.Drawing.Color.Gray;
                    Click.BackColor = System.Drawing.Color.Aquamarine;
                    Swipe.BackColor = System.Drawing.Color.Gray;
                    break;
                case 50:
                    CurrentType = 1;
                    Swipe.BringToFront();
                    Hold.BackColor = System.Drawing.Color.Aquamarine;
                    Click.BackColor = System.Drawing.Color.Gray;
                    Swipe.BackColor = System.Drawing.Color.Gray;
                    if (CurrentHoldMode == 0) Link.BringToFront();
                    else Unlink.BringToFront();
                    break;
                case 51:
                    CurrentType = 2;
                    Hold.BringToFront();
                    Hold.BackColor = System.Drawing.Color.Gray;
                    Click.BackColor = System.Drawing.Color.Gray;
                    Swipe.BackColor = System.Drawing.Color.Aquamarine;
                    switch (CurrentDir) 
                    {
                        case 0:
                            Up.BringToFront(); break;
                        case 1:
                            Down.BringToFront(); break;
                        case 2:
                            Left.BringToFront(); break;
                        case 3:
                            Right.BringToFront(); break;
                    }
                    break;
            }
        }
        private void Hold_Click(object sender, EventArgs e)
        {
            TypeSwitch(50);
        }

        private void Click_Click(object sender, EventArgs e)
        {
            TypeSwitch(49);
        }

        private void Swipe_Click(object sender, EventArgs e)
        {
            TypeSwitch(51);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TypeSwitch(e.KeyChar);
            if (CurrentType == 2)
            {
                switch ((int)e.KeyChar)
                {
                    case 87:
                    case 119:
                        CurrentDir = 0;
                        Up.BringToFront();
                        break;
                    case 83:
                    case 115:
                        CurrentDir = 1;
                        Down.BringToFront();
                        break;
                    case 65:
                    case 97:
                        CurrentDir = 2;
                        Left.BringToFront();
                        break;
                    case 68:
                    case 100:
                        CurrentDir = 3;
                        Right.BringToFront();
                        break;
                }
            }
            else if (CurrentType == 1)
            {
                if ((int)e.KeyChar == 81 || (int)e.KeyChar == 113)
                {
                    if (CurrentHoldMode == 1)
                    {
                        CurrentHoldMode = 0;
                        Link.BringToFront();
                    }
                    else
                    {
                        CurrentHoldMode = 1;
                        Unlink.BringToFront();
                    }
                }
            }
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
