using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OpenCvSharp;
using TwinCAT.TypeSystem;

namespace XYSTAGE_SYSTEM_DESIGN
{
    public partial class Form1 : Form
    {
        #region 그래프 변수 
        Graphics g;
        int Margin = 40;                                // pl_stage와 user stage의 간격

        public Rectangle client_stage;                         //user stage Rect
        public System.Drawing.Point client_home;        // user stage의 홈 좌표
        public System.Drawing.Point client_max;         // user stage의 최대 좌표

        // Mouse Event
        System.Drawing.Point Mouse_Point;
        List<System.Drawing.Point> stage_point_list = new List<System.Drawing.Point>(); // 스테이지 가시화용
        List<System.Drawing.Point> feed_pos_list = new List<System.Drawing.Point>();    // feedback 
     
        const double x_MaxPos = 1200.0;
        const double y_MaxPos = 1200.0;

        //속도,가속도,가감속도
        double x_vel;
        double x_acc;
        double x_dec;
        double y_vel;
        double y_acc;
        double y_dec;

        // 이동 횟수 디폴트 5번 
        int Move_num = 5;

        // Pen 객체
        Pen P_Black_2 = new Pen(Color.Black, 2);
        Pen P_BlueViolet_2 = new Pen(Color.Red, 1);
        Pen P_Black_3 = new Pen(Color.Black, 3);

        // Circle Move  
        System.Drawing.Point CircleCenter;
        int radius = 100;
        double angle = 0;
        double angleincrelment = 10;
        int y; int x;

        // PM Move PTP
        double anglePM = 0;
        List<System.Drawing.Point> PM_point_list = new List<System.Drawing.Point>();    // 실제 좌표 
        System.Drawing.Point first_pos;  // 실제 좌표 
        System.Drawing.Point fist_pos2Stage;

        public System.Drawing.Point CurPos; public System.Drawing.Point tem =new System.Drawing.Point(0,0); int draw_feedflag = 0; int draw_feedflag_first = 0;
        #endregion


        public static TcAdsClient Ads = new TcAdsClient();
        public static CTwincat XAxis;
        public static CTwincat YAxis;
        public static CCommand User_Tasks;
    
        public int flag = 0;
        public int thread_flag = 0;

        public bool xEstop_flag;
        public bool yEstop_flag;

        
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1300, 800);

            g = pl_stage.CreateGraphics(); // pl_stage에 그림을 그리기 위한 Graphics 객체 생성
            client_stage = new Rectangle(Margin, Margin, pl_stage.Width - 2 * Margin, pl_stage.Height - 2 * Margin);
            client_home = new System.Drawing.Point(client_stage.Width + Margin, Margin); // 우상단
            client_max = new System.Drawing.Point(client_home.X - client_stage.Width, client_stage.Height + Margin);

            InitStage();

            x_vel = Double.Parse(tb_xVel.Text);
            x_acc = Double.Parse(tb_xAcc.Text);
            x_dec = Double.Parse(tb_xDec.Text);
            y_vel = Double.Parse(tb_yVel.Text);
            y_vel = Double.Parse(tb_yAcc.Text);
            y_vel = Double.Parse(tb_yDec.Text);

