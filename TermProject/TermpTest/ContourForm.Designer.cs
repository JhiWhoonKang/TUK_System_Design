namespace TermpTest
{
    partial class ContourForm
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
            this.PB_CONTOUR = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CONTOUR)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_CONTOUR
            // 
            this.PB_CONTOUR.Location = new System.Drawing.Point(0, 0);
            this.PB_CONTOUR.Margin = new System.Windows.Forms.Padding(0);
            this.PB_CONTOUR.Name = "PB_CONTOUR";
            this.PB_CONTOUR.Size = new System.Drawing.Size(640, 480);
            this.PB_CONTOUR.TabIndex = 0;
            this.PB_CONTOUR.TabStop = false;
            // 
            // ContourForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(639, 480);
            this.Controls.Add(this.PB_CONTOUR);
            this.Name = "ContourForm";
            this.Text = "ContourForm";
            ((System.ComponentModel.ISupportInitialize)(this.PB_CONTOUR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_CONTOUR;
    }
}