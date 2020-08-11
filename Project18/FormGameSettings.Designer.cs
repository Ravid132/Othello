namespace OthelloGUI
{
    partial class FormGameSettings
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
            this.ButtonBoardSize = new System.Windows.Forms.Button();
            this.buttonPlayAgainstComputer = new System.Windows.Forms.Button();
            this.buttonPlayAgainstFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ButtonBoardSize
            // 
            this.ButtonBoardSize.Location = new System.Drawing.Point(20, 67);
            this.ButtonBoardSize.Name = "ButtonBoardSize";
            this.ButtonBoardSize.Size = new System.Drawing.Size(526, 62);
            this.ButtonBoardSize.TabIndex = 0;
            this.ButtonBoardSize.TabStop = false;
            this.ButtonBoardSize.Text = "Board Size : 6x6(click to increase)";
            this.ButtonBoardSize.UseVisualStyleBackColor = true;
            this.ButtonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonPlayAgainstComputer
            // 
            this.buttonPlayAgainstComputer.Location = new System.Drawing.Point(20, 173);
            this.buttonPlayAgainstComputer.Name = "buttonPlayAgainstComputer";
            this.buttonPlayAgainstComputer.Size = new System.Drawing.Size(250, 61);
            this.buttonPlayAgainstComputer.TabIndex = 1;
            this.buttonPlayAgainstComputer.Text = "Play against the computer";
            this.buttonPlayAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstComputer.Click += new System.EventHandler(this.buttonPlayAgainstComputer_Click);
            // 
            // buttonPlayAgainstFriend
            // 
            this.buttonPlayAgainstFriend.Location = new System.Drawing.Point(281, 173);
            this.buttonPlayAgainstFriend.Name = "buttonPlayAgainstFriend";
            this.buttonPlayAgainstFriend.Size = new System.Drawing.Size(265, 61);
            this.buttonPlayAgainstFriend.TabIndex = 2;
            this.buttonPlayAgainstFriend.Text = "Play against your friend";
            this.buttonPlayAgainstFriend.UseVisualStyleBackColor = true;
            this.buttonPlayAgainstFriend.Click += new System.EventHandler(this.buttonPlayAgainstFriend_Click);
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 246);
            this.Controls.Add(this.buttonPlayAgainstFriend);
            this.Controls.Add(this.buttonPlayAgainstComputer);
            this.Controls.Add(this.ButtonBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.GameSettings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonBoardSize;
        private System.Windows.Forms.Button buttonPlayAgainstComputer;
        private System.Windows.Forms.Button buttonPlayAgainstFriend;
    }
}