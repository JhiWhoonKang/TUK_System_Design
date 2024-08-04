using System;
using System.Drawing;
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
            // Resources 폴더에서 'sobel' 이미지를 로드하여 Bitmap 객체로 변환
            Bitmap img = Properties.Resources.sobel;

            // Bitmap 이미지를 OpenCV의 Mat 객체로 변환
            Mat src = BitmapConverter.ToMat(img);
            Mat dst = new Mat();

            // 이미지가 비어 있는지 확인
            if (src.Empty())
            {
                MessageBox.Show("Image Load Err");
                return;
            }

            // 소벨 필터를 적용하여 가장자리 검출
            Cv2.Sobel(src, dst, MatType.CV_8UC1, 1, 0, 3, 1, 0, BorderTypes.Reflect101);

            // 이미지를 시계 방향으로 90도 회전
            Mat rotatedDst = new Mat();
            Cv2.Rotate(dst, rotatedDst, RotateFlags.Rotate90Clockwise);

            // 회전된 이미지를 절반 크기로 리사이즈
            Mat resizedDst = new Mat();
            Cv2.Resize(rotatedDst, resizedDst, new OpenCvSharp.Size(rotatedDst.Width / 2, rotatedDst.Height / 2));

            // 결과 이미지를 화면에 표시
            Cv2.ImShow("Sobel", resizedDst);
        }
    }
}
