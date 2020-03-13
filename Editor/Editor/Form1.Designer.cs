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
            this.ProgressBar_Bottom = new System.Windows.Forms.Panel();
            this.music_duration = new System.Windows.Forms.Label();
            this.music_position = new System.Windows.Forms.Label();
            this.ProgressBar_Background = new System.Windows.Forms.Panel();
            this.ProgressBar = new System.Windows.Forms.Panel();
            this.music = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Unlink = new System.Windows.Forms.Button();
            this.Link = new System.Windows.Forms.Button();
            this.Swipe = new System.Windows.Forms.Button();
            this.Left = new System.Windows.Forms.Button();
            this.Up = new System.Windows.Forms.Button();
            this.Right = new System.Windows.Forms.Button();
            this.Down = new System.Windows.Forms.Button();
            this.Hold = new System.Windows.Forms.Button();
            this.Click = new System.Windows.Forms.Button();
            this.Fraction_2 = new System.Windows.Forms.Label();
            this.Fraction_3 = new System.Windows.Forms.Label();
            this.Fraction_4 = new System.Windows.Forms.Label();
            this.Fraction_8 = new System.Windows.Forms.Label();
            this.MainPanel = new Editor.BackgroundPanel();
            this.MainPanel_Background = new Editor.BackgroundPanel();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.CurrentSectionName = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.ProgressBar_Bottom.SuspendLayout();
            this.ProgressBar_Background.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(630, 100);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(122, 615);
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
            // ProgressBar_Bottom
            // 
            this.ProgressBar_Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar_Bottom.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBar_Bottom.Controls.Add(this.music_duration);
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
            this.music_position.Location = new System.Drawing.Point(553, 700);
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
            this.music.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.music.AutoSize = true;
            this.music.Location = new System.Drawing.Point(634, 59);
            this.music.Name = "music";
            this.music.Size = new System.Drawing.Size(0, 15);
            this.music.TabIndex = 4;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Unlink
            // 
            this.Unlink.BackColor = System.Drawing.Color.Aquamarine;
            this.Unlink.Image = global::Editor.Properties.Resources.unlink;
            this.Unlink.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Unlink.Location = new System.Drawing.Point(25, 100);
            this.Unlink.Name = "Unlink";
            this.Unlink.Size = new System.Drawing.Size(32, 32);
            this.Unlink.TabIndex = 16;
            this.Unlink.UseVisualStyleBackColor = false;
            // 
            // Link
            // 
            this.Link.BackColor = System.Drawing.Color.Aquamarine;
            this.Link.Image = global::Editor.Properties.Resources.link_1_;
            this.Link.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Link.Location = new System.Drawing.Point(25, 100);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(32, 32);
            this.Link.TabIndex = 15;
            this.Link.UseVisualStyleBackColor = false;
            // 
            // Swipe
            // 
            this.Swipe.BackColor = System.Drawing.Color.Gray;
            this.Swipe.Image = global::Editor.Properties.Resources.swipe;
            this.Swipe.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Swipe.Location = new System.Drawing.Point(25, 150);
            this.Swipe.Name = "Swipe";
            this.Swipe.Size = new System.Drawing.Size(32, 32);
            this.Swipe.TabIndex = 10;
            this.Swipe.UseVisualStyleBackColor = false;
            this.Swipe.Click += new System.EventHandler(this.Swipe_Click);
            // 
            // Left
            // 
            this.Left.BackColor = System.Drawing.Color.Aquamarine;
            this.Left.Enabled = false;
            this.Left.Image = global::Editor.Properties.Resources.left;
            this.Left.Location = new System.Drawing.Point(25, 150);
            this.Left.Name = "Left";
            this.Left.Size = new System.Drawing.Size(32, 32);
            this.Left.TabIndex = 14;
            this.Left.UseVisualStyleBackColor = false;
            // 
            // Up
            // 
            this.Up.BackColor = System.Drawing.Color.Aquamarine;
            this.Up.Enabled = false;
            this.Up.Image = global::Editor.Properties.Resources.up;
            this.Up.Location = new System.Drawing.Point(25, 150);
            this.Up.Name = "Up";
            this.Up.Size = new System.Drawing.Size(32, 32);
            this.Up.TabIndex = 13;
            this.Up.UseVisualStyleBackColor = false;
            // 
            // Right
            // 
            this.Right.BackColor = System.Drawing.Color.Aquamarine;
            this.Right.Enabled = false;
            this.Right.Image = global::Editor.Properties.Resources.right;
            this.Right.Location = new System.Drawing.Point(25, 150);
            this.Right.Name = "Right";
            this.Right.Size = new System.Drawing.Size(32, 32);
            this.Right.TabIndex = 12;
            this.Right.UseVisualStyleBackColor = false;
            // 
            // Down
            // 
            this.Down.BackColor = System.Drawing.Color.Aquamarine;
            this.Down.Enabled = false;
            this.Down.Image = global::Editor.Properties.Resources.download;
            this.Down.Location = new System.Drawing.Point(25, 150);
            this.Down.Name = "Down";
            this.Down.Size = new System.Drawing.Size(32, 32);
            this.Down.TabIndex = 11;
            this.Down.UseVisualStyleBackColor = false;
            // 
            // Hold
            // 
            this.Hold.BackColor = System.Drawing.Color.Gray;
            this.Hold.Image = global::Editor.Properties.Resources.hold;
            this.Hold.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Hold.Location = new System.Drawing.Point(25, 100);
            this.Hold.Name = "Hold";
            this.Hold.Size = new System.Drawing.Size(32, 32);
            this.Hold.TabIndex = 9;
            this.Hold.UseVisualStyleBackColor = false;
            this.Hold.Click += new System.EventHandler(this.Hold_Click);
            // 
            // Click
            // 
            this.Click.BackColor = System.Drawing.Color.Aquamarine;
            this.Click.Image = global::Editor.Properties.Resources.click_2_;
            this.Click.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Click.Location = new System.Drawing.Point(25, 50);
            this.Click.Name = "Click";
            this.Click.Size = new System.Drawing.Size(32, 32);
            this.Click.TabIndex = 8;
            this.Click.UseVisualStyleBackColor = false;
            this.Click.Click += new System.EventHandler(this.Click_Click);
            // 
            // Fraction_2
            // 
            this.Fraction_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Fraction_2.AutoSize = true;
            this.Fraction_2.BackColor = System.Drawing.SystemColors.Control;
            this.Fraction_2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Fraction_2.Location = new System.Drawing.Point(633, 30);
            this.Fraction_2.Name = "Fraction_2";
            this.Fraction_2.Size = new System.Drawing.Size(24, 20);
            this.Fraction_2.TabIndex = 17;
            this.Fraction_2.Text = "½";
            this.Fraction_2.Click += new System.EventHandler(this.Fraction_2_Click);
            // 
            // Fraction_3
            // 
            this.Fraction_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Fraction_3.AutoSize = true;
            this.Fraction_3.BackColor = System.Drawing.SystemColors.Control;
            this.Fraction_3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Fraction_3.Location = new System.Drawing.Point(658, 30);
            this.Fraction_3.Name = "Fraction_3";
            this.Fraction_3.Size = new System.Drawing.Size(22, 20);
            this.Fraction_3.TabIndex = 18;
            this.Fraction_3.Text = "⅓";
            this.Fraction_3.Click += new System.EventHandler(this.Fraction_3_Click);
            // 
            // Fraction_4
            // 
            this.Fraction_4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Fraction_4.AutoSize = true;
            this.Fraction_4.BackColor = System.Drawing.SystemColors.Control;
            this.Fraction_4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Fraction_4.Location = new System.Drawing.Point(683, 30);
            this.Fraction_4.Name = "Fraction_4";
            this.Fraction_4.Size = new System.Drawing.Size(24, 20);
            this.Fraction_4.TabIndex = 19;
            this.Fraction_4.Text = "¼";
            this.Fraction_4.Click += new System.EventHandler(this.Fraction_4_Click);
            // 
            // Fraction_8
            // 
            this.Fraction_8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Fraction_8.AutoSize = true;
            this.Fraction_8.BackColor = System.Drawing.SystemColors.Control;
            this.Fraction_8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Fraction_8.Location = new System.Drawing.Point(708, 30);
            this.Fraction_8.Name = "Fraction_8";
            this.Fraction_8.Size = new System.Drawing.Size(22, 20);
            this.Fraction_8.TabIndex = 20;
            this.Fraction_8.Text = "⅛";
            this.Fraction_8.Click += new System.EventHandler(this.Fraction_8_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Controls.Add(this.MainPanel_Background);
            this.MainPanel.Location = new System.Drawing.Point(90, -2300);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(420, 5000);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.Click += new System.EventHandler(this.MainPanel_Click);
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            this.MainPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseDown);
            this.MainPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseMove);
            this.MainPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseUp);
            this.MainPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainPanel_MouseWheel);
            // 
            // MainPanel_Background
            // 
            this.MainPanel_Background.AllowDrop = true;
            this.MainPanel_Background.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel_Background.BackColor = System.Drawing.Color.Transparent;
            this.MainPanel_Background.Enabled = false;
            this.MainPanel_Background.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainPanel_Background.Location = new System.Drawing.Point(0, 0);
            this.MainPanel_Background.Name = "MainPanel_Background";
            this.MainPanel_Background.Size = new System.Drawing.Size(421, 5000);
            this.MainPanel_Background.TabIndex = 1;
            this.MainPanel_Background.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Background_Paint);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BottomPanel.Controls.Add(this.CurrentSectionName);
            this.BottomPanel.Location = new System.Drawing.Point(90, 700);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(421, 63);
            this.BottomPanel.TabIndex = 7;
            // 
            // CurrentSectionName
            // 
            this.CurrentSectionName.AutoSize = true;
            this.CurrentSectionName.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CurrentSectionName.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CurrentSectionName.Location = new System.Drawing.Point(14, 24);
            this.CurrentSectionName.Name = "CurrentSectionName";
            this.CurrentSectionName.Size = new System.Drawing.Size(63, 20);
            this.CurrentSectionName.TabIndex = 17;
            this.CurrentSectionName.Text = "Section";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.Fraction_8);
            this.Controls.Add(this.Fraction_4);
            this.Controls.Add(this.Fraction_3);
            this.Controls.Add(this.Fraction_2);
            this.Controls.Add(this.Hold);
            this.Controls.Add(this.Unlink);
            this.Controls.Add(this.Link);
            this.Controls.Add(this.Swipe);
            this.Controls.Add(this.Left);
            this.Controls.Add(this.Up);
            this.Controls.Add(this.Right);
            this.Controls.Add(this.Down);
            this.Controls.Add(this.Click);
            this.Controls.Add(this.music);
            this.Controls.Add(this.music_position);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Controls.Add(this.ProgressBar_Background);
            this.Controls.Add(this.ProgressBar_Bottom);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form1";
            this.SizeChanged += new System.EventHandler(this.form1_SizeChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ProgressBar_Bottom.ResumeLayout(false);
            this.ProgressBar_Bottom.PerformLayout();
            this.ProgressBar_Background.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
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
        private System.Windows.Forms.Panel ProgressBar_Bottom;
        private System.Windows.Forms.Panel ProgressBar_Background;
        private System.Windows.Forms.Panel ProgressBar;
        private System.Windows.Forms.Label music;
        private System.Windows.Forms.Label music_position;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label music_duration;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Button Click;
        private System.Windows.Forms.Button Hold;
        private System.Windows.Forms.Button Swipe;
        private BackgroundPanel MainPanel_Background;
        private BackgroundPanel MainPanel;
        private System.Windows.Forms.Button Down;
        private System.Windows.Forms.Button Right;
        private System.Windows.Forms.Button Up;
        private System.Windows.Forms.Button Left;
        private System.Windows.Forms.Button Link;
        private System.Windows.Forms.Button Unlink;
        private System.Windows.Forms.Label CurrentSectionName;
        private System.Windows.Forms.Label Fraction_2;
        private System.Windows.Forms.Label Fraction_3;
        private System.Windows.Forms.Label Fraction_4;
        private System.Windows.Forms.Label Fraction_8;
    }
}

