using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace TermpTest
{
    public partial class ContourForm : Form
    {
        private Define C_DEFINE;
        public ContourForm()
        {
            InitializeComponent();

            C_DEFINE = new Define();
            PB_CONTOUR.Width = C_DEFINE._WIDTH;
            PB_CONTOUR.Height = C_DEFINE._HEIGHT;
            PB_CONTOUR.Location = new System.Drawing.Point(0, 0);
        }

        public void UpdateImage(Bitmap image)
        {
            PB_CONTOUR.Image = image; // PictureBox에 이미지 업데이트
        }
    }
}
