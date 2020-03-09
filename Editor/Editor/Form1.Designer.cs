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
            this.Settings = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.flowLayoutPanel1.Controls.Add(this.Settings);
            this.flowLayoutPanel1.Controls.Add(this.button5);
            this.flowLayoutPanel1.Controls.Add(this.button6);
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
            // Settings
            // 
            this.Settings.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Settings.Location = new System.Drawing.Point(3, 103);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(111, 44);
            this.Settings.TabIndex = 2;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
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
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // MainPanel_Background
            // 
            this.MainPanel_Background.AllowDrop = true;
            this.MainPanel_Background.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel_Background.BackColor = System.Drawing.Color.White;
            this.MainPanel_Background.Enabled = false;
            this.MainPanel_Background.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainPanel_Background.Location = new System.Drawing.Point(760, 33);
            this.MainPanel_Background.Name = "MainPanel_Background";
            this.MainPanel_Background.Size = new System.Drawing.Size(10, 40);
            this.MainPanel_Background.TabIndex = 1;
            this.MainPanel_Background.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Background_Paint);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.AutoScroll = true;
            this.MainPanel.BackColor = System.Drawing.Color.LightGray;
            this.MainPanel.Location = new System.Drawing.Point(90, 70);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(420, 10000);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.Click += new System.EventHandler(this.MainPanel_Click);
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            this.MainPanel.Move += new System.EventHandler(this.MainPanel_Move);
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(90, 688);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(421, 75);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Location = new System.Drawing.Point(90, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(420, 72);
            this.panel2.TabIndex = 8;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.music);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.ProgressBar_Background);
            this.Controls.Add(this.ProgressBar_Bottom);
            this.Controls.Add(this.MainPanel_Background);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.SizeChanged += new System.EventHandler(this.form1_SizeChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}

