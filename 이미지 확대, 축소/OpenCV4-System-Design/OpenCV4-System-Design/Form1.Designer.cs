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
            this.PB_PYRUP = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PB_PYRDOWN = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORIGINAL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PYRUP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PYRDOWN)).BeginInit();
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
            // PB_PYRUP
            // 
            this.PB_PYRUP.Location = new System.Drawing.Point(418, 25);
            this.PB_PYRUP.Margin = new System.Windows.Forms.Padding(2);
            this.PB_PYRUP.Name = "PB_PYRUP";
            this.PB_PYRUP.Size = new System.Drawing.Size(381, 448);
            this.PB_PYRUP.TabIndex = 3;
            this.PB_PYRUP.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "PyrUp Image";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(820, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "PyrDown Image";
            // 
            // PB_PYRDOWN
            // 
            this.PB_PYRDOWN.Location = new System.Drawing.Point(822, 25);
            this.PB_PYRDOWN.Margin = new System.Windows.Forms.Padding(2);
            this.PB_PYRDOWN.Name = "PB_PYRDOWN";
            this.PB_PYRDOWN.Size = new System.Drawing.Size(381, 448);
            this.PB_PYRDOWN.TabIndex = 8;
            this.PB_PYRDOWN.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 489);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PB_PYRDOWN);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PB_PYRUP);
            this.Controls.Add(this.PB_ORIGINAL);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORIGINAL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PYRUP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PYRDOWN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_ORIGINAL;
        private System.Windows.Forms.PictureBox PB_PYRUP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PB_PYRDOWN;
    }
}

