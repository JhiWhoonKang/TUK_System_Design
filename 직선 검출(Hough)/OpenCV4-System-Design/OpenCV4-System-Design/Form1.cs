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
            // Resources 폴더에서 'houghline' 이미지를 로드하여 Bitmap 객체로 변환
            Bitmap img = Properties.Resources.houghline;
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
            Mat binary = new Mat();  // 이진화된 이미지
            Mat morp = new Mat();    // 모폴로지 연산 후 이미지
            Mat canny = new Mat();   // 캐니 에지 검출 후 이미지
            Mat dst = src.Clone();   // 원본 이미지를 복제한 이미지

            // 구조 요소 커널 생성 (모폴로지 연산에 사용)
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));

            // 원본 이미지를 그레이스케일로 변환
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            // 그레이스케일 이미지를 이진화 (임계값: 150, 최대값: 255)
            Cv2.Threshold(gray, binary, 150, 255, ThresholdTypes.Binary);
            // 이진화된 이미지를 팽창 연산
            Cv2.Dilate(binary, morp, kernel, new OpenCvSharp.Point(-1, -1));
            // 팽창된 이미지를 침식 연산 (3회 반복)
            Cv2.Erode(morp, morp, kernel, new OpenCvSharp.Point(-1, -1), 3);
            // 침식된 이미지를 다시 팽창 연산 (2회 반복)
            Cv2.Dilate(morp, morp, kernel, new OpenCvSharp.Point(1, 1), 2);
            // 모폴로지 연산 결과 이미지에 대해 캐니 에지 검출 수행
            Cv2.Canny(morp, canny, 0, 0, 3);

            // 허프 변환을 통해 선 검출
            LineSegmentPoint[] lines = Cv2.HoughLinesP(canny, 1, Cv2.PI / 180, 500, 50, 10);

            // 검출된 선을 노란색으로 그리기
            for (int i = 0; i < lines.Length; i++)
            {
                Cv2.Line(dst, lines[i].P1, lines[i].P2, Scalar.Yellow, 2);
            }

            // 결과 이미지를 새로운 창에 표시
            Cv2.ImShow("Hough", dst);
        }
    }
}
