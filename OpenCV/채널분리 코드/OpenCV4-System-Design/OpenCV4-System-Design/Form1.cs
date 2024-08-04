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
            // Properties.Resources에서 RGB 이미지를 로드하여 Bitmap 객체로 저장
            Bitmap img = Properties.Resources.RGB_Image;

            // Bitmap 이미지를 OpenCV의 Mat 객체로 변환합니다.
            Mat src = BitmapConverter.ToMat(img);

            // 이미지 로드가 실패한 경우 메시지 박스를 표시하고 함수 실행을 종료
            if (src.Empty())
            {
                MessageBox.Show("Image load error");
                return;
            }

            // 이미지를 RGB 채널로 분할
            Mat[] channels = Cv2.Split(src);

            // 원본 이미지를 PictureBox에 표시
            PB_ORIGINAL.Image = BitmapConverter.ToBitmap(src);

            // 분할된 각 채널 이미지를 각 PictureBox에 표시
            PB_CH_B.Image = BitmapConverter.ToBitmap(channels[0]);
            PB_CH_G.Image = BitmapConverter.ToBitmap(channels[1]);
            PB_CH_R.Image = BitmapConverter.ToBitmap(channels[2]);
        }
    }
}