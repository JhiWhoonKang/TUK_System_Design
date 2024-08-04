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
            Bitmap img = Properties.Resources.affine;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 원본 이미지 표시
            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);

            // Affine 변환
            // 원본 이미지의 세 점 (Affine 변환 전)
            Point2f[] srcPoints = {
                new Point2f(0, 0),
                new Point2f(src.Cols - 1, 0),
                new Point2f(0, src.Rows - 1)
            };

            // 변환 후 위치할 세 점 (Affine 변환 후)
            Point2f[] dstPoints = {
                new Point2f(0, src.Rows * 0.33f),
                new Point2f(src.Cols * 0.85f, src.Rows * 0.25f),
                new Point2f(src.Cols * 0.15f, src.Rows * 0.7f)
            };

            // Affine 변환 매트릭스 계산
            Mat affineMatrix = Cv2.GetAffineTransform(srcPoints, dstPoints);

            // Affine 변환 적용
            Mat dst = new Mat();
            Cv2.WarpAffine(src, dst, affineMatrix, src.Size());

            // 변환된 이미지 표시
            PB_AFFINE.Image = BitmapConverter.ToBitmap(dst);
        }
    }
}