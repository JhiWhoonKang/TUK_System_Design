using System;
using System.Drawing;
using System.IO;
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
            Bitmap img = Properties.Resources.canny;
            Mat src = BitmapConverter.ToMat(img);
            Mat dst = new Mat();

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            Cv2.Sobel(src, dst, MatType.CV_8UC1, 1, 0, 3, 1, 0, BorderTypes.Reflect101);

            Mat rotatedDst = new Mat();
            Cv2.Rotate(dst, rotatedDst, RotateFlags.Rotate90Clockwise);

            Mat resizedDst = new Mat();
            Cv2.Resize(rotatedDst, resizedDst, new OpenCvSharp.Size(rotatedDst.Width / 2, rotatedDst.Height / 2));

            Cv2.ImShow("dst", resizedDst);
        }
    }
}