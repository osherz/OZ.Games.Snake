﻿namespace SnakeGame.SnakeBoard
{
    partial class Board
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerOfSnakeMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerOfSnakeMove
            // 
            this.timerOfSnakeMove.Interval = 1;
            this.timerOfSnakeMove.Tick += new System.EventHandler(this.timerOfSnakeMove_Tick);
            // 
            // Board
            // 
            this.BackgroundImage = global::SnakeGame.Properties.Resources.back;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerOfSnakeMove;
    }
}