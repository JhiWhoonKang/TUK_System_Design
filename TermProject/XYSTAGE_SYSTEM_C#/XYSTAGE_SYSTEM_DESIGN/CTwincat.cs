using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwinCAT.Ads;


namespace XYSTAGE_SYSTEM_DESIGN
{
    public class CTwincat
    {
        public TcAdsClient ADS;                 // ADS 통신 인터페이스 변수 
        public int AxisType;                    // 0은 X축, 1은 Y축
        public bool IsConnect = false;
        public bool IsOn = false;

        public CTwincat(TcAdsClient ads, int type)      // 생성자
        {
            ADS = ads;
            AxisType = type;
        }

        #region Twincat Handle Variable

        public int hOnMoter;      // Servo On 핸들 변수
        public int hAbMove_Ex;    // Absolute Move Fuction Block Execute  핸들 변수
        
        /*********지령 Pos, Vel, Acc, Dec 핸들 변수***********/
        public int hCommand_Vel;
        public int hCommand_Acc;
        public int hCommand_Dec;
        public int hCommand_Pos;
        /*****************************************************/

        public int hBusy;         // 현재 이동중 ? 
        public int hDone;         // 이동 종료 ?

        /*********현재 Position, Velocity 핸들 변수***********/
        public int hX_Pos;                      
        public int hX_Vel;

        public int hY_Pos;
        public int hY_Vel;
        /***********************************************/

        /*********X축, Y축 PID 게인 값 핸들 변수***********/
        public static int hX_GetP_Gain;
        public static int hX_GetI_Gain;
        public static int hX_GetD_Gain;
        public static int hX_GetPID_Ex;

        public static int hY_GetP_Gain;
        public static int hY_GetI_Gain;
        public static int hY_GetD_Gain;
        public static int hY_GetPID_Ex;
        /***********************************************/

        /*********Pos Clear(원점) 하기 위한 핸들 변수***********/
        public static int hX_Pos_Offset_Ex;
        public static int hY_Pos_Offset_Ex;
        /***********************************************/

        public static int hX_Stop_EX;
        public static int hY_Stop_EX;
        #endregion

        #region Connect TwinCat3 to C# Handle Variable
        public void SetAxisHandler()
        {        
            if (AxisType == 0) //X
            {
                hOnMoter = ADS.CreateVariableHandle("GVL.OnMoterX");
                hCommand_Vel = ADS.CreateVariableHandle("GVL.X_Command_Vel");
                hCommand_Acc = ADS.CreateVariableHandle("GVL.X_Command_Acc");
                hCommand_Dec = ADS.CreateVariableHandle("GVL.X_Command_Dec");
                hCommand_Pos = ADS.CreateVariableHandle("GVL.X_Command_Pos");
                hBusy = ADS.CreateVariableHandle("GVL.X_Busy");
                hAbMove_Ex = ADS.CreateVariableHandle("GVL.X_AbMove_Ex");
                hX_Vel = ADS.CreateVariableHandle("GVL.X_Vel");
                hX_Pos = ADS.CreateVariableHandle("GVL.X_Pos");


                hX_GetP_Gain = ADS.CreateVariableHandle("GVL.X_GetP_Gain");
                hX_GetI_Gain = ADS.CreateVariableHandle("GVL.X_GetI_Gain");
                hX_GetD_Gain = ADS.CreateVariableHandle("GVL.X_GetD_Gain");
                hX_GetPID_Ex = ADS.CreateVariableHandle("GVL.X_CoERead_EX");

                hX_Pos_Offset_Ex = ADS.CreateVariableHandle("GVL.X_Pos_Offset_EX");
                hX_Stop_EX = ADS.CreateVariableHandle("GVL.X_Stop_EX");
                IsConnect = true;
            }
            else if (AxisType == 1) //Y
            {
                hOnMoter = ADS.CreateVariableHandle("GVL.OnMoterY");
                hCommand_Vel = ADS.CreateVariableHandle("GVL.Y_Command_Vel");
                hCommand_Acc = ADS.CreateVariableHandle("GVL.Y_Command_Acc");
                hCommand_Dec = ADS.CreateVariableHandle("GVL.Y_Command_Dec");
                hCommand_Pos = ADS.CreateVariableHandle("GVL.Y_Command_Pos");
                hBusy = ADS.CreateVariableHandle("GVL.Y_Busy");
                hAbMove_Ex = ADS.CreateVariableHandle("GVL.Y_AbMove_Ex");
                hY_Vel = ADS.CreateVariableHandle("GVL.Y_Vel");
                hY_Pos = ADS.CreateVariableHandle("GVL.Y_Pos");


                hY_GetP_Gain = ADS.CreateVariableHandle("GVL.Y_GetP_Gain");
                hY_GetI_Gain = ADS.CreateVariableHandle("GVL.Y_GetI_Gain");
                hY_GetD_Gain = ADS.CreateVariableHandle("GVL.Y_GetD_Gain");
                hY_GetPID_Ex = ADS.CreateVariableHandle("GVL.Y_CoERead_EX");

                hY_Pos_Offset_Ex = ADS.CreateVariableHandle("GVL.Y_Pos_Offset_EX");
                hY_Stop_EX = ADS.CreateVariableHandle("GVL.Y_Stop_EX");
                IsConnect = true;
            }
        }
        #endregion

