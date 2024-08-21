using OpenCvSharp;

namespace TermpTest
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
        public readonly Scalar LOWER_RED1 = new Scalar(0, 100, 100); // 빨간색 하한선 1
        public readonly Scalar UPPER_RED1 = new Scalar(10, 255, 255); // 빨간색 상한선 1
        public readonly Scalar LOWER_RED2 = new Scalar(160, 100, 100); // 빨간색 하한선 2
        public readonly Scalar UPPER_RED2 = new Scalar(180, 255, 255); // 빨간색 상한선 2
        public readonly Scalar LOVER_BLUE = new Scalar(100, 100, 50); // 빨간색 하한선 1
        public readonly Scalar UPPER_BLUE = new Scalar(135, 255, 255); // 빨간색 상한선 1
        public readonly Scalar LOWER_GREEN = new Scalar(34, 100, 50); // 빨간색 하한선 1
        public readonly Scalar UPPER_GREEN = new Scalar(78, 255, 255); // 빨간색 상한선 1
    }
    #endregion
}