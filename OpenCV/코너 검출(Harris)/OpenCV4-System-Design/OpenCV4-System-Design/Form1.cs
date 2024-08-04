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
            // Resources 폴더에서 'contourdraw' 이미지를 로드하여 Bitmap 객체로 변환
            Bitmap img = Properties.Resources.contourdraw;
            // Bitmap 이미지를 OpenCV의 Mat 객체로 변환
            Mat src = BitmapConverter.ToMat(img);

            // 이미지가 비어 있는지 확인
            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 필요한 Mat 객체 초기화
            Mat gray = new Mat();    // 그레이스케일 이미지
            Mat dst = src.Clone();   // 원본 이미지를 복제한 이미지

            // 원본 이미지를 그레이스케일로 변환
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // 코너 검출을 위해 goodFeaturesToTrack 함수 사용
            Point2f[] corners = Cv2.GoodFeaturesToTrack(gray, 100, 0.03, 5, null, 3, false, 0);
            // 검출된 코너를 서브픽셀 정확도로 조정
            Point2f[] sub_corners = Cv2.CornerSubPix(gray, corners, new OpenCvSharp.Size(7, 7), new OpenCvSharp.Size(-1, -1), TermCriteria.Both(10, 0.03));

            // 검출된 코너를 노란색 원으로 그리기
            for (int i = 0; i < corners.Length; i++)
            {
                OpenCvSharp.Point pt = new OpenCvSharp.Point((int)corners[i].X, (int)corners[i].Y);
                Cv2.Circle(dst, pt, 5, Scalar.Yellow, Cv2.FILLED);
            }

            // 서브픽셀 정확도로 조정된 코너를 빨간색 원으로 그리기
            for (int i = 0; i < sub_corners.Length; i++)
            {
                OpenCvSharp.Point pt = new OpenCvSharp.Point((int)sub_corners[i].X, (int)(sub_corners[i].Y));
                Cv2.Circle(dst, pt, 5, Scalar.Red, Cv2.FILLED);
            }

            // 결과 이미지를 새로운 창에 표시
            Cv2.ImShow("Harris", dst);
        }
    }
}
