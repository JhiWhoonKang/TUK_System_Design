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
            Bitmap img = Properties.Resources.circle_hough;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }
            Mat image= new Mat();
            Mat dst = src.Clone();

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));

            Cv2.CvtColor(src, image, ColorConversionCodes.BGR2GRAY);
            Cv2.Dilate(image, image, kernel, new OpenCvSharp.Point(-1, -1), 3);
            Cv2.GaussianBlur(image, image, new OpenCvSharp.Size(13, 13), 3, 3, BorderTypes.Reflect101);
            Cv2.Erode(image, image, kernel, new OpenCvSharp.Point(-1, -1), -3);

            CircleSegment[] circles = Cv2.HoughCircles(image, HoughModes.Gradient, 1, 100, 100, 35, 0, 0);

            for(int i=0;i<circles.Length;i++)
            {
                OpenCvSharp.Point center=new OpenCvSharp.Point(circles[i].Center.X, circles[i].Center.Y);

                Cv2.Circle(dst, center, (int)circles[i].Radius, Scalar.Red, 3);
                Cv2.Circle(dst, center, 5, Scalar.Red, Cv2.FILLED);
            }

            Cv2.ImShow("dst", dst);
        }
    }
}