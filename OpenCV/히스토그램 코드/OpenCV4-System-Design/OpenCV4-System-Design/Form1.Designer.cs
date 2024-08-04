namespace OpenCV4_System_Design
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.PB_ORIGINAL = new System.Windows.Forms.PictureBox();
            this.PB_HIST = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORIGINAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_HIST)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_ORIGINAL
            // 
            this.PB_ORIGINAL.Location = new System.Drawing.Point(14, 25);
            this.PB_ORIGINAL.Margin = new System.Windows.Forms.Padding(2);
            this.PB_ORIGINAL.Name = "PB_ORIGINAL";
            this.PB_ORIGINAL.Size = new System.Drawing.Size(381, 448);
            this.PB_ORIGINAL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_ORIGINAL.TabIndex = 0;
            this.PB_ORIGINAL.TabStop = false;
            // 
            // PB_HIST
            // 
            this.PB_HIST.Location = new System.Drawing.Point(425, 25);
            this.PB_HIST.Margin = new System.Windows.Forms.Padding(2);
            this.PB_HIST.Name = "PB_HIST";
            this.PB_HIST.Size = new System.Drawing.Size(381, 448);
            this.PB_HIST.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_HIST.TabIndex = 1;
            this.PB_HIST.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hist Image";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Original Image";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 489);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PB_HIST);
            this.Controls.Add(this.PB_ORIGINAL);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORIGINAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_HIST)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_ORIGINAL;
        private System.Windows.Forms.PictureBox PB_HIST;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}

