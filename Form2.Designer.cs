namespace Minesweeper
{
    partial class Form2
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
            //this.flagCounterLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flagCounterLabel
            // 
            //this.flagCounterLabel.Font = new System.Drawing.Font("MingLiU_HKSCS-ExtB", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.flagCounterLabel.Location = new System.Drawing.Point(209, 55);
            //this.flagCounterLabel.Name = "flagCounterLabel";
            //this.flagCounterLabel.Size = new System.Drawing.Size(200, 40);
            //this.flagCounterLabel.TabIndex = 7;
            //this.flagCounterLabel.Text = "Flags remaining: ";
            //this.flagCounterLabel.Click += new System.EventHandler(this.flagCounterLabel_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 661);
            //this.Controls.Add(this.flagCounterLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion
        //private System.Windows.Forms.Label flagCounterLabel;
    }
}