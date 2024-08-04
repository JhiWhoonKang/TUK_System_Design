using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace XYSTAGE_SYSTEM_DESIGN
{
    public class CCommand
    {
        public CTwincat X_Axis;
        public CTwincat Y_Axis;
        public Form1 form1;
        List<CAxisParameter> Axis_Para_list;
        

        public CCommand(CTwincat x, CTwincat y)             //생성자
        {
            X_Axis = x;
            Y_Axis = y;
            Axis_Para_list=new List<CAxisParameter>();
        }

        /*********************작업 등록 *********************/
        public void RegistAxisPara(int x, int y)
        {
            Axis_Para_list.Add(new CAxisParameter(x, y));
        }
        public void RegistAxisPara(int x, int y, double x_vel, double y_vel)
        {
            Axis_Para_list.Add(new CAxisParameter(x, y, x_vel, y_vel));
        }
        public void RegistAxisPara(int x, int y , double x_vel, double y_vel, double x_acc, double y_acc )
        {
            Axis_Para_list.Add(new CAxisParameter(x, y, x_vel, y_vel, x_acc, y_acc));
        }
        public void RegistAxisPara(int x, int y, double x_vel, double y_vel, double x_acc, double y_acc,double x_dec,double y_dec)
        {
            Axis_Para_list.Add(new CAxisParameter(x, y, x_vel, y_vel, x_acc, y_acc,x_dec,y_dec));
        }
        /****************************************************/


        public int Get_Count_AxisParalist()         // 작업 수 리턴
        {
            return Axis_Para_list.Count;
        }

        public int Get_IndexVlaue_XPos_AxisParalist(int index)
        {
            return Axis_Para_list[index].x_pos;
        }

        public int Get_IndexVlaue_YPos_AxisParalist(int index)
        {
            return Axis_Para_list[index].y_pos;
        }

        public void Clear_AxisParalist()
        {
            Axis_Para_list.Clear(); 
        }

        public void Set_DrawForm(Form1 form)
        {
            form1 = form;
        }

        public void Move()                      
        {
            while (Axis_Para_list.Count != 0)
            {
                X_Axis.SetPos(Axis_Para_list[0].x_pos);
                X_Axis.SetVel(Axis_Para_list[0].x_vel);
                X_Axis.SetAcc(Axis_Para_list[0].x_acc);
                X_Axis.SetDec(Axis_Para_list[0].x_dec);

                Y_Axis.SetPos(Axis_Para_list[0].y_pos);
                Y_Axis.SetVel(Axis_Para_list[0].y_vel);
                Y_Axis.SetAcc(Axis_Para_list[0].y_acc);
                Y_Axis.SetDec(Axis_Para_list[0].y_dec);

                X_Axis.Excute();
                Y_Axis.Excute();

                while (true)   // 움직일동안 대기 
                {
                    if (Convert.ToBoolean(X_Axis.ADS.ReadAny(X_Axis.hBusy, typeof(bool))) == true && Convert.ToBoolean(Y_Axis.ADS.ReadAny(Y_Axis.hBusy, typeof(bool))) == true) break;

                }

                while (true)   // 움직일동안 대기 
                {
                    if (Convert.ToBoolean(X_Axis.ADS.ReadAny(X_Axis.hBusy, typeof(bool))) == false && Convert.ToBoolean(Y_Axis.ADS.ReadAny(Y_Axis.hBusy, typeof(bool))) == false) break;

                }
               
                X_Axis.Excute_Disable();
                Y_Axis.Excute_Disable();


                Axis_Para_list.RemoveAt(0);
            }
        }
    }
}
