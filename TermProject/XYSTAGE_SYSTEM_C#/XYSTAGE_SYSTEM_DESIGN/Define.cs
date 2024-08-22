using OpenCvSharp;

namespace XYSTAGE_SYSTEM_DESIGN
{
    #region video
    internal partial class Define
    {
        public readonly int _WIDTH = 640;
        public readonly int _HEIGHT = 480;
        public readonly int _VIDEO_MARGIN = 12;
    }
    #endregion

    #region 색상 범위
    internal partial class Define
    {
        public readonly Scalar lowerRed1 = new Scalar(0, 100, 100); // 빨간색 하한선 1
        public readonly Scalar upperRed1 = new Scalar(10, 255, 255); // 빨간색 상한선 1
        public readonly Scalar lowerRed2 = new Scalar(160, 100, 100); // 빨간색 하한선 2
        public readonly Scalar upperRed2 = new Scalar(180, 255, 255); // 빨간색 상한선 2
    }
    #endregion

}
