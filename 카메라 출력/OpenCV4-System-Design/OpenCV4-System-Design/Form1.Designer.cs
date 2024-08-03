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
            this.LB_CAMERA = new System.Windows.Forms.Label();
            this.LB_IMAGE = new System.Windows.Forms.Label();
            this.PB_IMAGE = new System.Windows.Forms.PictureBox();
            this.PB_CAMERA = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PB_IMAGE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CAMERA)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_CAMERA
            // 
            this.LB_CAMERA.AutoSize = true;
            this.LB_CAMERA.Location = new System.Drawing.Point(12, 11);
            this.LB_CAMERA.Name = "LB_CAMERA";
            this.LB_CAMERA.Size = new System.Drawing.Size(50, 12);
            this.LB_CAMERA.TabIndex = 7;
            this.LB_CAMERA.Text = "Camera";
            // 
            // LB_IMAGE
            // 
            this.LB_IMAGE.AutoSize = true;
            this.LB_IMAGE.Location = new System.Drawing.Point(658, 11);
            this.LB_IMAGE.Name = "LB_IMAGE";
            this.LB_IMAGE.Size = new System.Drawing.Size(40, 12);
            this.LB_IMAGE.TabIndex = 10;
            this.LB_IMAGE.Text = "Image";
            // 
            // PB_IMAGE
            // 
            this.PB_IMAGE.Location = new System.Drawing.Point(660, 26);
            this.PB_IMAGE.Name = "PB_IMAGE";
            this.PB_IMAGE.Size = new System.Drawing.Size(484, 480);
            this.PB_IMAGE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PB_IMAGE.TabIndex = 9;
            this.PB_IMAGE.TabStop = false;
            // 
            // PB_CAMERA
            // 
            this.PB_CAMERA.Location = new System.Drawing.Point(14, 26);
            this.PB_CAMERA.Name = "PB_CAMERA";
            this.PB_CAMERA.Size = new System.Drawing.Size(640, 480);
            this.PB_CAMERA.TabIndex = 8;
            this.PB_CAMERA.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 516);
            this.Controls.Add(this.LB_IMAGE);
            this.Controls.Add(this.PB_IMAGE);
            this.Controls.Add(this.PB_CAMERA);
            this.Controls.Add(this.LB_CAMERA);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PB_IMAGE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_CAMERA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LB_CAMERA;
        private System.Windows.Forms.PictureBox PB_CAMERA;
        private System.Windows.Forms.PictureBox PB_IMAGE;
        private System.Windows.Forms.Label LB_IMAGE;
    }
}

