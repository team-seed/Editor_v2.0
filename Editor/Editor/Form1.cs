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
using System.IO;

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
        class SET
        {
            public double BPM, OFFSET;
            public int BEAT;
            public string NAME;
            public List<Note> noteset;
            
            public SET(string name, double bpm, double offset, int beat)
            {
                this.BPM = bpm;
                this.OFFSET = offset;
                this.BEAT = beat;
                this.NAME = name;
                this.noteset = new List<Note>();
            }
            public SET(string name, double bpm, double offset, int beat, List<Note> n) : this(name, bpm, offset, beat)
            {
                this.noteset = n;
            }
        };

        List<SET> SetList = new List<SET>();
        double current_position = 0;
        double dilation = 1;
        string save_location = "";
        int CurrentSection = 0;
        int CurrentFraction = 1;
        int CurrentType = 0;
        int CurrentDir  = 0;
        int CurrentHoldMode = 0;
        bool data_is_ready = false;
        bool isDragging = false;
        bool isLoaded = false;
        SubForm_setting SetForm;
        Graphics BackGraphics;
        Bitmap backBmp;


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
                Import.BackColor = System.Drawing.Color.FromArgb(255, 227, 227, 227);
                Import.Enabled = true;
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
                current_position = 0;
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
                current_position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;
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
                current_position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;
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
            current_position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;
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
            current_position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;
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
                current_position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;
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
        private int X_Cal(double x1, double x2, double y1, double y2, double y3)
        {
            double x3;
            x3 = (y2 - y3) * (x1 - x2) / (y2 - y1) + x2;
            return Convert.ToInt32(x3);
        }
        private List<int> Coordinate_Cal(int section,int noteindex,double toptime,double bottomtime)
        {
            List<int> coordinate = new List<int>();
            int nextindex = SetList[section].noteset.FindIndex(xn => xn.pos == SetList[section].noteset[noteindex].nextpos);
            if (nextindex >= 0)
            {
                double x2 = SetList[section].noteset[nextindex].first * MainPanel.Width / 16;
                double x1 = SetList[section].noteset[noteindex].first * MainPanel.Width     / 16;
                double y2 = SetList[section].noteset[nextindex].pos;
                double y1 = SetList[section].noteset[noteindex].pos;
                int topleft_X    = X_Cal(x1, x2, y1, y2, toptime);
                int bottomleft_X = X_Cal(x1, x2, y1, y2, bottomtime);

                x1 = SetList[section].noteset[noteindex].last * MainPanel.Width / 16;
                x2 = SetList[section].noteset[nextindex].last * MainPanel.Width / 16;
                int topright_X = X_Cal(x1, x2, y1, y2, toptime);
                int bottomright_X = X_Cal(x1, x2, y1, y2, bottomtime);

                coordinate.Add(topleft_X);
                coordinate.Add(topright_X);
                coordinate.Add(bottomleft_X);
                coordinate.Add(bottomright_X);
            }
            else 
            {
                Console.WriteLine("Error Occur at: " + SetList[section].noteset[noteindex].pos + " next: " + SetList[section].noteset[noteindex].nextpos);
                Console.WriteLine("pos:" + SetList[section].noteset[noteindex].pos + "l:" + SetList[section].noteset[noteindex].first + " r:" + SetList[section].noteset[noteindex].last + " type:"
                    + SetList[section].noteset[noteindex].type);
            }
            return coordinate;
        }
        private void MainPanel_DrawBeatLine(object sender, PaintEventArgs e)
        {
            if (!data_is_ready) return;
            if (SetList[CurrentSection].BPM == 0) return;
            
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
           
            
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(255, 0, 0, 0), 5);
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            

            Current_BeatLines.Clear();
            double BeatLength = 60 / SetList[CurrentSection].BPM * 1000;
            double cur_pos;
            if (current_position < 0) cur_pos = current_position;
            else cur_pos = axWindowsMediaPlayer1.Ctlcontrols.currentPosition * 1000;


        ///
        //   (out of panel) Hold 
        ///

            for (int index = 0; index < SetList.Count(); index++)
            {
                if (index == CurrentSection)    pen.Color = System.Drawing.Color.FromArgb(200, 0, 191, 255);
                else    pen.Color = System.Drawing.Color.FromArgb(150, 0, 191, 255);
                for (int i = 0; i < SetList[index].noteset.Count(); i++)
                {
                    if (SetList[index].noteset[i].nextpos == -1) continue;
                    double toptime = cur_pos + (BottomPanel.Location.Y - 100) / dilation;
                    double bottomtime = cur_pos - (100 / dilation);
                    if (SetList[index].noteset[i].nextpos >= toptime && SetList[index].noteset[i].pos <= bottomtime)    // 全版
                    {
                        int top_y = Convert.ToInt32(3000 - BottomPanel.Location.Y);
                        int bottom_y = 3000;
                        List<int> temp = Coordinate_Cal(index,i, toptime, bottomtime);
                        if (temp.Count() == 0) { continue; }
                        Point[] HoldPoints = { new Point(temp[0],top_y), new Point(temp[1],top_y)
                                            , new Point(temp[3],bottom_y),new Point(temp[2],bottom_y)};
                        e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                    }
                    else if (SetList[index].noteset[i].nextpos >= toptime && SetList[index].noteset[i].pos < toptime)   // 上版
                    {
                        int top_y = Convert.ToInt32(3000 - BottomPanel.Location.Y);
                        int bottom_y = Convert.ToInt32(2900 - (SetList[index].noteset[i].pos - cur_pos) * dilation);
                        int bottom_x1 = SetList[index].noteset[i].first * MainPanel.Width / 16;
                        int bottom_x2 = SetList[index].noteset[i].last * MainPanel.Width / 16;
                        List<int> temp = Coordinate_Cal(index,i, toptime, bottomtime);
                        if (temp.Count() == 0) { continue; }
                        Point[] HoldPoints = { new Point(temp[0],top_y), new Point(temp[1],top_y)
                                            , new Point(bottom_x2,bottom_y),new Point(bottom_x1,bottom_y)};
                        e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                    }
                    else if (SetList[index].noteset[i].nextpos > bottomtime && SetList[index].noteset[i].pos <= bottomtime) // 下版
                    {
                        int top_y = Convert.ToInt32(2900 - (SetList[index].noteset[i].nextpos - cur_pos) * dilation);
                        int bottom_y = 3000;
                        int nextindex = SetList[index].noteset.FindIndex(xn => xn.pos == SetList[index].noteset[i].nextpos);
                        if (nextindex < 0) continue;
                        int top_x1 = SetList[index].noteset[nextindex].first * MainPanel.Width / 16;
                        int top_x2 = SetList[index].noteset[nextindex].last * MainPanel.Width / 16;
                        List<int> temp = Coordinate_Cal(index,i, toptime, bottomtime);
                        if (temp.Count() == 0) { continue; }
                        Point[] HoldPoints = { new Point(top_x1,top_y), new Point(top_x2,top_y)
                                            , new Point(temp[3],bottom_y),new Point(temp[2],bottom_y)};
                        e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                    }


                }
            }

        ///
        //  1/n BeatLine
        ///

            int Beatcount = 0;
            int fraction = 0;
            double bottom = cur_pos - (100 / dilation);
            double ofs = SetList[CurrentSection].OFFSET;
            double baseline = bottom -((bottom - ofs) % BeatLength);
            if ((bottom - ofs) < 0) baseline -= BeatLength;
            for (double i = 3000; i > 0; i -= BeatLength/CurrentFraction)
            {
                if (CurrentFraction == 1) break;

                fraction = Beatcount % CurrentFraction;
                int Cur_Line = Convert.ToInt32(baseline + (Beatcount++ * BeatLength / CurrentFraction));
                string drawString = Cur_Line.ToString();


                switch (CurrentFraction) 
                {
                    case 2:
                        if (fraction == 1) pen.Color = System.Drawing.Color.FromArgb(255, 209, 95, 71);
                        break;
                    case 3:
                        if (fraction % 3 != 0) pen.Color = System.Drawing.Color.FromArgb(255, 0, 158, 13);
                        break;
                    case 4:
                        if (fraction == 1 || fraction == 3) pen.Color = System.Drawing.Color.FromArgb(255, 122, 244, 248);
                        else if (fraction == 2) pen.Color = System.Drawing.Color.FromArgb(255, 209, 95, 71);
                        break;
                    case 6:
                        if (fraction == 1 || fraction == 5) pen.Color = System.Drawing.Color.FromArgb(255, 0, 158, 13);//pen.Color = System.Drawing.Color.FromArgb(255, 133, 122, 248);
                        else if (fraction == 2 || fraction == 4) pen.Color = System.Drawing.Color.FromArgb(255, 0, 158, 13);
                        else pen.Color = System.Drawing.Color.FromArgb(255, 209, 95, 71);
                        break;
                    case 8:
                        if (fraction == 4) pen.Color = System.Drawing.Color.FromArgb(255, 209, 95, 71);
                        else if (fraction == 2 || fraction == 6) pen.Color = System.Drawing.Color.FromArgb(255, 122, 244, 248);
                        else pen.Color = System.Drawing.Color.FromArgb(255, 233, 243, 99);
                        break;
                    case 12:
                        if (fraction == 6) pen.Color = System.Drawing.Color.FromArgb(255, 209, 95, 71);
                        else if (fraction == 3 || fraction == 9) pen.Color = System.Drawing.Color.FromArgb(255, 122, 244, 248);
                        else if (fraction == 2 || fraction == 10) pen.Color = System.Drawing.Color.FromArgb(255, 0, 158, 13);//pen.Color = System.Drawing.Color.FromArgb(255, 133, 122, 248);
                        else if (fraction == 4 || fraction == 8) pen.Color = System.Drawing.Color.FromArgb(255, 0, 158, 13);
                        else pen.Color = System.Drawing.Color.FromArgb(255, 130, 255, 190);
                        break;
                }
                

                pen.Width = 3;

                int Beatline_Y = Convert.ToInt32(2900 - (Cur_Line - cur_pos) * dilation);

                Current_BeatLines.Add(new Beatline(Cur_Line, Beatline_Y));
                e.Graphics.DrawLine(pen, 0, Beatline_Y, MainPanel.Width, Beatline_Y);
                e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, Beatline_Y + 2, drawFormat);
                e.Graphics.DrawString(fraction.ToString(), drawFont, drawBrush, 50, Beatline_Y + 2, drawFormat);

            }
        ///
        //  Beatline
        ///  
            Beatcount = 0;
             for (double i = 3000; i > 0; i -= BeatLength) {
                int Cur_Line = Convert.ToInt32( baseline+ (Beatcount++ * BeatLength ));
                string drawString = Cur_Line.ToString();
                if (SetList[CurrentSection].BEAT != 0 && Convert.ToInt32((Cur_Line-ofs) / BeatLength) % SetList[CurrentSection].BEAT == 0)
                {
                    pen.Color = System.Drawing.Color.FromArgb(230, 240, 20, 20);
                    pen.Width = 5;
                }
                else 
                {
                    pen.Color = System.Drawing.Color.FromArgb(255, 0, 0, 0);
                    pen.Width = 5;
                }
                int BeatLine_Y = Convert.ToInt32( 2900 - (Cur_Line - cur_pos)*dilation );
                Current_BeatLines.Add( new Beatline( Cur_Line, BeatLine_Y ) );
                e.Graphics.DrawLine(pen, 0, BeatLine_Y, MainPanel.Width, BeatLine_Y);
                e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, BeatLine_Y+2, drawFormat);
            }
        ///
        //  Note
        ///
            for(int index=0;index<SetList.Count();index++)
            {
                for (int i = 0; i < SetList[index].noteset.Count(); i++)
                {
                    double toptime = cur_pos + (BottomPanel.Location.Y - 100) * dilation;
                    double bottomtime = cur_pos - 100 * dilation;
                    if (SetList[index].noteset[i].pos >= bottomtime && SetList[index].noteset[i].pos <= toptime)
                    {

                        int l = SetList[index].noteset[i].first;
                        int r = SetList[index].noteset[i].last;


                        ///
                        // 畫出選取範圍
                        ///
                        int leftBound = l * MainPanel.Width / 16;
                        int rightBound = r * MainPanel.Width / 16;
                        int y_pos = Convert.ToInt32(2900 - (SetList[index].noteset[i].pos - cur_pos)*dilation);
                        
                        if (index == CurrentSection) pen.Color = System.Drawing.Color.FromArgb(100, 255, 200, 200);
                        else    pen.Color = System.Drawing.Color.FromArgb(40, 255, 200, 200);
                        e.Graphics.DrawLine(pen, leftBound, y_pos, rightBound, y_pos);

                        ///
                        //  畫出Note
                        ///
                        switch (SetList[index].noteset[i].type)
                        {
                            case 0:
                                GraphicsPath path = new GraphicsPath();
                                path.AddEllipse(leftBound - 5, y_pos - 10, rightBound - leftBound + 10, 20);
                                PathGradientBrush pthGrBrush = new PathGradientBrush(path);
                                System.Drawing.Color[] colors = { System.Drawing.Color.FromArgb(225, 218, 18, 18) };

                                if (index == CurrentSection)
                                {
                                    pthGrBrush.CenterColor = System.Drawing.Color.FromArgb(225, 252, 82, 82);
                                    colors[0] = System.Drawing.Color.FromArgb(225, 218, 18, 18);
                                }
                                else 
                                {
                                    pthGrBrush.CenterColor = System.Drawing.Color.FromArgb(170, 252, 82, 82);
                                    colors[0] = System.Drawing.Color.FromArgb(170, 218, 18, 18);
                                }
                                pthGrBrush.SurroundColors = colors;
                                e.Graphics.FillRectangle(pthGrBrush, leftBound - 5, y_pos - 7, rightBound - leftBound + 10, 14);
                                break;
                            case 1:
                                if (SetList[index].noteset[i].nextpos != -1)
                                {
                                    if (SetList[index].noteset[i].pos < bottomtime || SetList[index].noteset[i].nextpos > toptime) break;

                                    int nextindex = SetList[index].noteset.FindIndex(x1 => x1.pos == SetList[index].noteset[i].nextpos);
                                    if (nextindex >= 0)
                                    {
                                        if (SetList[index].noteset[nextindex].type != 1)
                                        {
                                            SetList[index].noteset[i].nextpos = -1;
                                            break;
                                        }
                                        int next_left = SetList[index].noteset[nextindex].first * MainPanel.Width / 16; ;
                                        int next_right = SetList[index].noteset[nextindex].last * MainPanel.Width / 16; ;
                                        int next_y = Convert.ToInt32(2900 + (cur_pos - SetList[index].noteset[nextindex].pos )*dilation );

                                        if (index == CurrentSection) pen.Color = System.Drawing.Color.FromArgb(200, 0, 191, 255);
                                        else    pen.Color = System.Drawing.Color.FromArgb(150, 0, 191, 255);

                                        Point[] HoldPoints = { new Point(next_left,next_y), new Point(next_right,next_y),
                                                            new Point(rightBound,y_pos), new Point(leftBound,y_pos)};
                                        e.Graphics.FillPolygon(pen.Brush, HoldPoints);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error Occur at:　" + SetList[index].noteset[i].pos + " next: " + SetList[index].noteset[i].nextpos);
                                        Console.WriteLine("pos:" + SetList[index].noteset[i].pos + "l:" + SetList[index].noteset[i].first + " r:" + SetList[index].noteset[i].last + " type:"
                                         + SetList[index].noteset[i].type);
                                    }
                                }
                                break;
                            case 2:
                                System.Drawing.Pen pen2;
                                if (index == CurrentSection) pen2 = new System.Drawing.Pen(System.Drawing.Color.FromArgb(225, 100, 240, 200), 5);
                                else  pen2 = new System.Drawing.Pen(System.Drawing.Color.FromArgb(155, 100, 240, 200), 5);
                                
                                e.Graphics.FillRectangle(pen2.Brush, leftBound, y_pos - 10, rightBound - leftBound, 20);
                                for (int count = leftBound; count < rightBound - 10; count += 20)
                                {
                                    switch (SetList[index].noteset[i].dir)
                                    {
                                        case 0:
                                            e.Graphics.DrawString("▲", drawFont, drawBrush, count, y_pos - 8, drawFormat);
                                            break;
                                        case 1:
                                            e.Graphics.DrawString("▼", drawFont, drawBrush, count, y_pos - 8, drawFormat);
                                            break;
                                        case 2:
                                            e.Graphics.DrawString("❮", drawFont, drawBrush, count, y_pos - 8, drawFormat);
                                            break;
                                        case 3:
                                            e.Graphics.DrawString("❯", drawFont, drawBrush, count, y_pos - 8, drawFormat);
                                            break;
                                    }
                                }
                                pen2.Dispose();
                                //e.Graphics.DrawString("2", drawFont, drawBrush, (l + r) * MainPanel.Width / 32, cur_pos % BeatLength + y - 5, drawFormat);
                                break;
                        }
                    }
                }
            }
        
            pen.Dispose();
            drawFormat.Dispose();
            drawFont.Dispose();
            drawBrush.Dispose();
        }
      
        private void MainPanel_Background_Paint(object sender, PaintEventArgs e)
        {

            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.DarkGray,5);
            int mpw = MainPanel_Background.Width;
            int mph = MainPanel_Background.Height;
            e.Graphics.DrawLine(pen, 1 * mpw / 4, 0, 1 * mpw / 4, mph);
            e.Graphics.DrawLine(pen, 2 * mpw / 4, 0, 2 * mpw / 4, mph);
            e.Graphics.DrawLine(pen, 3 * mpw / 4, 0, 3 * mpw / 4, mph);

            pen.Color = System.Drawing.Color.FromArgb(140, 30, 30,230);
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
                if (Control.ModifierKeys == Keys.Control)
                {
                    dilation += 0.05;
                }
                else
                {
                    if (current_position < 0)
                    {
                        current_position += 100 / dilation;
                        if (current_position > 0) current_position = 0;
                    }
                    else
                    {
                        axWindowsMediaPlayer1.Ctlcontrols.currentPosition += 0.1 / dilation;
                        current_position = axWindowsMediaPlayer1.Ctlcontrols.currentPosition *1000;
                    }
                }
            }
            else
            {
                if (Control.ModifierKeys == Keys.Control)
                {
                    if (dilation > 1) dilation -= 0.05;
                }
                else
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentPosition -= 0.1 / dilation;
                    current_position -= 100 / dilation;
                }
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
                    for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                    {
                        if (SetList[CurrentSection].noteset[i].pos == time) SetList[CurrentSection].noteset.RemoveAt(i);
                    }
                    for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                    {
                        if (SetList[CurrentSection].noteset[i].nextpos == time) SetList[CurrentSection].noteset[i].nextpos = -1;
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
                // 2. 找出所有橫跨的行  left~right  (0~16)
                int time  = Current_BeatLines[selectline].time;
                int left  = point_x  / (MainPanel.Width / 16);
                int right = me.X     / (MainPanel.Width / 16);
                if (right < 0) right = 0;
                if (right > 16) right = 16;
                    if (left > right)
                {
                    int temp = left;
                    left     = right;
                    right    = temp;
                }
                int index = SetList[CurrentSection].noteset.FindIndex(x => x.pos == time);
                bool old = false;
                if (index >= 0)     // 將舊的SetList[CurrentSection].noteset更新
                {
                    if (SetList[CurrentSection].noteset[index].type != CurrentType) 
                    {
                        for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                        {
                            if (SetList[CurrentSection].noteset[i].pos == time) SetList[CurrentSection].noteset.RemoveAt(i);
                        }
                        for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                        {
                            if (SetList[CurrentSection].noteset[i].nextpos == time) SetList[CurrentSection].noteset[i].nextpos = -1;
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
                                for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                                {
                                    if (SetList[CurrentSection].noteset[i].nextpos == time) SetList[CurrentSection].noteset[i].nextpos = -1;
                                }
                                SetList[CurrentSection].noteset[index].Refresh(time, 0, left, right);
                            }
                            else SetList[CurrentSection].noteset.Add(new Note(time, 0, left, right));
                            break;
                        case 1:
                            // mode switch  -  type0: 建立Hold起始位置，不與前面Hold建立連結
                            //              -  type1: 延伸Hold,抓前面一個Hold建立連結
                            
                            int NearestHold = -1;
                            for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                            {
                                if (SetList[CurrentSection].noteset[i].pos >= time || SetList[CurrentSection].noteset[i].type != 1 ) continue;
                                if (NearestHold == -1 || SetList[CurrentSection].noteset[i].pos > SetList[CurrentSection].noteset[NearestHold].pos) NearestHold = i;
                            }
                            if (NearestHold != -1 && CurrentHoldMode == 0)
                            {
                                SetList[CurrentSection].noteset[NearestHold].nextpos = time;
                            }
                            
                            if (old == true) SetList[CurrentSection].noteset[index].Refresh(time, 1, left, right);
                            else SetList[CurrentSection].noteset.Add(new Note(time, 1, left, right, -1) );
                            break;
                        case 2:
                            if (old == true)
                            {
                                for (int i = 0; i < SetList[CurrentSection].noteset.Count(); i++)
                                {
                                    if (SetList[CurrentSection].noteset[i].nextpos == time) SetList[CurrentSection].noteset[i].nextpos = -1;
                                }
                                SetList[CurrentSection].noteset[index].Refresh(time, 2, left, right, CurrentDir);
                            }
                            else SetList[CurrentSection].noteset.Add(new Note(time, 2, left, right, CurrentDir));
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
           
            if (e.KeyChar == 101 || e.KeyChar == 69) 
            {
                switch (CurrentFraction) 
                {
                    case 1:
                        CurrentFraction = 2;
                        Fraction_2.BackColor = System.Drawing.Color.Aquamarine;
                        break;
                    case 2:
                        CurrentFraction = 3;
                        Fraction_2.BackColor = System.Drawing.Color.White;
                        Fraction_3.BackColor = System.Drawing.Color.Aquamarine;
                        break;
                    case 3:
                        CurrentFraction = 4;
                        Fraction_3.BackColor = System.Drawing.Color.White;
                        Fraction_4.BackColor = System.Drawing.Color.Aquamarine;
                        break;
                    case 4:
                        CurrentFraction = 6;
                        Fraction_4.BackColor = System.Drawing.Color.White;
                        Fraction_6.BackColor = System.Drawing.Color.Aquamarine;
                        break;
                    case 6:
                        CurrentFraction = 8;
                        Fraction_6.BackColor = System.Drawing.Color.White;
                        Fraction_8.BackColor = System.Drawing.Color.Aquamarine;
                        break;
                    case 8:
                        CurrentFraction = 12;
                        Fraction_8.BackColor = System.Drawing.Color.White;
                        Fraction_12.BackColor = System.Drawing.Color.Aquamarine;
                        break;
                    case 12:
                        CurrentFraction = 1;
                        Fraction_12.BackColor = System.Drawing.Color.White;
                        break;
                }
                MainPanel.Refresh();
            }
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
        private void Fraction_2_Click(object sender, EventArgs e)
        {
            if (CurrentFraction == 2)
            {
                CurrentFraction = 1;
                Fraction_2.BackColor = System.Drawing.Color.White;
            }
            else
            {
                CurrentFraction = 2;
                Fraction_2.BackColor = System.Drawing.Color.Aquamarine;
                Fraction_3.BackColor = System.Drawing.Color.White;
                Fraction_4.BackColor = System.Drawing.Color.White;
                Fraction_6.BackColor = System.Drawing.Color.White;
                Fraction_8.BackColor = System.Drawing.Color.White;
                Fraction_12.BackColor = System.Drawing.Color.White;
            }
            MainPanel.Refresh();
        }

        private void Fraction_3_Click(object sender, EventArgs e)
        {
            if (CurrentFraction == 3)
            {
                CurrentFraction = 1;
                Fraction_3.BackColor = System.Drawing.Color.White;
            }
            else
            {
                CurrentFraction = 3;
                Fraction_2.BackColor = System.Drawing.Color.White;
                Fraction_3.BackColor = System.Drawing.Color.Aquamarine;
                Fraction_4.BackColor = System.Drawing.Color.White;
                Fraction_6.BackColor = System.Drawing.Color.White;
                Fraction_8.BackColor = System.Drawing.Color.White;
                Fraction_12.BackColor = System.Drawing.Color.White;
            }
            MainPanel.Refresh();
        }

        private void Fraction_4_Click(object sender, EventArgs e)
        {
            if (CurrentFraction == 4)
            {
                CurrentFraction = 1;
                Fraction_4.BackColor = System.Drawing.Color.White;
            }
            else
            {
                CurrentFraction = 4;
                Fraction_2.BackColor = System.Drawing.Color.White;
                Fraction_3.BackColor = System.Drawing.Color.White;
                Fraction_4.BackColor = System.Drawing.Color.Aquamarine;
                Fraction_6.BackColor = System.Drawing.Color.White;
                Fraction_8.BackColor = System.Drawing.Color.White;
                Fraction_12.BackColor = System.Drawing.Color.White;
            }
            MainPanel.Refresh();
        }
        private void Fraction_6_Click(object sender, EventArgs e)
        {
            if (CurrentFraction == 6)
            {
                CurrentFraction = 1;
                Fraction_6.BackColor = System.Drawing.Color.White;
            }
            else
            {
                CurrentFraction = 6;
                Fraction_2.BackColor = System.Drawing.Color.White;
                Fraction_3.BackColor = System.Drawing.Color.White;
                Fraction_4.BackColor = System.Drawing.Color.White;
                Fraction_6.BackColor = System.Drawing.Color.Aquamarine;
                Fraction_8.BackColor = System.Drawing.Color.White;
                Fraction_12.BackColor = System.Drawing.Color.White;
            }
            MainPanel.Refresh();
        }
        private void Fraction_8_Click(object sender, EventArgs e)
        {
            if (CurrentFraction == 8)
            {
                CurrentFraction = 1;
                Fraction_8.BackColor = System.Drawing.Color.White;
            }
            else
            {
                CurrentFraction = 8;
                Fraction_2.BackColor = System.Drawing.Color.White;
                Fraction_3.BackColor = System.Drawing.Color.White;
                Fraction_4.BackColor = System.Drawing.Color.White;
                Fraction_6.BackColor = System.Drawing.Color.White;
                Fraction_8.BackColor = System.Drawing.Color.Aquamarine;
                Fraction_12.BackColor = System.Drawing.Color.White;
            }
            MainPanel.Refresh();
        }
        private void Fraction_12_Click(object sender, EventArgs e)
        {
            if (CurrentFraction == 12)
            {
                CurrentFraction = 1;
                Fraction_12.BackColor = System.Drawing.Color.White;
            }
            else
            {
                CurrentFraction = 12;
                Fraction_2.BackColor = System.Drawing.Color.White;
                Fraction_3.BackColor = System.Drawing.Color.White;
                Fraction_4.BackColor = System.Drawing.Color.White;
                Fraction_8.BackColor = System.Drawing.Color.White;
                Fraction_6.BackColor = System.Drawing.Color.White;
                Fraction_12.BackColor = System.Drawing.Color.Aquamarine;
            }
            MainPanel.Refresh();
        }

        ///
        // Yabadado
        ///
        
        private void Settings_Click(object sender, EventArgs e)
        {
            SetForm = new SubForm_setting(SetList.Count());            
            SetForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetForm_Closing);
            SetForm.Show(this);
        }
        private void SetForm_Closing(object sender, EventArgs e)
        {
            if (SetForm.set_ready == true)
            {
                data_is_ready = true;
                AutoSave.Start();
                if (SetForm.newSection)        // New Section
                {
                    string name = "";
                    double bpm = 0, offset = 0;
                    int beat = 0; ;
                    SetForm.SetData(ref name,ref bpm, ref offset, ref beat);
                    SET temp = new SET(name, bpm, offset, beat);

                    Label l = new Label();
                    l.AutoSize = true;
                    l.Font = new System.Drawing.Font("標楷體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                    l.Location = new System.Drawing.Point(10 + SetList.Count() * 100, 23);
                    l.Name  = (SetList.Count()).ToString();         //Section Index
                    l.Size = new System.Drawing.Size(88, 17);
                    l.TabIndex = 3;
                    l.Text = name;
                    l.MouseClick += new System.Windows.Forms.MouseEventHandler(this.l_Click);
                    if (BottomPanel.Controls.Count == 0)
                    {
                        current_position = offset;
                        l.BackColor = System.Drawing.Color.Aquamarine;
                    }
                    SetList.Add(temp);
                    this.BottomPanel.Controls.Add(l);
                }
                else 
                {

                        SetForm.SetData(ref SetList[SetForm.SectionIndex].NAME,
                                            ref SetList[SetForm.SectionIndex].BPM,
                                              ref SetList[SetForm.SectionIndex].OFFSET,
                                               ref SetList[SetForm.SectionIndex].BEAT);
                        SetForm.Label__ref.Text = SetList[SetForm.SectionIndex].NAME;
                }
                MainPanel.Refresh();
               // MessageBox.Show("Set Done!");
            }
            else
            {
                if (SetForm.remove)
                {
                    if (BottomPanel.Controls.Contains(SetForm.Label__ref))
                    {
                        int index = Convert.ToInt32(SetForm.Label__ref.Name);
                        for (int i = index+1; i < SetList.Count; i++)
                        {
                            if (i == CurrentSection) CurrentSection--;
                            Control[] temp = BottomPanel.Controls.Find(i.ToString(),true);
                            temp[0].Name = (i - 1).ToString();
                            temp[0].Location = new Point(temp[0].Location.X-100,temp[0].Location.Y);
                        }
                        SetList.RemoveAt(index);
                        if (CurrentSection!=0 && CurrentSection >= SetList.Count) CurrentSection--;
                        BottomPanel.Controls.Remove(SetForm.Label__ref);
                        Control[] temp2 = BottomPanel.Controls.Find(CurrentSection.ToString(), true);
                        temp2[0].BackColor = System.Drawing.Color.Aquamarine;
                        if (SetList.Count == 0) data_is_ready = false;
                        MainPanel.Refresh();
                    }
                }
                //MessageBox.Show("Set Failed!");
            }
        }
        private void Section_Add()
        {

            for (int i = 0; i < SetList.Count(); i++)
            { 
                Label l = new Label();
                l.AutoSize = true;
                l.Font = new System.Drawing.Font("標楷體", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                l.BackColor = (BottomPanel.Controls.Count==0) ? System.Drawing.Color.Aquamarine : System.Drawing.Color.Transparent;
                l.Location = new System.Drawing.Point(10 + i * 100, 23);
                l.Name = i.ToString();         //Section Index
                l.Size = new System.Drawing.Size(88, 17);
                l.TabIndex = 3;
                l.Text = (SetList[i].NAME == null) ? "Section_"+i.ToString() : SetList[i].NAME;
                l.MouseClick += new System.Windows.Forms.MouseEventHandler(this.l_Click);
                this.BottomPanel.Controls.Add(l);
            }
        }

        private void l_Click(object sender, MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Left)
            {
                Label l1 = (Label)sender;
                //MessageBox.Show("Select " + l1.Name);
                for (int i = 0; i < BottomPanel.Controls.Count; i++)
                {
                    BottomPanel.Controls[i].BackColor = System.Drawing.Color.Transparent;
                }
                l1.BackColor = System.Drawing.Color.Aquamarine;
                CurrentSection = Convert.ToInt32(l1.Name);
                MainPanel.Refresh();
            }
            else if (me.Button == MouseButtons.Right)
            {
                Label l1 = (Label)sender;
                int index = Convert.ToInt32(l1.Name);
                string name = SetList[index].NAME;
                double bpm = SetList[index].BPM;
                double offset = SetList[index].OFFSET;
                int beat = SetList[index].BEAT;

                SetForm = new SubForm_setting(ref l1,index,name,bpm,offset,beat);
                SetForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetForm_Closing);
                SetForm.Show(this);
            }

        }
        
        private void Export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdialog = new SaveFileDialog();

            string s = SetJsonData();
            string out_edt = SetEdtrData();
            sfdialog.Filter = "txt files (*.json)|*.json|All files (*.*)|*.*";
            sfdialog.FilterIndex = 1;
            sfdialog.RestoreDirectory = true;
            if (sfdialog.ShowDialog() == DialogResult.OK)
            {
                save_location = sfdialog.FileName;
                File.WriteAllText(sfdialog.FileName, s);
                File.WriteAllText(sfdialog.FileName + "_edt", out_edt);
            }

        }

        private void Import_Click(object sender, EventArgs e)
        {
            SetList.Clear();
            OpenFileDialog ofdialog = new OpenFileDialog();
            string sinput = "";
            ofdialog.Filter = "txt files (*.json_edt)|*.json_edt|All files (*.*)|*.*";
            ofdialog.FilterIndex = 1;
            ofdialog.RestoreDirectory = true;
            if (ofdialog.ShowDialog() == DialogResult.OK)
            {
                sinput = File.ReadAllText(ofdialog.FileName);


                JObject jb = JObject.Parse(sinput);
                string bpm_range = (string)jb["BPM_RANGE"];
                JArray in_sec = (JArray)jb["SECTION"];

                List<JObject> sect = in_sec.Select(c => (JObject)c).ToList();

                //Parse JArray(NOTES) to NoteList
                foreach (JObject s in sect)
                {
                    string NAME = (string)s["NAME"];
                    double BPM = Convert.ToDouble(s["BPM"]);
                    double OFFSET = Convert.ToDouble(s["OFFSET"]);
                    int BEAT = Convert.ToInt32(s["BEATS"]);
                    List<Note> n = new List<Note>();
                    JArray nt = (JArray)s["NOTES"];
                    ParseJArray(ref n, nt);
                    SetList.Add(new SET(NAME, BPM, OFFSET, BEAT, n));

                }

                data_is_ready = true;
                Section_Add();
                MainPanel.Refresh();

            }
        }

        private string SetJsonData() {

            //不同SECTION有不同NoteList
            List<string> Notedata = new List<string>();     //  Notelist in section
            List<JObject> section = new List<JObject>();    //  Jobject list for json file output.

            foreach (SET s in SetList)
            {

                //NoteList to string List
                string sb = "";
                s.noteset.Sort((x, y) => { return x.pos.CompareTo(y.pos); });

                foreach (Note n in s.noteset)
                {

                    //  may new gesture
                    //  cur = (pos, gesture, first, last, type) 
                    //  cur only for click and swipe.

                    string cur = n.pos.ToString() + ",0," + n.first.ToString() + "," + n.last.ToString() + "," + n.type.ToString();

                    if (n.type == 0)            //click
                        Notedata.Add(cur);

                    if (n.type == 1)            //hold   ***format: pos,first,last,type|pos,first,last.***
                    {
                        if (sb != "")
                            sb += "|" + n.pos.ToString() + ":" + n.first.ToString() + ":" + n.last.ToString();
                        else
                            sb = cur;
                        if (n.nextpos == -1)
                        {
                            Notedata.Add(sb);
                            sb = "";
                        }
                    }
                    if (n.type == 2)           // swipe
                    {
                        Notedata.Add(cur + "|" + n.dir.ToString());
                    }
                }
                //Console.WriteLine(Notedata.Count());
                JObject SEC = new JObject(
                    //new JProperty("NAME", s.NAME),
                    new JProperty("BEATS", s.BEAT),
                    new JProperty("BPM", s.BPM),
                    new JProperty("NOTES",
                        new JArray(
                            from nt in Notedata
                            select new JValue(nt)
                        )
                    ),
                    new JProperty("OFFSET", s.OFFSET)
                    );
                section.Add(SEC);
                Notedata.Clear();
            }


            JObject jObj =
                new JObject(
                    new JProperty("BPM_RANGE", 0),
                    new JProperty("SECTION",
                        new JArray(
                            from st in section
                            select new JObject(st)
                            )));



            return jObj.ToString();
        }

        private string SetEdtrData()
        {
            //不同SECTION有不同NoteList
            List<string> Notedata = new List<string>();     //  Notelist in section
            List<JObject> section = new List<JObject>();    //  Jobject list for json file output.

            foreach (SET s in SetList)
            {

                //NoteList to string List
                string sb = "";
                foreach (Note n in s.noteset)
                {

                    //  may new gesture
                    //  cur = (pos, gesture, first, last, type) 
                    //  cur only for click and swipe.
                    string cur = n.pos.ToString() + ",0," + n.first.ToString() + "," + n.last.ToString() + "," + n.type.ToString();

                    if (n.type == 0)            //click
                        Notedata.Add(cur);

                    if (n.type == 1)            //hold   ***format: pos,first,last,type|pos,first,last.***
                    {
                        if (sb != "")
                            sb += "|" + n.pos.ToString() + ":" + n.first.ToString() + ":" + n.last.ToString();
                        else
                            sb = cur;
                        if (n.nextpos == -1)
                        {
                            Notedata.Add(sb);
                            sb = "";
                        }
                    }
                    if (n.type == 2)           // swipe
                    {
                        Notedata.Add(cur + "|" + n.dir.ToString());
                    }
                }
                //Console.WriteLine(Notedata.Count());
                JObject SEC = new JObject(
                    new JProperty("NAME", s.NAME),
                    new JProperty("BEATS", s.BEAT),
                    new JProperty("BPM", s.BPM),
                    new JProperty("NOTES",
                        new JArray(
                            from nt in Notedata
                            select new JValue(nt)
                        )
                    ),
                    new JProperty("OFFSET", s.OFFSET)
                    );
                section.Add(SEC);
                Notedata.Clear();
            }


            JObject jObj =
                new JObject(
                    new JProperty("BPM_RANGE", 0),
                    new JProperty("SECTION",
                        new JArray(
                            from st in section
                            select new JObject(st)
                            )));



            return jObj.ToString();
        }

        private void AutoSave_Tick(object sender, EventArgs e)
        {
            if (!isLoaded || !data_is_ready) return;
            string out_edt = SetEdtrData();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            AutoSavePath.Text = "AutoSaveAt " + docPath;
           // Console.WriteLine(docPath);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, music.Text+"_AutoSave.json")))
            {
                outputFile.WriteLine(out_edt);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (data_is_ready && isLoaded && (e.Control && e.KeyCode == Keys.S))
            {
                // quick save
                string s = SetJsonData();
                string out_edt = SetEdtrData();
                if (save_location != "")
                {
                    File.WriteAllText(save_location, s);
                    File.WriteAllText(save_location + "_edt", out_edt);
                }
                else
                {
                    SaveFileDialog sfdialog = new SaveFileDialog();
                    sfdialog.Filter = "txt files (*.json)|*.json|All files (*.*)|*.*";
                    sfdialog.FilterIndex = 1;
                    sfdialog.RestoreDirectory = true;
                    if (sfdialog.ShowDialog() == DialogResult.OK)
                    {
                        save_location = sfdialog.FileName;
                        File.WriteAllText(sfdialog.FileName, s);
                        File.WriteAllText(sfdialog.FileName + "_edt", out_edt);
                    }
                }
                AutoSavePath.Text = "QuickSaveAt: " + save_location;
            }
        }

        private void Help_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Height = 430;
            pictureBox1.Width = 750;
            pictureBox1.Visible = true;
        }

        private void Help_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void ParseJArray(ref List<Note> n, JArray nl)
        {
            foreach (string s in nl)
            {
                string[] note = s.Split(',');
                int pos = Convert.ToInt32(note[0]);
                int first = Convert.ToInt32(note[2]);
                int last = Convert.ToInt32(note[3]);
                int type = Convert.ToInt32(note[4][0] - 48);

                if (type == 0)
                {
                    //Console.WriteLine(pos.ToString() + " " + type.ToString() + " " + first.ToString() + " " + last.ToString());
                    n.Add(new Note(pos, type, first, last));
                }
                else if (type == 1)
                {
                    string[] hold = note[4].Substring(1, note[4].Length - 1).Split('|');
                    foreach (string h in hold)
                    {
                        if (h == "") continue;  //avoid error

                        string[] next = h.Split(':');
                        int next_pos = Convert.ToInt32(next[0]);

                        //h的index = N 時Add 第(N-1)筆資訊
                        n.Add(new Note(pos, type, first, last, next_pos));
                        //更新pos first last 資訊給下一個note用
                        pos = next_pos;
                        first = Convert.ToInt32(next[1]);
                        last = Convert.ToInt32(next[2]);
                    }
                    //Console.WriteLine(pos.ToString() + " " + type.ToString() + " " + first.ToString() + " " + last.ToString() + " -1");
                    n.Add(new Note(pos, type, first, last, -1));
                }
                else if (type == 2)
                {
                    int dir = Convert.ToInt32(note[4][2] - 48);
                    n.Add(new Note(pos, type, first, last, dir));
                    //Console.WriteLine(pos.ToString() + " " + type.ToString() + " " + first.ToString() + " " + last.ToString() + " " + dir.ToString());
                }

            }

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
