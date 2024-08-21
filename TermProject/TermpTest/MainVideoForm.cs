using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
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

        const double x_MaxPos = 1200.0;
        const double y_MaxPos = 1200.0;
        Pen P_Black_2 = new Pen(Color.Black, 2);
        Graphics g;
        int Margin = 40;                                // pl_stage와 user stage의 간격

        public Rectangle client_stage;                         //user stage Rect
        public System.Drawing.Point client_home;        // user stage의 홈 좌표
        public System.Drawing.Point client_max;
        List<System.Drawing.Point> points = new List<System.Drawing.Point>();
        System.Drawing.Point point = new System.Drawing.Point();
        System.Drawing.Point tem = new System.Drawing.Point(0,0);

        bool start_flag = false;
        public MainVideoForm()
        {
            InitializeComponent();

            C_DEFINE = new Define();

            F_CONTOURFORM = new ContourForm();
            F_CONTOURFORM.Show();

            PB_CAMERA.Width = C_DEFINE._WIDTH;
            PB_CAMERA.Height = C_DEFINE._HEIGHT;
            //PB_CAMERA.Location = new System.Drawing.Point(0, 0);
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
            //Task.Run(() => ProcessCamera(cancelTokenSource.Token));
        }
        public void InitStage()
        {
            Graphics g = panel1.CreateGraphics();
            string Left_Top = "(" + x_MaxPos.ToString() + "," + (0).ToString() + ")";
            string Right_Top = "(" + (0).ToString() + "," + (0).ToString() + ")";
            string Left_Bottom = "(" + x_MaxPos.ToString() + "," + y_MaxPos.ToString() + ")";
            string Right_Bottom = "(" + (0).ToString() + "," + y_MaxPos.ToString() + ")";
            int gap = 15;

            g.FillRectangle(Brushes.White, panel1.ClientRectangle);
            g.DrawRectangle(P_Black_2, client_stage);
            g.DrawString(Right_Top, Font, Brushes.Black, client_home.X - gap, client_home.Y - gap);
            g.DrawString(Left_Top, Font, Brushes.Black, client_max.X - gap, Margin - gap);
            g.DrawString(Left_Bottom, Font, Brushes.Black, client_max.X - gap, client_max.Y + gap);
            g.DrawString(Right_Bottom, Font, Brushes.Black, client_home.X - gap, client_max.Y + gap);
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
                    // Console.WriteLine($"Distance Center Point: ({centerX:F2} , {centerY:F2} )");

                    //int x = Convert.ToInt32((1200 / client_stage.Width) * (client_home.X- (centerX * client_stage.Width / 640)));
                    //int y = Convert.ToInt32((1200 / client_stage.Height) * ((centerY * client_stage.Height / 480)-40));

                    //if (x >= 0 && x <= 1200 && y >= 0 && y <= 1200) Console.WriteLine($"Distance Center Point: ({x:F2} , {y:F2} )");

                    if(start_flag)
                    {
                        foreach (var p in contour)
                        {
                            point.X = Convert.ToInt32((1200 / client_stage.Width) * (client_home.X - (p.X * client_stage.Width / 640)));
                            point.Y = Convert.ToInt32((1200 / client_stage.Height) * ((p.Y * client_stage.Height / 480) - 40));
                            if (point.X >= 0 && point.X <= 1200 && point.Y >= 0 && point.Y <= 1200) points.Add(point);
                        }

                        System.Drawing.Point CurPos = new System.Drawing.Point();
                        foreach (var p in points)
                        {
                            CurPos.X = client_home.X - (int)((p.X * client_stage.Width) / 1200);
                            CurPos.Y = Margin + (int)((p.Y * client_stage.Height) / 1200);
                            g.DrawLine(P_Black_2, tem, CurPos);
                            tem = CurPos;

                        }
                    }
                    points.Clear();
                    start_flag = false;
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

        private void button1_Click(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics(); // pl_stage에 그림을 그리기 위한 Graphics 객체 생성
            client_stage = new Rectangle(Margin, Margin, panel1.Width - 2 * Margin, panel1.Height - 2 * Margin);
            client_home = new System.Drawing.Point(client_stage.Width + Margin, Margin); // 우상단
            client_max = new System.Drawing.Point(client_home.X - client_stage.Width, client_stage.Height + Margin);
            InitStage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() => ProcessCamera(cancelTokenSource.Token));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            start_flag = true;
        }
    }
}