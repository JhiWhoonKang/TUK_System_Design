using System;
using System.Drawing;
using System.Drawing.Configuration;
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
            Bitmap img = Properties.Resources.houghline;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            Mat gray = new Mat();
            Mat binary = new Mat();
            Mat morp = new Mat();
            Mat canny = new Mat();
            Mat dst = src.Clone();

            Mat kernel=Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(gray, binary, 150, 255, ThresholdTypes.Binary);
            Cv2.Dilate(binary, morp, kernel, new OpenCvSharp.Point(-1, -1));
            Cv2.Erode(morp, morp, kernel, new OpenCvSharp.Point(-1, -1), 3);
            Cv2.Dilate(morp, morp, kernel, new OpenCvSharp.Point(1, 1), 2);
            Cv2.Canny(morp, canny, 0, 0, 3);

            LineSegmentPoint[] lines = Cv2.HoughLinesP(canny, 1, Cv2.PI / 180 , 500, 50, 10);

            for(int i=0;i<lines.Length;i++)
            {
                Cv2.Line(dst, lines[i].P1, lines[i].P2, Scalar.Yellow, 2);
            }

            Cv2.ImShow("dst", dst);            
        }
    }
}