        #region Motor_Power
        public void OnMoter()
        {
            if (IsConnect)
            {
                ADS.WriteAny(hOnMoter, true);
                IsOn = true;
            }

        }
        public void OffMoter()
        {
            if (IsConnect)
            {
                ADS.WriteAny(hOnMoter, false);
                IsOn = false;
            }

        }
        #endregion

        #region Set Position, Velocity , Acceleration , Deceleration
        public void SetPos(double pos)
        {
            if (IsConnect)
            {
                ADS.WriteAny(hCommand_Pos, Convert.ToDouble(pos));
            }
        }
        public void SetVel(double vel)
        {
            if (IsConnect)
            {
                ADS.WriteAny(hCommand_Vel, Convert.ToDouble(vel));
            }
        }
        public void SetAcc(double Acc)
        {
            if (IsConnect)
            {
                ADS.WriteAny(hCommand_Acc, Convert.ToDouble(Acc));
            }
        }
        public void SetDec(double Dec)
        {
            if (IsConnect)
            {
                ADS.WriteAny(hCommand_Dec, Convert.ToDouble(Dec));
            }
        }
        #endregion

        #region Get Current Position , Velocity
        public double GetCur_X_Position()
        {
            return Convert.ToDouble(ADS.ReadAny(hX_Pos, typeof(double)));
        }

        public double GetCur_Y_Position()
        {

            return Convert.ToDouble(ADS.ReadAny(hY_Pos, typeof(double)));
        }
        public double GetCur_X_Velocity()
        {
            return Convert.ToDouble(ADS.ReadAny(hX_Vel, typeof(double)));
        }

        public double GetCur_Y_Velocity()
        {

            return Convert.ToDouble(ADS.ReadAny(hY_Vel, typeof(double)));
        }
        #endregion

        #region Move Excute Flag (On, Off)
        public void Excute()
        {
            ADS.WriteAny(hAbMove_Ex, true);
        }

        public void Excute_Disable()
        {
            ADS.WriteAny(hAbMove_Ex, false);
        }
        #endregion

        #region ESTOP
        public void X_STOP()
        {
            ADS.WriteAny(hX_Stop_EX, true);
        }
        public void X_STOP_Clr()
        {

            if (Convert.ToBoolean(ADS.ReadAny(hX_Stop_EX, typeof(bool))) == true)
            {
                ADS.WriteAny(hX_Stop_EX, false);
            }
           
        }

        public void Y_STOP()
        {
            ADS.WriteAny(hY_Stop_EX, true);
        }

        public void Y_STOP_Clr()
        {
            if (Convert.ToBoolean(ADS.ReadAny(hY_Stop_EX, typeof(bool))) == true)
            {
                ADS.WriteAny(hY_Stop_EX, false);
            }
        }
        #endregion

        #region Pos_Offset_Clear (원점)
        public void Set_XPos_Clr()
        {
            if (IsConnect)
            {
                ADS.WriteAny(hX_Pos_Offset_Ex, true);

                if (Convert.ToBoolean(ADS.ReadAny(hX_Pos_Offset_Ex, typeof(bool))) == true)
                {
                    ADS.WriteAny(hX_Pos_Offset_Ex, false);
                }

            }
                
        }

        public void Set_YPos_Clr()
        {
            if (IsConnect)
            {
                ADS.WriteAny(hY_Pos_Offset_Ex, true);

                if (Convert.ToBoolean(ADS.ReadAny(hY_Pos_Offset_Ex, typeof(bool))) == true)
                {
                    ADS.WriteAny(hY_Pos_Offset_Ex, false);
                }
            }

        }
        #endregion

        #region Get_PID
        public string GetX_P_GAIN()
        {
            ADS.WriteAny(hX_GetPID_Ex, true);
            return ADS.ReadAny(hX_GetP_Gain, typeof(Int32)).ToString();
        }
        public string GetX_I_GAIN()
        {
            ADS.WriteAny(hX_GetPID_Ex, true);
            return ADS.ReadAny(hX_GetI_Gain, typeof(Int32)).ToString();
        }
        public string GetX_D_GAIN()
        {
            ADS.WriteAny(hX_GetPID_Ex, true);
            return ADS.ReadAny(hX_GetD_Gain, typeof(Int32)).ToString();
        }
        public string GetY_P_GAIN()
        {
            ADS.WriteAny(hY_GetPID_Ex, true);
            return ADS.ReadAny(hY_GetP_Gain, typeof(Int32)).ToString();
        }
        public string GetY_I_GAIN()
        {
            ADS.WriteAny(hY_GetPID_Ex, true);
            return ADS.ReadAny(hY_GetI_Gain, typeof(Int32)).ToString();
        }
        public string GetY_D_GAIN()
        {
            ADS.WriteAny(hY_GetPID_Ex, true);
            return ADS.ReadAny(hY_GetD_Gain, typeof(Int32)).ToString();
        }
        #endregion
    }
}
