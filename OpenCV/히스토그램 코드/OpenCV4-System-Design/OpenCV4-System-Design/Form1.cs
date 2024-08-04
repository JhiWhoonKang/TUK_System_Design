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
            Bitmap img = Properties.Resources.hist;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 원본 이미지를 PictureBox에 표시
            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);

            // 그레이스케일로 변환
            Mat gray = new Mat();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // 히스토그램 계산
            Mat hist = new Mat();
            int[] histSize = { 256 };
            Rangef[] ranges = { new Rangef(0, 256) };
            Cv2.CalcHist(new Mat[] { gray }, new int[] { 0 }, null, hist, 1, histSize, ranges);

            // 히스토그램을 시각적으로 표시하기 위한 이미지 생성
            int histW = 512;
            int histH = 400;
            int binW = (int)((double)histW / histSize[0]);
            Mat histImage = new Mat(histH, histW, MatType.CV_8UC3, Scalar.All(255));

            // 히스토그램 정규화
            Cv2.Normalize(hist, hist, 0, histImage.Rows, NormTypes.MinMax);

            // 히스토그램을 그리기
            for (int i = 1; i < histSize[0]; i++)
            {
                Cv2.Line(histImage,
                    new OpenCvSharp.Point(binW * (i - 1), histH - Math.Round(hist.At<float>(i - 1))),
                    new OpenCvSharp.Point(binW * i, histH - Math.Round(hist.At<float>(i))),
                    new Scalar(0, 0, 0), 2);
            }

            // 히스토그램 이미지를 PictureBox에 표시
            PB_HIST.Image = BitmapConverter.ToBitmap(histImage);
        }
    }
}