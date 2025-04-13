using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XYSTAGE_SYSTEM_DESIGN
{
    public class CAxisParameter                 // 작업 등록 파라미터 
    {
        public int x_pos;
        public int y_pos;
        public double x_vel;
        public double y_vel;
        public double x_acc;
        public double y_acc;
        public double x_dec;
        public double y_dec;


        public CAxisParameter(int _x_pos, int _y_pos) 
        {
            x_pos = _x_pos;
            y_pos = _y_pos;
            x_vel = 500;
            y_vel = 500;    
            x_acc = 500;
            y_acc = 500;
            x_dec = 500;
            y_dec = 500;
        }
        public CAxisParameter(int _x_pos, int _y_pos, double _x_vel, double _y_vel) 
        {
            x_pos = _x_pos;
            y_pos = _y_pos;
            x_vel = _x_vel;
            y_vel = _y_vel;
            x_acc = 500;
            y_acc = 500;
            x_dec = 500;
            y_dec = 500;
        }
        public CAxisParameter(int _x_pos, int _y_pos, double _x_vel, double _y_vel, double _x_acc, double _y_acc) 
        {
            x_pos = _x_pos;
            y_pos = _y_pos;
            x_vel = _x_vel;
            y_vel = _y_vel;
            x_acc = _x_acc;
            y_acc = _y_acc;
            x_dec = 500;
            y_dec = 500;
        }

        public CAxisParameter(int _x_pos, int _y_pos, double _x_vel, double _y_vel, double _x_acc, double _y_acc, double _x_dec, double _y_dec) 
        {
            x_pos = _x_pos;
            y_pos = _y_pos;
            x_vel = _x_vel;
            y_vel = _y_vel;
            x_acc = _x_acc;
            y_acc = _y_acc;
            x_dec = _x_dec; 
            y_dec = _y_dec;
        }


    }
}
