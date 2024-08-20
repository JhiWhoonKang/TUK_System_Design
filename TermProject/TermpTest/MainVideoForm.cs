using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace TermpTest
{
    public partial class MainVideoForm : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private Bitmap image;
        private bool isCameraRunning;
        private CancellationTokenSource cancelTokenSource;
        private MainVideoForm F_MAINVIDEOFORM;
        private ContourForm F_CONTOURFORM;
        private Define C_DEFINE;        

        public MainVideoForm()
        {
            InitializeComponent();

            C_DEFINE = new Define();

            F_CONTOURFORM = new ContourForm();
            F_CONTOURFORM.Show();

            PB_CAMERA.Width = C_DEFINE._WIDTH;
            PB_CAMERA.Height = C_DEFINE._HEIGHT;
            PB_CAMERA.Location = new System.Drawing.Point(0, 0);
        }

        private void MainVideoForm_Load(object sender, EventArgs e)
        {           
            // ################ 카메라
            capture = new VideoCapture(1);
            frame = new Mat();
            image = new Bitmap(PB_CAMERA.Width, PB_CAMERA.Height);

            if (!capture.IsOpened())
            {
                MessageBox.Show("카메라를 열 수 없습니다.");
                return;
            }

            isCameraRunning = true;
            cancelTokenSource = new CancellationTokenSource();
            Task.Run(() => ProcessCamera(cancelTokenSource.Token));
        }

        private async Task ProcessCamera(CancellationToken token)
        {
            while (isCameraRunning && !token.IsCancellationRequested)
            {
                capture.Read(frame);

                if (!frame.Empty())
                {
                    image = BitmapConverter.ToBitmap(frame);
                    PB_CAMERA.Invoke((Action)(() => { PB_CAMERA.Image = image; }));

                    //await Task.Run(() => ProcessFrame(frame));

                    Mat staticImage = frame.Clone();
                    await Task.Run(() => ProcessFrame(staticImage));
                }
                Thread.Sleep(30);
            }
        }

        private void ProcessFrame(Mat frame)
        {
            Mat hsv = new Mat();
            Mat mask1 = new Mat();
            Mat mask2 = new Mat();
            Mat mask = new Mat();
            Mat contoursImage = new Mat(frame.Size(), MatType.CV_8UC3, new Scalar(0, 0, 0));

            Cv2.CvtColor(frame, hsv, ColorConversionCodes.BGR2HSV);

            Cv2.InRange(hsv, C_DEFINE.lowerRed1, C_DEFINE.upperRed1, mask1);
            Cv2.InRange(hsv, C_DEFINE.lowerRed2, C_DEFINE.upperRed2, mask2);
            Cv2.BitwiseOr(mask1, mask2, mask);

            // 윤곽선 검출
            Cv2.FindContours(mask, out OpenCvSharp.Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxNone);

            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area > 1000)
                {
                    Cv2.DrawContours(contoursImage, new[] { contour }, -1, new Scalar(0, 255, 0), 2);

                    Moments moments = Cv2.Moments(contour);
                    double centerX = moments.M10 / moments.M00;
                    double centerY = moments.M01 / moments.M00;

                    Cv2.Circle(contoursImage, new OpenCvSharp.Point((int)centerX, (int)centerY), 5, new Scalar(255, 0, 0), -1);
                }
            }
            F_CONTOURFORM.Invoke((Action)(() => { F_CONTOURFORM.UpdateImage(BitmapConverter.ToBitmap(contoursImage)); }));

        }

        private void MainVideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isCameraRunning = false;
            cancelTokenSource.Cancel();
            capture.Release();
        }
    }
}