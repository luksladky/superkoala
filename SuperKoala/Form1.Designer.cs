namespace SuperKoule
{
    partial class MainProgram
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainProgram));
            this.newgame_btn = new System.Windows.Forms.Button();
            this.pbGameCanvas = new System.Windows.Forms.PictureBox();
            this.replay_btn = new System.Windows.Forms.Button();
            this.pause_btn = new System.Windows.Forms.Button();
            this.congrat_btn = new System.Windows.Forms.Button();
            this.next_level_btn = new System.Windows.Forms.Button();
            this.superkoala_header = new System.Windows.Forms.PictureBox();
            this.retry_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.superkoala_header)).BeginInit();
            this.SuspendLayout();
            // 
            // newgame_btn
            // 
            this.newgame_btn.BackgroundImage = global::SuperKoule.Properties.Resources.new_game;
            this.newgame_btn.FlatAppearance.BorderSize = 0;
            this.newgame_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.newgame_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.newgame_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newgame_btn.Location = new System.Drawing.Point(412, 333);
            this.newgame_btn.Name = "newgame_btn";
            this.newgame_btn.Size = new System.Drawing.Size(300, 100);
            this.newgame_btn.TabIndex = 1;
            this.newgame_btn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.newgame_btn.UseVisualStyleBackColor = true;
            this.newgame_btn.Click += new System.EventHandler(this.Newgame);
            // 
            // pbGameCanvas
            // 
            this.pbGameCanvas.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbGameCanvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbGameCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbGameCanvas.Margin = new System.Windows.Forms.Padding(0);
            this.pbGameCanvas.Name = "pbGameCanvas";
            this.pbGameCanvas.Size = new System.Drawing.Size(1193, 878);
            this.pbGameCanvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGameCanvas.TabIndex = 2;
            this.pbGameCanvas.TabStop = false;
            // 
            // replay_btn
            // 
            this.replay_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("replay_btn.BackgroundImage")));
            this.replay_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.replay_btn.FlatAppearance.BorderSize = 0;
            this.replay_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.replay_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.replay_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.replay_btn.Location = new System.Drawing.Point(1085, 12);
            this.replay_btn.Name = "replay_btn";
            this.replay_btn.Size = new System.Drawing.Size(47, 48);
            this.replay_btn.TabIndex = 3;
            this.replay_btn.UseVisualStyleBackColor = false;
            this.replay_btn.Click += new System.EventHandler(this.Retry);
            // 
            // pause_btn
            // 
            this.pause_btn.BackColor = System.Drawing.Color.Transparent;
            this.pause_btn.BackgroundImage = global::SuperKoule.Properties.Resources.hud_pause;
            this.pause_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pause_btn.FlatAppearance.BorderSize = 0;
            this.pause_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.pause_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.pause_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pause_btn.Location = new System.Drawing.Point(1138, 12);
            this.pause_btn.Name = "pause_btn";
            this.pause_btn.Size = new System.Drawing.Size(47, 48);
            this.pause_btn.TabIndex = 5;
            this.pause_btn.UseVisualStyleBackColor = false;
            this.pause_btn.Click += new System.EventHandler(this.pause_btn_Click);
            // 
            // congrat_btn
            // 
            this.congrat_btn.BackgroundImage = global::SuperKoule.Properties.Resources.congratulations;
            this.congrat_btn.FlatAppearance.BorderSize = 0;
            this.congrat_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.congrat_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.congrat_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.congrat_btn.Location = new System.Drawing.Point(383, 146);
            this.congrat_btn.Name = "congrat_btn";
            this.congrat_btn.Size = new System.Drawing.Size(378, 68);
            this.congrat_btn.TabIndex = 6;
            this.congrat_btn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.congrat_btn.UseVisualStyleBackColor = true;
            // 
            // next_level_btn
            // 
            this.next_level_btn.BackgroundImage = global::SuperKoule.Properties.Resources.next_level;
            this.next_level_btn.FlatAppearance.BorderSize = 0;
            this.next_level_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.next_level_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.next_level_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.next_level_btn.Location = new System.Drawing.Point(435, 457);
            this.next_level_btn.Name = "next_level_btn";
            this.next_level_btn.Size = new System.Drawing.Size(232, 56);
            this.next_level_btn.TabIndex = 7;
            this.next_level_btn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.next_level_btn.UseVisualStyleBackColor = true;
            this.next_level_btn.Click += new System.EventHandler(this.Nextlevel);
            // 
            // superkoala_header
            // 
            this.superkoala_header.BackColor = System.Drawing.Color.Transparent;
            this.superkoala_header.BackgroundImage = global::SuperKoule.Properties.Resources.superkoala;
            this.superkoala_header.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.superkoala_header.Location = new System.Drawing.Point(401, 146);
            this.superkoala_header.Name = "superkoala_header";
            this.superkoala_header.Size = new System.Drawing.Size(327, 89);
            this.superkoala_header.TabIndex = 8;
            this.superkoala_header.TabStop = false;
            // 
            // retry_btn
            // 
            this.retry_btn.BackgroundImage = global::SuperKoule.Properties.Resources.retry;
            this.retry_btn.FlatAppearance.BorderSize = 0;
            this.retry_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.retry_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.retry_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.retry_btn.Location = new System.Drawing.Point(467, 531);
            this.retry_btn.Name = "retry_btn";
            this.retry_btn.Size = new System.Drawing.Size(157, 56);
            this.retry_btn.TabIndex = 9;
            this.retry_btn.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.retry_btn.UseVisualStyleBackColor = true;
            this.retry_btn.Click += new System.EventHandler(this.Retry);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 873);
            this.Controls.Add(this.retry_btn);
            this.Controls.Add(this.superkoala_header);
            this.Controls.Add(this.next_level_btn);
            this.Controls.Add(this.congrat_btn);
            this.Controls.Add(this.pause_btn);
            this.Controls.Add(this.replay_btn);
            this.Controls.Add(this.newgame_btn);
            this.Controls.Add(this.pbGameCanvas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "Form1";
            this.Text = "SuperKoala";
            this.SizeChanged += new System.EventHandler(this.resize_game);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbGameCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.superkoala_header)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newgame_btn;
        private System.Windows.Forms.PictureBox pbGameCanvas;
        private System.Windows.Forms.Button replay_btn;
        private System.Windows.Forms.Button pause_btn;
        private System.Windows.Forms.Button congrat_btn;
        private System.Windows.Forms.Button next_level_btn;
        private System.Windows.Forms.PictureBox superkoala_header;
        private System.Windows.Forms.Button retry_btn;

    }
}

