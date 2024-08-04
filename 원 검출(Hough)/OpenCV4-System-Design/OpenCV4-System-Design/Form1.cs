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
            // Resources 폴더에서 'circle_hough' 이미지를 로드하여 Bitmap 객체로 변환
            Bitmap img = Properties.Resources.circle_hough;
            // Bitmap 이미지를 OpenCV의 Mat 객체로 변환
            Mat src = BitmapConverter.ToMat(img);

            // 이미지가 비어 있는지 확인
            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 필요한 Mat 객체 초기화
            Mat image = new Mat();   // 그레이스케일 이미지
            Mat dst = src.Clone();   // 원본 이미지를 복제한 이미지

            // 구조 요소 커널 생성 (모폴로지 연산에 사용)
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));

            // 원본 이미지를 그레이스케일로 변환
            Cv2.CvtColor(src, image, ColorConversionCodes.BGR2GRAY);
            // 그레이스케일 이미지를 팽창 연산 (3회 반복)
            Cv2.Dilate(image, image, kernel, new OpenCvSharp.Point(-1, -1), 3);
            // 이미지를 가우시안 블러 처리 (커널 크기: 13x13, 표준편차: 3)
            Cv2.GaussianBlur(image, image, new OpenCvSharp.Size(13, 13), 3, 3, BorderTypes.Reflect101);
            // 이미지를 침식 연산 (3회 반복)
            Cv2.Erode(image, image, kernel, new OpenCvSharp.Point(-1, -1), 3);

            // 허프 변환을 통해 원 검출
            CircleSegment[] circles = Cv2.HoughCircles(image, HoughModes.Gradient, 1, 100, 100, 35, 0, 0);

            // 검출된 원을 빨간색으로 그리기
            for (int i = 0; i < circles.Length; i++)
            {
                OpenCvSharp.Point center = new OpenCvSharp.Point(circles[i].Center.X, circles[i].Center.Y);

                // 원의 외곽선 그리기
                Cv2.Circle(dst, center, (int)circles[i].Radius, Scalar.Red, 3);
                // 원의 중심점 그리기
                Cv2.Circle(dst, center, 5, Scalar.Red, Cv2.FILLED);
            }

            // 결과 이미지를 새로운 창에 표시
            Cv2.ImShow("HoughCircle", dst);
        }
    }
}
