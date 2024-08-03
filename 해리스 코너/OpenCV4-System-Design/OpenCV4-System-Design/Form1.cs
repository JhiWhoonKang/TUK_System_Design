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
            // string imagePath = "C:\\JHIWHOON_ws\\Tech University of Korea\\LAB\\2023 시스템설계 교과목 개발\\채널분리 코드\\RGB Image.png";
            Bitmap img = Properties.Resources.contourdraw;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            Mat gray = new Mat();
            Mat dst = src.Clone();

            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            Point2f[] corners = Cv2.GoodFeaturesToTrack(gray, 100, 0.03, 5, null, 3, false, 0);
            Point2f[] sub_corners = Cv2.CornerSubPix(gray, corners, new OpenCvSharp.Size(7, 7), new OpenCvSharp.Size(-1, -1), TermCriteria.Both(10, 0.03));

            for(int i=0;i<corners.Length;i++)
            {
                OpenCvSharp.Point pt=new OpenCvSharp.Point((int)corners[i].X, (int)corners[i].Y);
                Cv2.Circle(dst, pt, 5, Scalar.Yellow, Cv2.FILLED);
            }

            for(int i=0;i<sub_corners.Length;i++)
            {
                OpenCvSharp.Point pt=new OpenCvSharp.Point((int)sub_corners[i].X, (int)(sub_corners[i].Y));
                Cv2.Circle(dst, pt, 5, Scalar.Red, Cv2.FILLED );
            }

            Cv2.ImShow("dst", dst);            
        }
    }
}