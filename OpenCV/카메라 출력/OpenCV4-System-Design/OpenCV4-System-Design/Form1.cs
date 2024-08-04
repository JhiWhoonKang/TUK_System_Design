using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace OpenCV4_System_Design
{
    public partial class Form1 : Form
    {
        private VideoCapture capture; //카메라 캡처 객체
        private Mat frame; //캡처된 프레임을 저장할 Mat 객체
        private Bitmap image; //picturebox에 표시할 이미지
        private bool isCameraRunning = false; //카메라 동작 플래그
        private CancellationTokenSource cancelTokenSource; //비동기 작업을 취소하기 위한 토큰 소스

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // ################ 카메라
            capture = new VideoCapture(1);
            frame = new Mat(); 
            image = new Bitmap(PB_CAMERA.Width, PB_CAMERA.Height); //PictureBox 크기에 맞는 빈 비트맵 생성

            if (!capture.IsOpened())
            {
                MessageBox.Show("카메라를 열 수 없습니다.");
                return;
            }

            isCameraRunning = true; //카메라 작동 상태를 true로 설정
            cancelTokenSource = new CancellationTokenSource(); //취소 토큰 소스 초기화
            Task.Run(() => CaptureCamera(cancelTokenSource.Token)); //비동기적으로 카메라 영상을 캡처하는 작업 시작
                                                                    // ################ 

            // ################ 이미지
            Bitmap img = Properties.Resources.TEST_IMG;
            PB_IMAGE.Image = img; // Properties.Resources.TEST_IMG를 PB_IMAGE에 출력
            // ################ 
        }

        /* 카메라 영상을 캡처하는 메서드 */
        private void CaptureCamera(CancellationToken token)
        {
            while (isCameraRunning && !token.IsCancellationRequested)
            {                
                capture.Read(frame); //프레임을 읽어서 frame 객체에 저장

                if (!frame.Empty()) //프레임이 비어 있지 않은 경우
                {
                    image = BitmapConverter.ToBitmap(frame); //프레임을 비트맵으로 변환
                    PB_CAMERA.Invoke((Action)(() => { PB_CAMERA.Image = image; })); //UI 스레드에서 PictureBox에 이미지 설정
                }

                Thread.Sleep(30); //30ms 대기 (약 33fps)
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isCameraRunning = false; //카메라 작동 상태를 false로 설정
            cancelTokenSource.Cancel(); //비동기 작업을 취소

            if (capture != null && capture.IsOpened())
            {
                capture.Release();
                capture.Dispose();
            } //카메라 객체가 열려 있는 경우 리소스를 해제

            if (frame != null)
            {
                frame.Dispose();
            } //frame 객체가 null이 아닌 경우 리소스를 해제
        }
    }
}