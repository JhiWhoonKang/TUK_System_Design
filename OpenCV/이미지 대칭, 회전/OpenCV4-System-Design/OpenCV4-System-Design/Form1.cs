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
            Bitmap img = Properties.Resources.GRM2D;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 원본 이미지 표시
            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);

            // 이미지 대칭 (좌우 반전)
            Mat flipImage = new Mat();
            Cv2.Flip(src, flipImage, FlipMode.Y);

            // 이미지 회전 (GetRotationMatrix2D와 WarpAffine 사용)
            Mat rotationMatrix = Cv2.GetRotationMatrix2D(new Point2f(src.Width / 2, src.Height / 2), 45, 1);
            Mat rotatedImage = new Mat();
            Cv2.WarpAffine(src, rotatedImage, rotationMatrix, new OpenCvSharp.Size(src.Width, src.Height));

            // 대칭된 이미지와 회전된 이미지 표시
            PB_FLIP.Image = BitmapConverter.ToBitmap(flipImage);
            PB_ROTATE.Image = BitmapConverter.ToBitmap(rotatedImage);
        }
    }
}