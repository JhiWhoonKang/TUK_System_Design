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
            this.PB_CH_R = new System.Windows.Forms.PictureBox();
            this.PB_CH_G = new System.Windows.Forms.PictureBox();
            this.PB_CH_B = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORIGINAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CH_R)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CH_G)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CH_B)).BeginInit();
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
            // PB_CH_R
            // 
            this.PB_CH_R.Location = new System.Drawing.Point(425, 25);
            this.PB_CH_R.Margin = new System.Windows.Forms.Padding(2);
            this.PB_CH_R.Name = "PB_CH_R";
            this.PB_CH_R.Size = new System.Drawing.Size(381, 448);
            this.PB_CH_R.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_CH_R.TabIndex = 1;
            this.PB_CH_R.TabStop = false;
            // 
            // PB_CH_G
            // 
            this.PB_CH_G.Location = new System.Drawing.Point(810, 25);
            this.PB_CH_G.Margin = new System.Windows.Forms.Padding(2);
            this.PB_CH_G.Name = "PB_CH_G";
            this.PB_CH_G.Size = new System.Drawing.Size(381, 448);
            this.PB_CH_G.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_CH_G.TabIndex = 2;
            this.PB_CH_G.TabStop = false;
            // 
            // PB_CH_B
            // 
            this.PB_CH_B.Location = new System.Drawing.Point(1195, 23);
            this.PB_CH_B.Margin = new System.Windows.Forms.Padding(2);
            this.PB_CH_B.Name = "PB_CH_B";
            this.PB_CH_B.Size = new System.Drawing.Size(381, 448);
            this.PB_CH_B.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_CH_B.TabIndex = 3;
            this.PB_CH_B.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(423, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ch R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(808, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ch G";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1193, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ch B";
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
            this.ClientSize = new System.Drawing.Size(1591, 489);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PB_CH_B);
            this.Controls.Add(this.PB_CH_G);
            this.Controls.Add(this.PB_CH_R);
            this.Controls.Add(this.PB_ORIGINAL);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORIGINAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CH_R)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CH_G)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CH_B)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_ORIGINAL;
        private System.Windows.Forms.PictureBox PB_CH_R;
        private System.Windows.Forms.PictureBox PB_CH_G;
        private System.Windows.Forms.PictureBox PB_CH_B;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

