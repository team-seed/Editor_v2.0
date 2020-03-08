namespace Editor
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SelectMusic = new System.Windows.Forms.Button();
            this.PlayPause = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.MainPanel_Background = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ProgressBar_Bottom = new System.Windows.Forms.Panel();
            this.music_duration = new System.Windows.Forms.Label();
            this.music_position = new System.Windows.Forms.Label();
            this.ProgressBar_Background = new System.Windows.Forms.Panel();
            this.ProgressBar = new System.Windows.Forms.Panel();
            this.music = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.MainPanel_Background.SuspendLayout();
            this.ProgressBar_Bottom.SuspendLayout();
            this.ProgressBar_Background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.SelectMusic);
            this.flowLayoutPanel1.Controls.Add(this.PlayPause);
            this.flowLayoutPanel1.Controls.Add(this.button4);
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Controls.Add(this.button6);
            this.flowLayoutPanel1.Controls.Add(this.button7);
            this.flowLayoutPanel1.Controls.Add(this.button8);
            this.flowLayoutPanel1.Controls.Add(this.button9);
            this.flowLayoutPanel1.Controls.Add(this.button10);
            this.flowLayoutPanel1.Controls.Add(this.button11);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(630, 30);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(122, 685);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // SelectMusic
            // 
            this.SelectMusic.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SelectMusic.Location = new System.Drawing.Point(3, 3);
            this.SelectMusic.Name = "SelectMusic";
            this.SelectMusic.Size = new System.Drawing.Size(111, 44);
            this.SelectMusic.TabIndex = 0;
            this.SelectMusic.Text = "SelectMusic";
            this.SelectMusic.UseVisualStyleBackColor = true;
            this.SelectMusic.Click += new System.EventHandler(this.SelectMusic_Click);
            // 
            // PlayPause
            // 
            this.PlayPause.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PlayPause.Location = new System.Drawing.Point(3, 53);
            this.PlayPause.Name = "PlayPause";
            this.PlayPause.Size = new System.Drawing.Size(111, 44);
            this.PlayPause.TabIndex = 1;
            this.PlayPause.Text = "Play/Pause";
            this.PlayPause.UseVisualStyleBackColor = true;
            this.PlayPause.Click += new System.EventHandler(this.PlayPause_Click);
            // 
            // button4
            // 
            this.button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button4.Location = new System.Drawing.Point(3, 103);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 44);
            this.button4.TabIndex = 2;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button5.Location = new System.Drawing.Point(3, 153);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(111, 44);
            this.button5.TabIndex = 3;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button6.Location = new System.Drawing.Point(3, 203);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(111, 44);
            this.button6.TabIndex = 4;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button7.Location = new System.Drawing.Point(3, 253);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(111, 44);
            this.button7.TabIndex = 5;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button8.Location = new System.Drawing.Point(3, 303);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(111, 44);
            this.button8.TabIndex = 6;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button9.Location = new System.Drawing.Point(3, 353);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(111, 44);
            this.button9.TabIndex = 7;
            this.button9.Text = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button10.Location = new System.Drawing.Point(3, 403);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(111, 44);
            this.button10.TabIndex = 8;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button11.Location = new System.Drawing.Point(3, 453);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(111, 44);
            this.button11.TabIndex = 9;
            this.button11.Text = "button11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // MainPanel_Background
            // 
            this.MainPanel_Background.AllowDrop = true;
            this.MainPanel_Background.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel_Background.BackColor = System.Drawing.Color.White;
            this.MainPanel_Background.Controls.Add(this.MainPanel);
            this.MainPanel_Background.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainPanel_Background.Location = new System.Drawing.Point(73, 43);
            this.MainPanel_Background.Name = "MainPanel_Background";
            this.MainPanel_Background.Size = new System.Drawing.Size(420, 654);
            this.MainPanel_Background.TabIndex = 1;
            this.MainPanel_Background.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Background_Paint);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(417, 468);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.Click += new System.EventHandler(this.MainPanel_Click);
            // 
            // ProgressBar_Bottom
            // 
            this.ProgressBar_Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Bottom.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar_Bottom.Controls.Add(this.music_duration);
            this.ProgressBar_Bottom.Controls.Add(this.music_position);
            this.ProgressBar_Bottom.Location = new System.Drawing.Point(556, 30);
            this.ProgressBar_Bottom.Name = "ProgressBar_Bottom";
            this.ProgressBar_Bottom.Size = new System.Drawing.Size(40, 685);
            this.ProgressBar_Bottom.TabIndex = 2;
            this.ProgressBar_Bottom.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ProgressBar_Bottom_MouseClick);
            this.ProgressBar_Bottom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ProgressBar_Bottom_MouseDown);
            this.ProgressBar_Bottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ProgressBar_Bottom_MouseMove);
            this.ProgressBar_Bottom.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ProgressBar_Bottom_MouseUp);
            this.ProgressBar_Bottom.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ProgressBar_Bottom_MouseWheel);
            // 
            // music_duration
            // 
            this.music_duration.AutoSize = true;
            this.music_duration.Location = new System.Drawing.Point(0, -1);
            this.music_duration.Name = "music_duration";
            this.music_duration.Size = new System.Drawing.Size(0, 15);
            this.music_duration.TabIndex = 6;
            // 
            // music_position
            // 
            this.music_position.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.music_position.AutoSize = true;
            this.music_position.Location = new System.Drawing.Point(3, 670);
            this.music_position.Name = "music_position";
            this.music_position.Size = new System.Drawing.Size(0, 15);
            this.music_position.TabIndex = 5;
            // 
            // ProgressBar_Background
            // 
            this.ProgressBar_Background.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Background.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ProgressBar_Background.CausesValidation = false;
            this.ProgressBar_Background.Controls.Add(this.ProgressBar);
            this.ProgressBar_Background.Enabled = false;
            this.ProgressBar_Background.Location = new System.Drawing.Point(571, 50);
            this.ProgressBar_Background.Name = "ProgressBar_Background";
            this.ProgressBar_Background.Size = new System.Drawing.Size(10, 645);
            this.ProgressBar_Background.TabIndex = 3;
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ProgressBar.Location = new System.Drawing.Point(0, 645);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(10, 0);
            this.ProgressBar.TabIndex = 0;
            // 
            // music
            // 
            this.music.AutoSize = true;
            this.music.Location = new System.Drawing.Point(70, 12);
            this.music.Name = "music";
            this.music.Size = new System.Drawing.Size(0, 15);
            this.music.TabIndex = 4;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(760, 12);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(10, 10);
            this.axWindowsMediaPlayer1.TabIndex = 6;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.music);
            this.Controls.Add(this.ProgressBar_Background);
            this.Controls.Add(this.ProgressBar_Bottom);
            this.Controls.Add(this.MainPanel_Background);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.SizeChanged += new System.EventHandler(this.form1_SizeChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.MainPanel_Background.ResumeLayout(false);
            this.ProgressBar_Bottom.ResumeLayout(false);
            this.ProgressBar_Bottom.PerformLayout();
            this.ProgressBar_Background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button SelectMusic;
        private System.Windows.Forms.Button PlayPause;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Panel MainPanel_Background;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel ProgressBar_Bottom;
        private System.Windows.Forms.Panel ProgressBar_Background;
        private System.Windows.Forms.Panel ProgressBar;
        private System.Windows.Forms.Label music;
        private System.Windows.Forms.Label music_position;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label music_duration;
    }
}

