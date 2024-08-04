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
            Mat binary = new Mat();  // 이진화된 이미지
            Mat morp = new Mat();    // 모폴로지 연산 결과 이미지
            Mat image = new Mat();   // 비트 반전 이미지
            Mat dst = src.Clone();   // 원본 이미지를 복제한 이미지

            // 모폴로지 연산을 위한 커널 생성
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            // 윤곽선을 저장할 배열과 계층 정보 배열 선언
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;

            // 원본 이미지를 그레이스케일로 변환
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            // 그레이스케일 이미지를 이진화
            Cv2.Threshold(gray, binary, 230, 255, ThresholdTypes.Binary);
            // 이진화된 이미지에 모폴로지 닫기 연산 적용
            Cv2.MorphologyEx(binary, morp, MorphTypes.Close, kernel, new OpenCvSharp.Point(-1, -1), 2);
            // 모폴로지 연산 결과 이미지를 비트 반전
            Cv2.BitwiseNot(morp, image);
            // 비트 반전 이미지에서 윤곽선 찾기
            Cv2.FindContours(image, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89KCOS);
            // 윤곽선을 원본 이미지에 그리기
            Cv2.DrawContours(dst, contours, -1, new Scalar(255, 0, 0), 2, LineTypes.AntiAlias, hierarchy, 3);

            // 윤곽선의 모든 점에 원을 그리기
            for (int i = 0; i < contours.Length; i++)
            {
                for (int j = 0; j < contours[i].Length; j++)
                {
                    Cv2.Circle(dst, contours[i][j], 1, new Scalar(0, 0, 255), 3);
                }
            }

            // 결과 이미지를 새로운 창에 표시
            Cv2.ImShow("Contour", dst);
        }
    }
}