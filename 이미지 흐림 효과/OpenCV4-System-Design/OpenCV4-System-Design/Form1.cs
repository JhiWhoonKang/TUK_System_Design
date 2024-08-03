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
            Bitmap img = Properties.Resources.blur;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 원본 이미지 표시
            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);

            // 이미지 확대 (Resize, 이웃 보간법 사용)
            Mat sizeUpImage = new Mat();
            Cv2.Resize(src, sizeUpImage, new OpenCvSharp.Size(src.Width * 2, src.Height * 2), 0, 0, InterpolationFlags.Nearest);

            // 이미지 축소 (Resize, 이웃 보간법 사용)
            Mat sizeDownImage = new Mat();
            Cv2.Resize(src, sizeDownImage, new OpenCvSharp.Size(src.Width / 2, src.Height / 2), 0, 0, InterpolationFlags.Nearest);

            // 이미지 블러 처리 (GaussianBlur 사용)
            Mat blurredImage = new Mat();
            Cv2.GaussianBlur(src, blurredImage, new OpenCvSharp.Size(15, 15), 0);

            // 확대된 이미지, 축소된 이미지, 블러 처리된 이미지 표시
            PB_BLUR.Image = BitmapConverter.ToBitmap(blurredImage);
        }
    }
}