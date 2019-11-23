namespace SnakeGame
{
    partial class MainGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainGame));
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.speedNum = new System.Windows.Forms.NumericUpDown();
            this.buttonPause = new System.Windows.Forms.Button();
            this.labelEndMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.speedNum)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStartGame.Font = new System.Drawing.Font("Metal Hebrew", 20F, System.Drawing.FontStyle.Bold);
            this.buttonStartGame.ForeColor = System.Drawing.Color.Blue;
            this.buttonStartGame.Location = new System.Drawing.Point(331, 437);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(158, 39);
            this.buttonStartGame.TabIndex = 0;
            this.buttonStartGame.Text = "משחק חדש";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.button1_Click);
            // 
            // speedNum
            // 
            this.speedNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.speedNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.speedNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F);
            this.speedNum.Location = new System.Drawing.Point(279, 438);
            this.speedNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.speedNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.speedNum.Name = "speedNum";
            this.speedNum.Size = new System.Drawing.Size(46, 37);
            this.speedNum.TabIndex = 1;
            this.speedNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.speedNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPause.Enabled = false;
            this.buttonPause.Font = new System.Drawing.Font("Lee", 15F);
            this.buttonPause.ForeColor = System.Drawing.Color.Blue;
            this.buttonPause.Location = new System.Drawing.Point(204, 437);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(69, 39);
            this.buttonPause.TabIndex = 2;
            this.buttonPause.Text = "השהה";
            this.buttonPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // labelEndMessage
            // 
            this.labelEndMessage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelEndMessage.AutoSize = true;
            this.labelEndMessage.BackColor = System.Drawing.Color.Maroon;
            this.labelEndMessage.Font = new System.Drawing.Font("Ahron HalulM", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelEndMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.labelEndMessage.Location = new System.Drawing.Point(170, 215);
            this.labelEndMessage.Name = "labelEndMessage";
            this.labelEndMessage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelEndMessage.Size = new System.Drawing.Size(173, 55);
            this.labelEndMessage.TabIndex = 3;
            this.labelEndMessage.Text = "בהצלחה";
            this.labelEndMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelEndMessage.Visible = false;
            // 
            // MainGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::SnakeGame.Properties.Resources.field;
            this.ClientSize = new System.Drawing.Size(501, 488);
            this.Controls.Add(this.labelEndMessage);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.speedNum);
            this.Controls.Add(this.buttonStartGame);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "סנייק";
            this.Load += new System.EventHandler(this.MainGame_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainGame_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.speedNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.NumericUpDown speedNum;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Label labelEndMessage;
    }
}