            xEstop_flag = false;
            yEstop_flag = false;
        }

        public void InitStage()
        {
            Graphics g = pl_stage.CreateGraphics();
            string Left_Top = "(" + x_MaxPos.ToString() + "," + (0).ToString() + ")";
            string Right_Top = "(" + (0).ToString() + "," + (0).ToString() + ")";
            string Left_Bottom = "(" + x_MaxPos.ToString() + "," + y_MaxPos.ToString() + ")";
            string Right_Bottom = "(" + (0).ToString() + "," + y_MaxPos.ToString() + ")";
            int gap = 15;
          
            g.FillRectangle(Brushes.White, pl_stage.ClientRectangle);
            g.DrawRectangle(P_Black_2, client_stage);
            g.DrawString(Right_Top, Font, Brushes.Black, client_home.X - gap, client_home.Y - gap);
            g.DrawString(Left_Top, Font, Brushes.Black, client_max.X - gap, Margin - gap);
            g.DrawString(Left_Bottom, Font, Brushes.Black, client_max.X - gap, client_max.Y + gap);
            g.DrawString(Right_Bottom, Font, Brushes.Black, client_home.X - gap, client_max.Y + gap);
        }

        private void pl_stage_MouseDown(object sender, MouseEventArgs e)
        {
            CircleCenter = e.Location;
            Mouse_Point = pl_stage.PointToClient(new System.Drawing.Point(Control.MousePosition.X, Control.MousePosition.Y));

            /*****************************************
               user stage <=> panel stage [Mapping]
               x (user stage x) , X (Mouse_pt_x)
               x = (x_MaxPos / client_rect_width)(Home.X - X)  

               y (user stage y) , Y (Mouse_pt_y)
               y = (y_MaxPos / client_rect_height)(Y-Margin)
            *****************************************/

            int x = Convert.ToInt32((x_MaxPos / client_stage.Width) * (client_home.X - Mouse_Point.X));
            int y = Convert.ToInt32((y_MaxPos / client_stage.Height) * (Mouse_Point.Y - Margin));
            System.Drawing.Point user_point = new System.Drawing.Point(x, y);



            if (x >= 0 && x <= x_MaxPos && y >= 0 && y <= y_MaxPos)
            {
                if(draw_feedflag_first==0)
                {
                    tem = Mouse_Point;
                    draw_feedflag_first = 1;
                }
                
                if (rb_PTP.Checked)   // Line 모드 일때 
                {
                    //1. user 지정 좌표, stage 좌표 list에 저장
                    string Point_display = "[" + user_point.X.ToString() + "," + user_point.Y.ToString() + "]";
                    g.DrawString(Point_display, Font, Brushes.Blue, Mouse_Point.X - 23, Mouse_Point.Y + 12);
                    User_Tasks.RegistAxisPara(user_point.X,user_point.Y,x_vel,y_vel,x_acc, y_acc,x_dec,y_dec);

                    stage_point_list.Add(Mouse_Point);
                    //2. 스테이지 이동 예상 경로 표시
                    if (User_Tasks.Get_Count_AxisParalist() > 1)
                    {
                        g.DrawLine(Pens.Red, stage_point_list[stage_point_list.Count - 2], stage_point_list[stage_point_list.Count - 1]);
                    }
                    //3. user 지정 좌표 가시화 [x,y] 문자 표시
                   
                    //4. user 지정 좌표 가시화 ㅁ 초록색 사각형 표시
                    g.FillRectangle(Brushes.Green, new Rectangle(Mouse_Point.X - 3, Mouse_Point.Y - 3, 6, 6));

                    if ((User_Tasks.Get_Count_AxisParalist() == Move_num) && !rb_CP.Checked && !rb_PM.Checked) //5개 찍으면 동작
                    {         
                        Thread PTP_thread = new Thread(new ThreadStart(Line_Function));
                        PTP_thread.Start();      
                    }
                }


                else if (rb_CP.Checked)
                {
                    //3. user 지정 좌표 가시화 [x,y] 문자 표시
                    string Point_display = "[" + user_point.X.ToString() + "," + user_point.Y.ToString() + "]";
                    g.DrawString(Point_display, Font, Brushes.Blue, Mouse_Point.X - 23, Mouse_Point.Y + 12);
                    //4. user 지정 좌표 가시화 ㅁ 초록색 사각형 표시
                    g.FillRectangle(Brushes.Green, new Rectangle(Mouse_Point.X - 3, Mouse_Point.Y - 3, 6, 6));


                    x = CircleCenter.X - radius;
                    y = CircleCenter.Y - radius;
                    g.DrawEllipse(Pens.Blue, x, y, radius * 2, radius * 2);

                    Thread CP_thread = new Thread(new ThreadStart(CP_Function));
                    CP_thread.Start();
                }

                else if (rb_PM.Checked)
                {
                    tb_PM_num.Enabled = true;
                    tb_PM_radius.Enabled = true;
                    bt_PM_numSet.Enabled=true;
                    bt_PM_radiusSet.Enabled=true;
    
                    double radian = 0;
                    int posx;
                    int posy;
                    

                    anglePM = 360 / int.Parse(tb_PM_num.Text);
                    System.Drawing.Point first = new System.Drawing.Point((int)(CircleCenter.X + Math.Cos(radian) * int.Parse(tb_PM_radius.Text))
                        , (int)(CircleCenter.Y - Math.Sin(radian) * int.Parse(tb_PM_radius.Text)));

                    PM_point_list.Add(first);
                    User_Tasks.RegistAxisPara(Convert.ToInt32((x_MaxPos / client_stage.Width) * (client_home.X - first.X))
                          , Convert.ToInt32((y_MaxPos / client_stage.Height) * (first.Y - Margin)), x_vel, y_vel, x_acc, y_acc, x_dec, y_dec);

                    int angle = 0;
                    while (true)
                    {
                        angle += (int)anglePM;
                        radian = angle * Math.PI / 180;

                        posx = (int)(CircleCenter.X + Math.Cos(radian) * int.Parse(tb_PM_radius.Text));
                        posy = (int)(CircleCenter.Y - Math.Sin(radian) * int.Parse(tb_PM_radius.Text));
                        if (angle >= 360) break;
                        PM_point_list.Add(new System.Drawing.Point(posx, posy));
                        User_Tasks.RegistAxisPara(Convert.ToInt32((x_MaxPos / client_stage.Width) * (client_home.X - posx))
                            , Convert.ToInt32((y_MaxPos / client_stage.Height) * (posy - Margin)), x_vel, y_vel, x_acc, y_acc, x_dec, y_dec);               

                    }
                    User_Tasks.RegistAxisPara(User_Tasks.Get_IndexVlaue_XPos_AxisParalist(0), User_Tasks.Get_IndexVlaue_YPos_AxisParalist(0), x_vel, y_vel, x_acc, y_acc, x_dec, y_dec);
                    first_pos = PM_point_list[0];
                    fist_pos2Stage.X = User_Tasks.Get_IndexVlaue_XPos_AxisParalist(0);
                    fist_pos2Stage.Y = User_Tasks.Get_IndexVlaue_YPos_AxisParalist(0);
                    string Point_display = "[" + user_point.X.ToString() + "," + user_point.Y.ToString() + "]";
                    g.DrawString(Point_display, Font, Brushes.Blue, Mouse_Point.X - 23, Mouse_Point.Y + 12);
                    
                    g.FillRectangle(Brushes.Green, new Rectangle(Mouse_Point.X - 3, Mouse_Point.Y - 3, 6, 6));
                    while (PM_point_list.Count != 0)
                    {
                        System.Drawing.Point tmp = PM_point_list[0];
                        PM_point_list.RemoveAt(0);
                        if (PM_point_list.Any())
                            g.DrawLine(Pens.Blue, tmp, PM_point_list[0]);
                        else
                            g.DrawLine(Pens.Blue, tmp, first_pos);
                    }


                    Thread.Sleep(500);

                    Thread PM_thread = new Thread(new ThreadStart(Line_Function));
                    PM_thread.Start();

                    //tb_PM_num.Enabled = false;
                    //tb_PM_radius.Enabled = false;
                    //bt_PM_numSet.Enabled = false;
                    //bt_PM_radiusSet.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("스테이지를 벗어난 좌표입니다.", "위치 지정 오류",
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void bt_connect_Click(object sender, EventArgs e)
        {
            Ads.Connect("169.254.80.96.1.1", 851); //연결


            try
            {
                if (Ads.IsConnected)
                {
                    YAxis = new CTwincat(Ads, 1); // 1 은 y축
                    XAxis = new CTwincat(Ads, 0); // 0 은 x축
                    

                    XAxis.SetAxisHandler();
                    YAxis.SetAxisHandler();
                    User_Tasks = new CCommand(XAxis, YAxis);
                    User_Tasks.Set_DrawForm(this);

                    MessageBox.Show("Target과 연결되었습니다.", "통신 연결", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lb_connect_state.ForeColor = Color.Yellow;
                    lb_connect_state.BackColor = Color.SkyBlue;
                    lb_connect_state.Text = "Connected";
                    timer1.Start();
                    Display_PID_Gain();
                    InitStage();
                }

            
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message, "통신 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Ads.IsConnected)
            {
                XAxis.OffMoter();
                YAxis.OffMoter();
                Ads.Disconnect();
            }
        }

        private void Line_Function()                            // Line 모드
        {
            draw_feedflag = 1;

            int list_state_cnt = 0;

            User_Tasks.Move();

            Thread.Sleep(1000);
            User_Tasks.Clear_AxisParalist();
            stage_point_list.Clear();
            list_state_cnt = 0;
            InitStage();
            draw_feedflag = 0;
            draw_feedflag_first = 0;
        }

       


        private void PM_Function()                           // PM 다각형 모드
        {
            draw_feedflag = 1;
            User_Tasks.Move();

           
            XAxis.SetPos(fist_pos2Stage.X);
            YAxis.SetPos(fist_pos2Stage.X);
            XAxis.Excute();
            YAxis.Excute();


            Thread.Sleep(1000);
            User_Tasks.Clear_AxisParalist(); 
            PM_point_list.Clear();
            InitStage();
            draw_feedflag = 0;
            draw_feedflag_first = 0;
        }


        private void CP_Function()
        {
            double radian = 0;
            angle = 0;
            int posx;
            int posy;
            draw_feedflag = 1;
            while (radian <= Math.PI * 2)
            {
                angle += angleincrelment;
                radian = angle * Math.PI / 180;

                posx = (int)(CircleCenter.X + Math.Cos(radian) * radius);
                posy = (int)(CircleCenter.Y - Math.Sin(radian) * radius);

                int x = Convert.ToInt32((x_MaxPos / client_stage.Width) * (client_home.X - posx));
                int y = Convert.ToInt32((y_MaxPos / client_stage.Height) * (posy - Margin));

                if(x < 0 || x > x_MaxPos || y < 0 || y > y_MaxPos)
                {

                    User_Tasks.Clear_AxisParalist();
                    MessageBox.Show("스테이지를 벗어난 좌표입니다.", "위치 지정 오류",
             MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                User_Tasks.RegistAxisPara(x,y, x_vel, y_vel, x_acc, y_acc, x_dec, y_dec);
            }

            User_Tasks.Move();

            Thread.Sleep(1000);
            User_Tasks.Clear_AxisParalist();
            InitStage();
            draw_feedflag = 0;
            draw_feedflag_first = 0;
        }

        private void bt_xServoOn_Click(object sender, EventArgs e)
        {
         
            if (!XAxis.IsOn)
            {
                XAxis.OnMoter();
                bt_xServoOn.Text = "Servo Off";
            }
            else
            {
                XAxis.OffMoter();
                bt_xServoOn.Text = "Servo On";
            }

        }

        private void bt_xHome_Click(object sender, EventArgs e)
        {
            XAxis.SetPos(0);
            XAxis.SetVel(1000);
            XAxis.SetAcc(1000);
            XAxis.SetDec(1000);
            XAxis.Excute();
        }

        private void bt_yHome_Click(object sender, EventArgs e)
        {
            YAxis.SetPos(0);
            YAxis.SetVel(1000);
            YAxis.SetAcc(1000);
            YAxis.SetDec(1000);   
            YAxis.Excute();
        }

        private void Serach_Function()
        {
            for (int xpos = 0; xpos <= 100; xpos ++)
            {
                if (thread_flag == 1)
                {
                    XAxis.Excute_Disable();
                    YAxis.Excute_Disable();
                    XAxis.SetVel(0);
                    XAxis.SetAcc(0);
                    YAxis.SetVel(0);
                    YAxis.SetAcc(0);
                    break;
                }
                if (flag == 0)
                {
                    for (int ypos = 0; ypos <= 100; ypos++)
                    {
                        XAxis.SetPos(xpos);
                        XAxis.SetVel(100);
                        XAxis.SetAcc(100);

                        
                        YAxis.SetPos(ypos);
                        YAxis.SetVel(100);
                        YAxis.SetAcc(100);


                        XAxis.Excute();
                        YAxis.Excute();
                        if (ypos == 150)
                            flag = 1;
                        if (thread_flag == 1)
                        {
                            XAxis.Excute_Disable();
                            YAxis.Excute_Disable();
                            XAxis.SetVel(0);
                            XAxis.SetAcc(0);
                            YAxis.SetVel(0);
                            YAxis.SetAcc(0);
                            break;
                        }
                    }
                }
                else
                {
                    for (int ypos = 100; ypos >= 0; ypos--)
                    {
                        XAxis.SetPos(xpos);
                        XAxis.SetVel(100);
                        XAxis.SetAcc(100);
 

                        YAxis.SetPos(ypos);
                        YAxis.SetVel(100);
                        YAxis.SetAcc(100);


                        XAxis.Excute();
                        YAxis.Excute();
                        if (ypos == 0)
                            flag = 0;
                        if (thread_flag == 1)
                        {
                            XAxis.Excute_Disable();
                            YAxis.Excute_Disable();
                            XAxis.SetVel(0);
                            XAxis.SetAcc(0);
                            YAxis.SetVel(0);
                            YAxis.SetAcc(0);
                            break;
                        }
                    }
                }

            }

        }

        private void bt_Normal_Move_Click(object sender, EventArgs e)
        {


            XAxis.SetPos(int.Parse(tb_Normal_Move_XPos.Text));
            XAxis.SetVel(1000);
            XAxis.SetAcc(1000);
            XAxis.SetDec(1000);

            YAxis.SetPos(int.Parse(tb_Normal_Move_YPos.Text));
            YAxis.SetVel(1000);
            YAxis.SetAcc(1000);
            YAxis.SetDec(1000);

            XAxis.Excute();
            YAxis.Excute();
        }

        private void rb_CAM_CheckedChanged(object sender, EventArgs e)
        {
            MainVideoForm v = new MainVideoForm(this);
            v.Show();
        }

        private void bt_yServoOn_Click(object sender, EventArgs e)
        {
            if (!YAxis.IsOn)
            {
                YAxis.OnMoter();
                bt_yServoOn.Text = "Servo Off";
            }
            else
            {
                YAxis.OffMoter();
                bt_yServoOn.Text = "Servo On";
            }
        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
           
            thread_flag = 1;
      
        }

        private void bt_Graph_Click(object sender, EventArgs e)
        {
            InitStage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            tb_xFeedPos.Text = User_Tasks.X_Axis.GetCur_X_Position().ToString("n2");
            tb_yFeedPos.Text = User_Tasks.Y_Axis.GetCur_Y_Position().ToString("n2");
            tb_xFeedVel.Text = User_Tasks.X_Axis.GetCur_X_Velocity().ToString("n2");
            tb_yFeedVel.Text = User_Tasks.Y_Axis.GetCur_Y_Velocity().ToString("n2");
            if (draw_feedflag==1)
            {
                Draw_FeddLine();
            }
        }


        public void Display_PID_Gain()
        { 
            this.Invoke(
            (MethodInvoker)(() =>
            {
                tb_xPGain.Text = User_Tasks.X_Axis.GetX_P_GAIN();
                tb_xIGain.Text = User_Tasks.X_Axis.GetX_I_GAIN();
                tb_xDGain.Text = User_Tasks.X_Axis.GetX_D_GAIN();
            }));

            this.Invoke(
           (MethodInvoker)(() =>
           {
               tb_yPGain.Text = User_Tasks.Y_Axis.GetY_P_GAIN();
               tb_yIGain.Text = User_Tasks.Y_Axis.GetY_I_GAIN();
               tb_yDGain.Text = User_Tasks.Y_Axis.GetY_D_GAIN();
           }));

          

        }

        public void Draw_FeddLine()
        {
            
     
            CurPos.X = client_home.X - (int)((User_Tasks.X_Axis.GetCur_X_Position() * client_stage.Width) / x_MaxPos);
            CurPos.Y = Margin + (int)((User_Tasks.Y_Axis.GetCur_Y_Position() * client_stage.Height) / y_MaxPos);
            g.DrawLine(P_Black_3, tem, CurPos);
            tem = CurPos;

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            InitStage();
        }

        private void bt_xPosClr_Click(object sender, EventArgs e)
        {
            XAxis.Set_XPos_Clr();
        }

        private void bt_yPosClr_Click(object sender, EventArgs e)
        {
            XAxis.Set_YPos_Clr();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void bt_xEstop_Click(object sender, EventArgs e)
        {
            XAxis.X_STOP();
            
            bt_xEstop_Clr.ForeColor = Color.Red;
            
        }

        private void bt_yEstop_Click(object sender, EventArgs e)
        {
            YAxis.Y_STOP();
           
            bt_yEstop_Clr.ForeColor = Color.Red;
            
        }

        private void bt_xEstop_Clr_Click(object sender, EventArgs e)
        {
            bt_xEstop_Clr.ForeColor = Color.Black ;
             XAxis.X_STOP_Clr();
            User_Tasks.Clear_AxisParalist();
            draw_feedflag = 0;
        }

        private void bt_yEstop_Clr_Click(object sender, EventArgs e)
        {
                bt_yEstop_Clr.ForeColor = Color.Black;
                YAxis.Y_STOP_Clr();
            User_Tasks.Clear_AxisParalist();
            draw_feedflag = 0;
        }

   
    }
}
