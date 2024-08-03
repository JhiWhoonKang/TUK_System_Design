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
            //string imagePath = "C:\\JHIWHOON_ws\\Tech University of Korea\\LAB\\2023 시스템설계 교과목 개발\\채널분리 코드\\RGB Image.png";
            Bitmap img = Properties.Resources.RGB_Image;
            Mat src = BitmapConverter.ToMat(img);

            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            Mat[] channels = Cv2.Split(src);

            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);
            PB_CH_B.Image = BitmapConverter.ToBitmap(channels[0]);
            PB_CH_G.Image = BitmapConverter.ToBitmap(channels[1]);
            PB_CH_R.Image = BitmapConverter.ToBitmap(channels[2]);                        
        }
    }
}