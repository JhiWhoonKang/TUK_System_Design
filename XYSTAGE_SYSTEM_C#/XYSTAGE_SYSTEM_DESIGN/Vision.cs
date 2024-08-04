using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
namespace XYSTAGE_SYSTEM_DESIGN
{
    public partial class Vision : Form
    {
        CvCapture capture;
        IplImage frame;
        IplImage houcircle;
        IplImage gray;
        IplImage templit_image;
        IplImage match;
        IplImage templit;
        int cnt = 0;

        CTwincat XAxis;
        CTwincat YAxis;
        Form1 form1;

        CvSeq<CvCircleSegment> circles;

        int flag = 0;
        public Vision()
        {
            InitializeComponent();
        }

        public Vision(Form1 form1)
        {
            InitializeComponent();
            this.form1=form1;
            cnt = 0;
            flag = 0;
         
        }


        private void Vision_Load(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("ss");
       //         templit_image = new IplImage("../../../templit.png");
                capture = CvCapture.FromCamera(CaptureDevice.DShow, 1);
              //  capture.SetCaptureProperty(CaptureProperty.FrameWidth, 605);
                //capture.SetCaptureProperty(CaptureProperty.FrameHeight,  411);
                timer1.Enabled = true;
            }
            catch { }
         
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frame = capture.QueryFrame();
            if(flag==0)
            {
                pictureBoxIpl1.ImageIpl = HoughCircles(frame);
            }
            
            else 
                pictureBoxIpl1.ImageIpl = frame;
        }


        public IplImage HoughCircles(IplImage src)
        {
            houcircle = new IplImage(src.Size, BitDepth.U8, 3);
            Cv.Copy(src, houcircle);
            gray = this.GrayScale(src);
            Cv.Smooth(gray, gray, SmoothType.Gaussian, 9);

            CvMemStorage Storage = new CvMemStorage();
            circles = Cv.HoughCircles(gray, Storage, HoughCirclesMethod.Gradient, 1, 100, 150, 50, 0, 0);

            foreach (CvCircleSegment item in circles)
            {
                Cv.DrawCircle(houcircle, item.Center, (int)item.Radius, CvColor.Blue, 3);

               
                if(cnt>=10)
                {
                    Form1.XAxis.Excute_Disable();
                    Form1.YAxis.Excute_Disable();
                    form1.thread_flag = 1;

                        flag = 1;

                    
                    break;
                }

               cnt++;
            }
            CvFont font = new CvFont(FontFace.HersheyPlain, 1, 1);
            Cv.PutText(houcircle, $"Circle Count: {cnt}", new CvPoint(10, 30), font, CvColor.Red);
            return houcircle;
        }

        public IplImage GrayScale(IplImage src)
        {
            gray = new IplImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.BgrToGray);
            return gray;
        }

        public IplImage TemplitImage(IplImage src, IplImage temp)
        {
            match = src;
            templit = temp;
            IplImage calc = new IplImage(new CvSize(match.Size.Width - templit.Size.Width + 1, match.Size.Height - templit.Size.Height + 1), BitDepth.F32, 1);

            Cv.MatchTemplate(match, templit, calc, MatchTemplateMethod.SqDiffNormed);


            CvPoint minloc, maxloc;
            Double minval, maxval;
            // Cv.MinMaxLoc(calc, out minloc, out maxloc);
            Cv.MinMaxLoc(calc, out minval, out maxval, out minloc, out maxloc);
           // CvPoint minloc, maxloc;
           // Cv.MinMaxLoc(calc, out minloc, out maxloc);

            Cv.DrawRect(match, new CvRect(minloc.X, minloc.Y, templit.Width, templit.Height), CvColor.Red, 3);
            Cv.PutText(frame, $"maxVal: {maxval}", new CvPoint(10, 20), new CvFont(FontFace.HersheySimplex, 0.5, 1), CvColor.Red);
            return match;
        }

        private void bt_Move_Click(object sender, EventArgs e)
        {

            for(int xpos=0;xpos<=150;xpos++)
            {
                if (flag == 0)
                {
                    for (int ypos = 0; ypos <= 150; ypos++)
                    {
          
                        Form1.XAxis.SetPos(xpos);
                       Form1. XAxis.SetVel(100);
                       Form1. XAxis.SetAcc(100);
                        Form1.XAxis.SetDec(100);

                        Form1.YAxis.SetPos(ypos);
                        Form1.YAxis.SetVel(100);
                        Form1.YAxis.SetAcc(100);


                        Form1.XAxis.Excute();
                        Form1.YAxis.Excute();
                        if (ypos == 150)
                            flag = 1;
                    }
                }
                else
                {
                    for (int ypos = 149; ypos >=0; ypos--)
                    {
                        Form1.XAxis.SetPos(xpos);
                        Form1.XAxis.SetVel(100);
                        Form1.XAxis.SetAcc(100);
                        Form1.XAxis.SetAcc(100);

                        Form1.YAxis.SetPos(ypos);
                        Form1.YAxis.SetVel(100);
                        Form1.YAxis.SetAcc(100);


                        Form1.XAxis.Excute();
                        Form1.YAxis.Excute();
                        if (ypos == 0)
                            flag = 0;
                    }
                }
            }


        }
    }
}
