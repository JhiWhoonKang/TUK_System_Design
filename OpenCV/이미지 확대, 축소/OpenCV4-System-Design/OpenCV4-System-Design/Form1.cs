using System;
using System.Drawing;
using System.Web;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace OpenCV4_System_Design
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // string imagePath = "C:\\JHIWHOON_ws\\Tech University of Korea\\LAB\\2023 시스템설계 교과목 개발\\채널분리 코드\\RGB Image.png";
            Bitmap img = Properties.Resources.pyr;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 원본 이미지 표시
            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);

            // 이미지 확대 (PyrUp)
            Mat pyrUpImage = new Mat();
            Cv2.PyrUp(src, pyrUpImage);

            // 이미지 축소 (PyrDown)
            Mat pyrDownImage = new Mat();
            Cv2.PyrDown(src, pyrDownImage);

            // 확대된 이미지와 축소된 이미지 표시
            PB_PYRUP.Image = BitmapConverter.ToBitmap(pyrUpImage);
            PB_PYRDOWN.Image = BitmapConverter.ToBitmap(pyrDownImage);
        }
    }
}