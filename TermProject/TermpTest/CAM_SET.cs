using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermpTest
{
    internal class CAM_SET
    {
        private VideoCapture _capture;
        private Mat _frame;
        private List<List<Point2f>> _imagePoints;
        private List<List<Point3f>> _objectPoints;
        private readonly Size _chessboardSize = new Size(9, 6);
        private readonly float _squareSize = 0.025f;


    }
}
